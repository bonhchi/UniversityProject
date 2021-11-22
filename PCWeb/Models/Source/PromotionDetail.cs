using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class PromotionDetail
    {
        public int PromotionDetailId { get; set; }
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
        public Product Product { get; set; }
        public Promotion Promotion{ get; set; }
    }
}
