using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class RevenueDetail
    {
        public int RevenueDetailId { get; set; }
        public int RevenueId { get; set; }
        public DateTime DateIssue { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        [DisplayName("Giá thực tế")]
        public double PriceReality { get; set; }
        public Revenue Revenue { get; set; }
    }
}
