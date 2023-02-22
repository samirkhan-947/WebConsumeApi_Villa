﻿using System.ComponentModel.DataAnnotations;

namespace WebConsumeApi_Villa.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
        public double? Rate { get; set; }
        public int? Sqft { get; set; }
        public int? Occupancy { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
