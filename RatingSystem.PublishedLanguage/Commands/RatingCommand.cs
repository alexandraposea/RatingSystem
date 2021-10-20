using MediatR;

namespace RatingSystem.PublishedLanguage.Commands
{
    public class RatingCommand : IRequest
    {
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public decimal RatingValue { get; set; }
    }
}
