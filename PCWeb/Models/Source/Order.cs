using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Order
    {
        [DisplayName("Mã đơn hàng")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [DisplayName("Tên khách hàng")]
        public string CusName { get; set; }
        [DisplayName("Địa Chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage ="Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Phải nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        [DisplayName("Ngày lập")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Lưu ý")]
        public string Note { get; set; }
        [DisplayName("Tình trạng thanh toán")]
        public string OrderCheckout { get; set; }
        public int PaymentMethodId { get; set; }
        public int OrderConditionId { get; set; }
        public OrderCondition OrderCondition { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
