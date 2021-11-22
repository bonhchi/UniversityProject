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
    public class Laptop
    {
        [DisplayName("STT")]
        public int LaptopId { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "Tên Laptop không được để trống")]
        public int ProductId { get; set; }
        [DisplayName("Mã Laptop")]
        [Required(ErrorMessage = "Loại laptop không được để trống")]
        public string LaptopSeries { get; set; }
        [DisplayName("CPU")]
        [Required(ErrorMessage = "CPU không được để trống")]
        public string LaptopCPU { get; set; }
        [DisplayName("Màn hình")]
        [Required(ErrorMessage = "Màn hình không được để trống")]
        public string LaptopScreen { get; set; }
        [DisplayName("Card đổ họa")]
        [Required(ErrorMessage = "Card đổ họa không được để trống")]
        public string LaptopGPU { get; set; }
        [DisplayName("RAM")]
        [Required(ErrorMessage = "RAM không được để trống")]
        public string LaptopRAM { get; set; }
        [DisplayName("Webcam")]
        [Required(ErrorMessage = "Webcam không được để trống")]
        public string LaptopWebcam { get; set; }
        [DisplayName("Lưu trữ")]
        [Required(ErrorMessage = "Lưu trữ không được để trống")]
        public string LaptopStorage { get; set; }
        [DisplayName("Cổng kết nối")]
        [Required(ErrorMessage = "Cổng kết nối không được để trống")]
        public string LaptopPort { get; set; }
        [DisplayName("Kết nối")]
        [Required(ErrorMessage = "Kết nối không được để trống")]
        public string LaptopConnectivity { get; set; }
        [DisplayName("Bàn phím")]
        [Required(ErrorMessage = "Bàn phím không được để trống")]
        public string LaptopKeyboard { get; set; }
        [DisplayName("Hệ điều hành")]
        [Required(ErrorMessage = "Hệ điều hành không được để trống")]
        public string LaptopOS { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public string LaptopDimension { get; set; }
        [DisplayName("Pin")]
        [Required(ErrorMessage = "Pin không được để trống")]
        public string LaptopBattery { get; set; }
        [DisplayName("Cân nặng")]
        [Required(ErrorMessage = "Cân nặng không được để trống")]
        public string LaptopWeight { get; set; }
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public string LaptopColor { get; set; }
        public int LaptopCategoryId { get; set; }
        public LaptopCategory LaptopCategory { get; set; }
    }
}
