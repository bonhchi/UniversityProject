using PCWeb.Models.Source;
using System.ComponentModel;

namespace PCWeb.Models.Name
{
    public class PCComponent
    {
        [DisplayName("STT")]
        public int PCComponentId { get; set; }
        public string PCComponentName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ComponentCategoryId { get; set; }
        public ComponentCategory ComponentCategory { get; set; }
    }
}
