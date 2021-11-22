using AutoMapper;
using PCWeb.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWeb.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistation, User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<StaffRegistation, User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
