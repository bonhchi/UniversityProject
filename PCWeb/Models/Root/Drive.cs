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
    public class Drive
    {
        public int DriveId { get; set; }
        [DisplayName("Loại ổ cứng")]
        [Required(ErrorMessage = "Loại ổ cứng không được để trống")]
        public string DriveSpec { get; set; }
        [DisplayName("Dung lượng")]
        [Required(ErrorMessage = "Dung lượng không được để trống")]
        public string DriveCapacity { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public string DriveSize { get; set; }
        [DisplayName("Kết nối")]
        [Required(ErrorMessage = "Kết nối không được để trống")]
        public string DriveConnectivity { get; set; }
        [DisplayName("Tốc độ đọc")]
        [Required(ErrorMessage = "Tốc độ đọc không được để trống")]
        public string DriveRead { get; set; }
        [DisplayName("Tốc độ ghi")]
        [Required(ErrorMessage = "Tốc độ ghi không được để trống")]
        public string DriveWrite { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
