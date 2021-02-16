﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryShop_API.Data
{
    [Table("Brands")]
    public partial class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products{ get; set; }
    }
}
