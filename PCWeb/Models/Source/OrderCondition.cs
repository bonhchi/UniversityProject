using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class OrderCondition
    {
        public int OrderConditionId { get; set; }
        [DisplayName("Trạng thái đơn hàng")]
        public string OrderConditionName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
