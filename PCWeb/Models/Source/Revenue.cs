using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Revenue
    {
        [DisplayName("Mã doanh thu")]
        public int RevenueId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }
        [DisplayName("Mã vạch")]
        public string ProductSeries { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        [DisplayName("Hình ảnh")]
        public string ProductImage { get; set; }
        [DisplayName("Giá")]
        public double ProductPrice { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime DayCreate { get; set; }
        public int RevenueDetailId { get; set; }
        [DisplayName("Ngày hết hạn")]
        public DateTime DateExpired { get; set; }
        public List<RevenueDetail> RevenueDetails { get; set; }
        
    }
}
