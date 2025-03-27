using System.ComponentModel.DataAnnotations;

namespace TravelRoutes.Domain.Entities
{
    public class Route
    {
        [Key]
        [Display(Name ="Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Origem")]
        public string Origin { get; set; }
        [Required]
        [Display(Name = "Destino")]
        public string Destination { get; set; }
        [Required]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }
    }
}
