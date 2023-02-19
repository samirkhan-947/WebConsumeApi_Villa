using System.ComponentModel.DataAnnotations;

namespace WebConsumeApi_Villa.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqrt { get; set; }

    }
}
