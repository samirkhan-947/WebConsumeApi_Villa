using System.ComponentModel.DataAnnotations;

namespace WebHouse.Web.Models.DTO
{
    public class VillaCreateDTO
    {
        
       
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        public string? Details { get; set; }
        public double? Rate { get; set; }
        [Required]
        public int? Occupancy { get; set; }
        [Required]
        public int? Sqrt { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public string? Amenity { get; set; }

    }
}
