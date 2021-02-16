using System.Collections.Generic;

namespace CherryShop_API.DTOs
{
    public class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }
}
