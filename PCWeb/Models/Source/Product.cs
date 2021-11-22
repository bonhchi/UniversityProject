using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PCWeb.Models.Source
{
    public class Product
    {
        [DisplayName("Mã số")]
        public int ProductId { get; set; }
        [DisplayName("Mã sản phẩm")]
        [MinLength(14, ErrorMessage = "Mã SP có ít nhất 14 ký tự")]
        [MaxLength(20, ErrorMessage = "Mã SP không được quá 20 ký tự")]
        public string ProductCode { get; set; }
        [DisplayName("Mã vạch")]
        [Required(ErrorMessage = "Thiếu mã vạch")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Mã vạch không hợp lệ")]
        [MinLength(10,ErrorMessage = "Mã vạch có ít nhất 10 số")]
        public string ProductSeries { get; set; }
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string ProductName { get; set; }
        [DisplayName("Hình ảnh")]
        public string ProductImage { get; set; }
        [DisplayName("Giá")]
        [Range(0, int.MaxValue, ErrorMessage = "Sản phẩm không được có giá dưới 0")]
        public double ProductPrice { get; set; }
        [DisplayName("Giá khuyến mãi")]
        [Range(0, int.MaxValue, ErrorMessage = "Sản phẩm không được có giá dưới 0")]
        public double ProductPriceReality { get; set; }
        [DisplayName("Mô tả sản phẩm")]
        public string ProductDescription { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime DayCreate { get; set; }
        [DisplayName("Bảo hành")]
        [Range(12, 120, ErrorMessage = "Sản phẩm phải được bảo hành từ 12 tháng 120 tháng")]
        public int ProductWarranty { get; set; }
        [DisplayName("Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int ProductQuantity { get; set; }
        [DisplayName("Tổng trọng lượng")]
        [Range(0, double.MaxValue, ErrorMessage = "Trọng lượng phải lớn hơn 0")]
        public double ProductPackage { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
