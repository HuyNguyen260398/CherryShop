using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CherryShop_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public float? DiscountPercent { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public virtual BrandDTO Brand { get; set; }
        public virtual IList<ImageDTO> Images { get; set; }
    }

    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public float? DiscountPercent { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }

    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public float? DiscountPercent { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
