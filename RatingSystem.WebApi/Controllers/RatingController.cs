using Microsoft.AspNetCore.Mvc;
using RatingSystem.Application.Queries;
using RatingSystem.PublishedLanguage.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public RatingController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddRating")]
        public async Task<string> AddRating(RatingCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return "OK";
        }


        [HttpGet]
        [Route("AverageRating")]
        public async Task<List<GetAverageRating.Model>> GetListOfAccounts([FromQuery] GetAverageRating.Query query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }
    }
}
