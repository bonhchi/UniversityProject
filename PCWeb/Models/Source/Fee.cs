using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Fee
    {
        public int FeeId { get; set; }
        [DisplayName("Tên phí")]
        [Required(ErrorMessage = "Tên phí không được để trống")]
        public string FeeName { get; set; }
        [DisplayName("Mức phí")]
        [Required(ErrorMessage = "Mức phí không được để trống")]
        [Range(0 , double.MaxValue , ErrorMessage = "Mức phí không được âm")]
        public double FeeAmount { get; set; }
        [DisplayName("Đơn vị")]
        [Required(ErrorMessage = "Đơn vị không được để trống")]
        public string FeeUnit { get; set; }

    }
}
