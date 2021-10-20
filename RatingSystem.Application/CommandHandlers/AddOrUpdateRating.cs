using FluentValidation;
using MediatR;
using RatingSystem.Data;
using RatingSystem.Models;
using RatingSystem.PublishedLanguage.Commands;
using RatingSystem.PublishedLanguage.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.Application.WriteOperations
{
    public class AddOrUpdateRating : IRequestHandler<RatingCommand>
    {
        private readonly IMediator _mediator;
        private readonly RatingDbContext _dbContext;

        public AddOrUpdateRating(IMediator mediator, RatingDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public class Validator : AbstractValidator<RatingCommand>
        {
            public Validator()
            {
                RuleFor(q => q).Must(q =>
                {
                    return !string.IsNullOrEmpty(q.ExternalId);
                }).WithMessage("External id must be provided");

                RuleFor(q => q).Must(q =>
                {
                    return !string.IsNullOrEmpty(q.Category);
                }).WithMessage("Category must be provided");

                RuleFor(q => q).Must(q =>
                {
                    return !string.IsNullOrEmpty(q.UserId);
                }).WithMessage("User id must be provided");

                RuleFor(q => q).Must(q =>
                {
                    return q.RatingValue > 0;
                }).WithMessage("Rating value must be provided");
            }
        }

        public async Task<Unit> Handle(RatingCommand request, CancellationToken cancellationToken)
        {
            var rating = _dbContext
                .Ratings
                .FirstOrDefault(x => x.ExternalId == request.ExternalId
                && x.Category == request.Category
                && x.UserId == request.UserId);

            INotification @event;

            if (rating == null)
            {
                rating = new Rating()
                {
                    ExternalId = request.ExternalId,
                    UserId = request.UserId,
                    Category = request.Category,
                    RatingValue = request.RatingValue
                };

                _dbContext.Ratings.Add(rating);

                @event = new RatingCreated() { ExternalId = request.ExternalId, Category = request.Category, UserId = request.UserId, RatingValue = request.RatingValue };
            }
            else
            {
                rating.RatingValue = request.RatingValue;

                @event = new RatingUpdated() { RatingValue = request.RatingValue };
            }

            _dbContext.SaveChanges();

            await _mediator.Publish(@event, cancellationToken);

            var ratingAverage = _dbContext
                .RatingAverages
                .FirstOrDefault(x => x.ExternalId == request.ExternalId && x.Category == request.Category);

            if (ratingAverage == null)
            {
                ratingAverage = new RatingAverage()
                {
                    ExternalId = request.ExternalId,
                    Category = request.Category,
                    AverageRating = request.RatingValue
                };
                _dbContext.RatingAverages.Add(ratingAverage);

                @event = new RatingAverageCreated() { ExternalId = request.ExternalId, Category = request.Category, AverageRating = request.RatingValue };
            }
            else
            {
                var averageRating = _dbContext
                    .Ratings
                    .Where(x => x.ExternalId == request.ExternalId && x.Category == request.Category)
                    .Average(x => x.RatingValue);
                ratingAverage.AverageRating = averageRating;

                @event = new RatingAverageUpdated() { AverageRating = averageRating };
            }
            _dbContext.SaveChanges();
            await _mediator.Publish(@event, cancellationToken);

            return Unit.Value;
        }
    }
}


