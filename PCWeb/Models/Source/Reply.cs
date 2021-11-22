using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        [DisplayName("Tên User")]
        public string ReplyUser { get; set; }
        [Required(ErrorMessage = "Bạn phải điền phản hồi")]
        public string ReplyContent { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
