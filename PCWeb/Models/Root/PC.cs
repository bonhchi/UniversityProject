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
    public class PC
    {
        [DisplayName("STT")]
        public int PCId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [DisplayName("CPU")]
        [Required(ErrorMessage = "CPU không được để trống")]
        public string PCCpu { get; set; }
        [DisplayName("Mainboard")]
        [Required(ErrorMessage = "Mainboard không được để trống")]
        public string PCMainboard { get; set; }
        [DisplayName("RAM")]
        [Required(ErrorMessage = "RAM không được để trống")]
        public string PCRam { get; set; }
        [DisplayName("Card đồ họa")]
        [Required(ErrorMessage = "Card đồ họa không được để trống")]
        public string PCGpu { get; set; }
        [DisplayName("HDD & SSD")]
        [Required(ErrorMessage = "Lưu trữ không được để trống ")]
        public string PCStorage { get; set; }
        [DisplayName("Bộ nguồn")]
        [Required(ErrorMessage = "Bộ nguồn không được để trống")]
        public string PCPsu { get; set; }
        [DisplayName("Thùng máy")]
        [Required(ErrorMessage = "Thùng máy không được để trống")]
        public string PCCase { get; set; }
        [DisplayName("Tản nhiệt")]
        [Required(ErrorMessage = "Tản nhiệt không được để trống")]
        public string PCCooling { get; set; }
        public int PCCategoryId { get; set; }
        public PCCategory PCCategory { get; set; }
    }
}
