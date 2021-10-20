using FluentValidation;
using MediatR;
using RatingSystem.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.Application.Queries
{
    public class GetAverageRating
    {
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(q => q).Must(q =>
                {
                    return !string.IsNullOrEmpty(q.ExternalId) && !string.IsNullOrEmpty(q.Category);
                }).WithMessage("You must provide external id and category");
            }
        }
        public class Query : IRequest<List<Model>>
        {
            public string ExternalId { get; set; }
            public string Category { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly RatingDbContext _dbContext;

            public QueryHandler(RatingDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var db = _dbContext
                    .RatingAverages
                    .Where(x => x.ExternalId == request.ExternalId && x.Category == request.Category);

                var result = db.Select(x => new Model
                {
                    ExternalId = x.ExternalId,
                    Category = x.Category,
                    AverageRating = x.AverageRating
                }).ToList();

                return Task.FromResult(result);
            }
        }

        public partial class Model
        {
            public string ExternalId { get; set; }
            public string Category { get; set; }
            public decimal AverageRating { get; set; }
        }
    }
}
