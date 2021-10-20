using System;
using System.Collections.Generic;

#nullable disable

namespace RatingSystem.Models
{
    public partial class RatingAverage
    {
        public string ExternalId { get; set; }
        public string Category { get; set; }
        public decimal AverageRating { get; set; }
    }
}
