using PCWeb.Models.Name;
using PCWeb.Models.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Root
{
    public class CPU
    {
        public int CPUId { get; set; }
        [DisplayName("Mã CPU")]
        [Required(ErrorMessage = "Mã CPU không được để trống")]
        public string CPUCode { get; set; }
        [DisplayName("Loại CPU")]
        [Required(ErrorMessage = "Loại CPU không được để trống")]
        public string CPUModel { get; set; }
        [DisplayName("Socket")]
        [Required(ErrorMessage = "Socket không được để trống")]
        public string CPUSocket { get; set; }
        [DisplayName("Xung cơ bản")]
        [Required(ErrorMessage = "Xung cơ bản không được để trống")]
        public double CPUSpeed { get; set; }
        [DisplayName("Xung tối đa")]
        [Required(ErrorMessage = "Xung tối đa không được để trống")]
        public double CPUTurbo { get; set; }
        [DisplayName("Bộ nhớ cache")]
        [Required(ErrorMessage = "Bộ nhớ cache không được để trống")]
        public string CPUCache { get; set; }
        [DisplayName("Số nhân")]
        [Required(ErrorMessage = "Số nhân không được để trống")]
        public int CPUCore { get; set; }
        [DisplayName("Số luồng")]
        [Required(ErrorMessage = "Số luồng không được để trống")]
        public int CPUThread { get; set; }
        [DisplayName("Tiến trình CPU")]
        [Required(ErrorMessage = "Tiến trình CPU không được để trống")]
        public string CPUProcess { get; set; }
        [DisplayName("Tiêu hao")]
        [Required(ErrorMessage = "Tiêu hao không được để trống")]
        public int CPUPower { get; set; }
        public int PCComponentId { get; set; }
        public PCComponent PCComponent { get; set; }
    }
}
