using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingSystem.PublishedLanguage.Events
{
    public class RatingAverageCreated: INotification
    {
        public string ExternalId { get; set; }
        public string Category { get; set; }
        public decimal AverageRating { get; set; }
    }
}
