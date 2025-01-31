using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public int? Size { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
