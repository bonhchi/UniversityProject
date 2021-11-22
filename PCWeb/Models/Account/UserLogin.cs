using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Account
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nhập password")]
        [DataType(DataType.Password)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Nhớ mật khẩu: ")]
        public bool RememberMe { get; set; }
    }
}
