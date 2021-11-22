using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Category
    {
        [DisplayName("STT")]
        public int CategoryId { get; set; }
        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
