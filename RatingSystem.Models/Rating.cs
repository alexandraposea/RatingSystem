using System;
using System.Collections.Generic;

#nullable disable

namespace RatingSystem.Models
{
    public partial class Rating
    {
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public decimal RatingValue { get; set; }
    }
}
