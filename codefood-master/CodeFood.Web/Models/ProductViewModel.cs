using System.ComponentModel.DataAnnotations;

namespace CodeFood.Web.Models
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(30)]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? ImagePath { get; set; }
    }
}
