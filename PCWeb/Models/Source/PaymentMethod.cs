using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        [DisplayName("Phương thức thanh toán")]
        public string PaymentMethodName { get; set; }
    }
}
