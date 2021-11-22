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
    public class Case
    {
        public int CaseId { get; set; }
        [DisplayName("Loại Case")]
        [Required(ErrorMessage = "Loại Case không được để trống")]
        public string CaseSpec { get; set; }
        [DisplayName("Mainboard hỗ trợ")]
        [Required(ErrorMessage = "Mainboard hỗ trợ không được để trống")]
        public string CaseSupport { get; set; }
        [DisplayName("Ổ cứng hỗ trợ")]
        [Required(ErrorMessage = "Ổ cứng hỗ trợ không được để trống")]
        public string CaseDrive { get; set; }
        [DisplayName("Số cổng kết nối")]
        [Required(ErrorMessage = "Số cổng kết nối không được để trống")]
        public string CasePort { get; set; }
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public string CaseColor { get; set; }
        [DisplayName("Chất liệu")]
        [Required(ErrorMessage = "Chất liệu không được để trống")]
        public string CaseMaterial { get; set; }
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Kích thước không được để trống")]
        public string CaseDimension { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
