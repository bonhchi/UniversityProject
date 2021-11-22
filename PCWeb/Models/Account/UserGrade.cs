using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Account
{
    public class UserGrade
    {
        public int UserGradeId { get; set; }
        public string UserGradeName { get; set; }
        public int UserGradeEntryPoint { get; set; }
        public double UserGradeDiscount { get; set; }
    }
}
