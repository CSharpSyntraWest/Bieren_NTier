
using AutoMapper;
using Bieren.BusinessLayer.Models;
using Bieren.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bieren.WPF.Automapper
{
    public class AutomapperConfig : Profile
    {

        public AutomapperConfig()
        {
            CreateMap<BO_User, User>();
            CreateMap<User, BO_User>();
            CreateMap<BO_BierSoort, BierSoort>();
            CreateMap<BierSoort, BO_BierSoort>();
            CreateMap<BO_Brouwer, Brouwer>();
            CreateMap<Brouwer, BO_Brouwer>();
            CreateMap<BO_Bier, Bier>();
            CreateMap<Bier, BO_Bier>();
        }

    }
}
