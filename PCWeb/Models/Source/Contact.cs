using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Contact
    {
        [DisplayName("STT")]
        public int ContactId { get; set; }
        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string ContactName { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string ContactPhone { get; set; }
        [DisplayName("Địa chi")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string ContactAddress { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string ContactEmail { get; set; }
        [DisplayName("Ghi chú")]
        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string ContactNote { get; set; }
    }
}
