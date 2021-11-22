using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [DisplayName("Tên User")]
        public string CommentUser { get; set; }
        [Required(ErrorMessage = "Bạn phải điền nhận xét")]
        public string CommentContent { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
