
using AutoMapper;
using Bieren.BusinessLayer.Models;
using Bieren.DataLayer.Models;
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


            CreateMap<BO_User, DbUser>();
            CreateMap<DbUser, BO_User>();
            CreateMap<BO_BierSoort, DbSoort>().ForMember(dest => dest.Soort, opt => opt.MapFrom(src => src.SoortNaam));
            CreateMap<DbSoort, BO_BierSoort>().ForMember(dest => dest.SoortNaam, opt => opt.MapFrom(src => src.Soort));
            CreateMap<BO_Brouwer, DbBrouwer>();
            CreateMap<DbBrouwer, BO_Brouwer>();
            CreateMap<BO_Bier, DbBier>()
                .ForMember(desc => desc.SoortNrNavigation, do_ => do_.MapFrom(scr => scr.BierSoort))
                .ForMember(desc => desc.BrouwerNrNavigation, do_ => do_.MapFrom(scr => scr.Brouwer));

            CreateMap<DbBier, BO_Bier>()
                .ForMember(desc => desc.BierSoort, do_ => do_.MapFrom(scr => scr.SoortNrNavigation))
                .ForMember(desc => desc.Brouwer, do_=> do_.MapFrom(scr => scr.BrouwerNrNavigation));

            CreateMap<BO_Aandeel, Aandeel>();
            CreateMap<Aandeel, BO_Aandeel>();
        }

    }
}
