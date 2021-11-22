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
    public class Power
    {
        public int PowerId { get; set; }
        [DisplayName("Công suất")]
        [Range(0, int.MaxValue, ErrorMessage = "Công suất không được dưới 0")]
        public int PowerCapacity { get; set; }
        [DisplayName("Cổng kết nối")]
        [Required(ErrorMessage = "Cổng kết nối không được để trống")]
        public string PowerPort { get; set; }
        [DisplayName("Chuẩn nguồn")]
        [Required(ErrorMessage = "Chuẩn nguồn không được để trống")]
        public string PowerEfficiency { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước hỗ trợ không được để trống")]
        public string PowerDimension { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }

    }
}
