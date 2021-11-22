using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class OrderDetail
    {
        [DisplayName("STT")]
        public int OrderDetailId { get; set; }
        [DisplayName("Mã đơn hàng")]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

    }
}
