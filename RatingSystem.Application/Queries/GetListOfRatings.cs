using FluentValidation;
using MediatR;
using RatingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.Application.Queries
{
    public class GetListOfRatings
    {
        public class Query : IRequest<List<Model>>
        {
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
                var db = _dbContext.Ratings;

                var result = db.Select(x => new Model
                {
                    ExternalId = x.ExternalId,
                    Category = x.Category,
                    UserId = x.UserId,
                    RatingValue = x.RatingValue
                }).ToList();

                return Task.FromResult(result);
            }
        }

        public partial class Model
        {
            public string ExternalId { get; set; }
            public string Category { get; set; }
            public string UserId { get; set; }
            public decimal RatingValue { get; set; }
        }
    }
}
