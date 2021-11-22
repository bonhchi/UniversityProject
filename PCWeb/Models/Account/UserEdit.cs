using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Account
{
    public class UserEdit
    {
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string FirstName { get; set; }
        [DisplayName("Họ")]
        [Required(ErrorMessage = "Họ không được để trống")]
        public string LastName { get; set; }
        [DisplayName("Giới tính")]
        public string Gender { get; set; }
        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Ngày sinh không được bỏ trống")]
        public DateTime DayOfBirth { get; set; }
        [MinLength(10)]
        [Required(ErrorMessage = "SĐT không được để trống")]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Đĩa chỉ không được để trống")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

    }
}
