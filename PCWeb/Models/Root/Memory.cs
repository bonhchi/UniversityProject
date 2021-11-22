using PCWeb.Models.Name;
using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Root
{
    public class Memory
    {
        public int MemoryId { get; set; }
        [DisplayName("Dung lượng")]
        [Required(ErrorMessage = "Dung lượng không được để trống")]
        public string MemoryCapacity { get; set; }
        [DisplayName("Bus RAM")]
        [Required(ErrorMessage = "Bus RAM không được để trống")]
        public string MemoryBus { get; set; }
        [DisplayName("Thế hệ RAM")]
        [Required(ErrorMessage = "Thế hệ RAM không được để trống")]
        public string MemoryGen { get; set; }
        [DisplayName("Đèn LED")]
        [Required(ErrorMessage = "Đèn LED không được để trống")]
        public string MemoryLED { get; set; }
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public string MemoryColor { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
