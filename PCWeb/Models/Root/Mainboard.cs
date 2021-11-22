using PCWeb.Models.Name;
using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PCWeb.Models.Root
{
    public class Mainboard
    {
        public int MainboardId { get; set; }
        [DisplayName("Mã Mainboard")]
        [Required(ErrorMessage = "Dung lượng không được để trống")]
        public string MainboardModel { get; set; }
        [DisplayName("CPU hỗ trợ")]
        [Required(ErrorMessage = "CPU hỗ trợ không được để trống")]
        public string MainboardCPU { get; set; }
        [DisplayName("Chipset")]
        [Required(ErrorMessage = "Chipset không được để trống")]
        public string MainboardChipset { get; set; }
        [DisplayName("Kiểu RAM hỗ trợ")]
        [Required(ErrorMessage = "Kiểu RAM hỗ trợ không được để trống")]
        public string MainboardRAMSupport { get; set; }
        [DisplayName("Bus RAM hỗ trợ")]
        [Required(ErrorMessage = "Bus RAM hỗ trợ không được để trống")]
        public string MainboardBusRAM { get; set; }
        [DisplayName("Số khe RAM")]
        [Required(ErrorMessage = "Số khe RAM hỗ trợ không được để trống")]
        public string MainboardSODIMM { get; set; }
        [DisplayName("Dung lượng RAM tối đa")]
        [Required(ErrorMessage = "Dung lượng RAM không được để trống")]
        public string MainboardRAMStorage { get; set; }
        [DisplayName("Lưu trữ")]
        [Required(ErrorMessage = "Lưu trữ không được để trống")]
        public string MainboardStorage { get; set; }
        [DisplayName("Khe M2 hỗ trợ")]
        [Required(ErrorMessage = "Lưu trữ không được để trống")]
        public string MainboardM2Support { get; set; }
        [DisplayName("Kết nối")]
        [Required(ErrorMessage = "Kết nối không được để trống")]
        public string MainboardConnection { get; set; }
        [DisplayName("Cổng xuất hình")]
        [Required(ErrorMessage = "Cổng xuất hình không được để trống")]
        public string MainboardDisplayPort { get; set; }
        [DisplayName("Cổng kết nối")]
        [Required(ErrorMessage = "Cổng kết nối không được để trống")]
        public string MainboardPort { get; set; }
        [DisplayName("Khe PCI")]
        [Required(ErrorMessage = "Khe PCI không được để trống")]
        public string MainboardPCI { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public string MainboardSize { get; set; }
        [DisplayName("Multi GPU")]
        [Required(ErrorMessage = "Multi GPU không được để trống")]
        public string MainboardMultiGPU { get; set; }
        [DisplayName("Âm thanh")]
        [Required(ErrorMessage = "Âm thanh không được để trống")]
        public string MainboardSoundCard { get; set; }
        [DisplayName("Đèn LED")]
        [Required(ErrorMessage = "Đèn LED không được để trống")]
        public string MainboardLED { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
