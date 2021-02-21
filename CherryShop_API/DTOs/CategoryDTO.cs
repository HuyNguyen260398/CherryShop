using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CherryShop_API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }

    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
