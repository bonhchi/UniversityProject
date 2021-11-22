using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Models.Account
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Address { get; set; }
        public int UserGradeId { get; set; }
        public UserGrade UserGrade { get; set; }
        public int UserPoint { get; set; }
    }
}
