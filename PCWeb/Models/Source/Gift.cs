using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        [Required(ErrorMessage = "Quà tặng không được để trống")]
        public string GiftName { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
    }
}
