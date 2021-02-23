using System.ComponentModel.DataAnnotations;

namespace CherryShop_API.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
        public int? ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
    }

    public class ImageCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        public int? ProductId { get; set; }
    }

    public class ImageUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        public int? ProductId { get; set; }
    }
}
