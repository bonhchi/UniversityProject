using PCWeb.Models.Name;
using PCWeb.Models.Source;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PCWeb.Models.Root
{
    public class Graphic
    {
        public int GraphicId { get; set; }
        [DisplayName("Mã GPU")]
        [Required(ErrorMessage = "Mã GPU không được để trống")]
        public string GraphicGPU { get; set; }
        [DisplayName("Series")]
        [Required(ErrorMessage = "Series không được để trống")]
        public string GraphicSeries { get; set; }
        [DisplayName("Dung lượng")]
        [Required(ErrorMessage = "Dung lượng không được để trống")]
        public string GraphicMemory { get; set; }
        [DisplayName("Loại bộ nhớ")]
        [Required(ErrorMessage = "Loại bộ nhớ không được để trống")]
        public string GraphicBus { get; set; }
        [DisplayName("Đầu cấp nguồn")]
        [Required(ErrorMessage = "Đầu kết nối không được để trống")]
        public string GraphicConnector { get; set; }
        [DisplayName("Tốc độ bộ nhớ")]
        [Required(ErrorMessage = "Tốc độ bộ nhớ bộ nhớ không được để trống")]
        public string GraphicMemFrequency { get; set; }
        [DisplayName("Tốc độ GPU")]
        [Required(ErrorMessage = "Tốc độ GPU không được để trống")]
        public string GraphicGPUBoost { get; set; }
        [DisplayName("Giao tiếp PCI")]
        [Required(ErrorMessage = "Giao tiếp PCI không được để trống")]
        public string GraphicPCI { get; set; }
        [DisplayName("Nhân đồ họa")]
        [Required(ErrorMessage = "Nhân đồ họa không được để trống")]
        public int GraphicCore { get; set; }
        [DisplayName("Số màn hình hỗ trợ")]
        [Required(ErrorMessage = "Số màn hình hỗ trợ không được để trống")]
        public string GraphicMaxMonitor { get; set; }
        [DisplayName("Độ phân giải tối đa")]
        [Required(ErrorMessage = "Độ phân giải tốc đa không được để trống")]
        public string GraphicResolution { get; set; }
        [DisplayName("Công suất")]
        [Required(ErrorMessage = "Công suất nguồn không được để trống")]
        public int GraphicPower { get; set; }
        [DisplayName("Công suất đề nghị")]
        [Required(ErrorMessage = "Công suất đề nghị nguồn không được để trống")]
        public int GraphicMinPower { get; set; }
        [DisplayName("Cổng kết nối")]
        [Required(ErrorMessage = "Cổng kết nối không được để trống")]
        public string GraphicPort { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước nguồn không được để trống")]
        public string GraphicDimension { get; set; }
        [DisplayName("Tản nhiệt")]
        [Required(ErrorMessage = "Tản nhiệt không được để trống")]
        public string GraphicCooling { get; set; }
        [DisplayName("Đèn LED")]
        [Required(ErrorMessage = "Đèn LED không được để trống")]
        public string GraphicLED { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
