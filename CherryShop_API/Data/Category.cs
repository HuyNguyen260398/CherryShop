using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryShop_API.Data
{
    [Table("Categories")]
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
