using PCWeb.Models.Source;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PCWeb.Models
{
    public class Promotion
    {
        [DisplayName("STT")]
        public int PromotionId { get; set; }
        [DisplayName("Tên khuyến mãi")]
        [Required(ErrorMessage = "Khuyến mãi không được để trống")]
        public string PromotionName { get; set; }
        //[RegularExpression(@"^[A-Z]+[0-9]+$", ErrorMessage = "Mã giảm giá phải có số và chữ")]
        [MinLength(2, ErrorMessage = "Kích hoạt giám giá ít nhất 2 kí tự")]
        [DisplayName("Mã giảm giá")]
        public string PromotionCode { get; set; }
        [DisplayName("Tỉ lệ khuyến mãi")]
        [Required(ErrorMessage = "Tỉ lệ khuyến mãi không được để trống")]
        [Range(1,99,ErrorMessage = "Tỉ lệ khuyến mãi chỉ được từ 1 đến 99")]
        public double PromotionDiscount { get; set; }
        public List<Gift> Gifts { get; set; }
        public List<PromotionDetail> PromotionDetails { get; set; }
    }
}
