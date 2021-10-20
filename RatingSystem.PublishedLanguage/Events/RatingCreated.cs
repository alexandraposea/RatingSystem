using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingSystem.PublishedLanguage.Events
{
    public class RatingCreated: INotification
    {
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public decimal RatingValue { get; set; }
    }
}
