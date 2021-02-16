using System.ComponentModel.DataAnnotations.Schema;

namespace CherryShop_API.Data
{
    [Table("Images")]
    public partial class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
