using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Source
{
    public class Brand
    {
        [DisplayName("STT")]
        public int BrandId { get; set; }
        [DisplayName("Tên thương hiệu")]
        [Required]
        public string BrandName { get; set; }
        public List<Product> Products { get; set; }
    }
}
