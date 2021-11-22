using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Name
{
    public class ComponentCategory
    {
        public int ComponentCategoryId { get; set; }
        [DisplayName("Tên linh kiện")]
        public string ComponentCategoryName { get; set; }
    }

}
