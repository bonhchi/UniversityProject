using PCWeb.Models.Root;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Name
{
    public class PCCategory
    {
        public int PCCategoryId { get; set; }
        [DisplayName("Loại PC")]
        public string PCCategoryName { get; set; }
        public List<PC> PCs { get; set; }
    }
}
