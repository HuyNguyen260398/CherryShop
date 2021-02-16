using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryShop_API.Data
{
    [Table("Products")]
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float DiscountPercent { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual IList<Image> Images { get; set; }
    }
}
