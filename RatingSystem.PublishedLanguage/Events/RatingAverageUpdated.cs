using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingSystem.PublishedLanguage.Events
{
    public class RatingAverageUpdated : INotification
    {
        public decimal AverageRating { get; set; }
    }
}
