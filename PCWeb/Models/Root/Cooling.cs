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
    public class Cooling
    {
        public int CoolingId { get; set; }
        [DisplayName("Loại tản nhiệt")]
        [Required(ErrorMessage = "Loại tản nhiệt không được để trống")]
        public string CoolingSpec { get; set; }
        [DisplayName("Socket hỗ trợ")]
        [Required(ErrorMessage = "Socket hỗ trợ không được để trống")]
        public string CoolingSocket { get; set; }
        [DisplayName("Chất liệu")]
        [Required(ErrorMessage = "Chất liệu không được để trống")]
        public string CoolingMaterial { get; set; }
        [DisplayName("Đèn LED")]
        [Required(ErrorMessage = "Đèn LED không được để trống")]
        public string CoolingLED { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public string CoolingDimension { get; set; }
        [DisplayName("Số vòng quay")]
        [Required(ErrorMessage = "Số vòng quay không được để trống")]
        public string CoolingRPM { get; set; }
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public string CoolingColor { get; set; }
        [DisplayName("Độ ồn")]
        [Required(ErrorMessage = "Độ ồn không được để trống")]
        public string CoolingNoise { get; set; }
        [DisplayName("Cân nặng")]
        [Required(ErrorMessage = "Cân nặng không được để trống")]
        public string CoolingWeight { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
