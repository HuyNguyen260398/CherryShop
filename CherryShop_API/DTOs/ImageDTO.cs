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
}
