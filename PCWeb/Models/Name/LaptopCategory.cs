using PCWeb.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Name
{
    public class LaptopCategory
    {
        [DisplayName("STT")]
        public int LaptopCategoryId { get; set; }
        [DisplayName("Loại Laptop")]
        public string LaptopCategoryName { get; set; }
        public List<Laptop> Laptops { get; set; }
    }
}
