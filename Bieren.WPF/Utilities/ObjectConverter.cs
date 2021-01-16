using Bieren.BusinessLayer.Models;
using Bieren.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.WPF.Utilities
{
    public static class ObjectConverter
    {
        public static List<User> BO_UsersToUsers(IList<BO_User> bo_users)
        {
            List<User> users = new List<User>();
            foreach (BO_User bo_user in bo_users)
            {
                users.Add(new User()
                {
                    UserNr = bo_user.UserNr,
                    Voornaam = bo_user.Voornaam,
                    Familienaam = bo_user.Familienaam,
                    Email = bo_user.Email,
                    GeboorteDatum = bo_user.GeboorteDatum,
                    FavorieteBieren = BO_BierenToBieren(bo_user.FavorieteBieren)
                });
            }
            return users;
        }

        public static IList<Bier> BO_BierenToBieren(IList<BO_Bier> bo_bieren)
        {
            List<Bier> bieren = new List<Bier>();
            foreach (BO_Bier bo_bier in bo_bieren)
            {
                bieren.Add(BO_BierToBier(bo_bier));
            }
            return bieren;
        }

        private static Brouwer BO_BrouwerToBrouwer(BO_Brouwer bo_brouwer)
        {
            if (bo_brouwer == null) return null;
            return new Brouwer()
            {
                BrouwerNr = bo_brouwer.BrouwerNr,
                BrNaam = bo_brouwer.BrNaam,
                Straat = bo_brouwer.Straat,
                PostCode = bo_brouwer.PostCode,
                Gemeente = bo_brouwer.Gemeente,
                Omzet = bo_brouwer.Omzet,
                Bieren = new ObservableCollection<Bier>(BO_BierenToBieren(bo_brouwer.Bieren))
            };
        }

        public static Bier BO_BierToBier(BO_Bier bo_bier)
        {
            if (bo_bier == null) return null;
            return new Bier()
            {
                BierNr = bo_bier.BierNr,
                Naam = bo_bier.Naam,
                Alcohol = bo_bier.Alcohol,
                BierSoort = BO_BiersoortToBiersoort(bo_bier.BierSoort),
                Brouwer = BO_BrouwerToBrouwer(bo_bier.Brouwer)
            };

        }

        public static BierSoort BO_BiersoortToBiersoort(BO_BierSoort bo_bierSoort)
        {
            if (bo_bierSoort == null) return null;
            return new BierSoort()
            {
                 SoortNr = bo_bierSoort.SoortNr,
                 SoortNaam = bo_bierSoort.SoortNaam
            };
        }

        public static IList<BierSoort> BO_BierSoortenToBierSoorten(IList<BO_BierSoort> bo_bierSoorten)
        {
            IList<BierSoort> bierSoorten = new List<BierSoort>();
            foreach (BO_BierSoort bo_bierSoort in bo_bierSoorten)
            {
                bierSoorten.Add(BO_BiersoortToBiersoort(bo_bierSoort));
            }
            return bierSoorten;
        }

        public static IList<Brouwer> BO_BrouwersToBrouwers(IList<BO_Brouwer> bo_brouwers)
        {
            IList<Brouwer> brouwers = new List<Brouwer>();
            foreach (BO_Brouwer bo_brouwer in bo_brouwers)
            {
                brouwers.Add(BO_BrouwerToBrouwer(bo_brouwer));
            }
            return brouwers;
        }


        public static BO_Bier BierToBO_Bier(Bier bier)
        {
            if (bier == null) return null;
            return new BO_Bier()
            {
                BierNr = bier.BierNr,
                Naam = bier.Naam,
                Alcohol = bier.Alcohol,
                BierSoort = BiersoortToBO_Biersoort( bier.BierSoort),
                Brouwer = BrouwerToBO_Brouwer(bier.Brouwer)
            };
        }
        private static BO_Brouwer BrouwerToBO_Brouwer(Brouwer brouwer)
        {
            if (brouwer == null) return null;
            return new BO_Brouwer()
            {
                BrouwerNr = brouwer.BrouwerNr,
                BrNaam = brouwer.BrNaam,
                Straat = brouwer.Straat,
                PostCode = brouwer.PostCode,
                Gemeente = brouwer.Gemeente,
                Omzet = brouwer.Omzet,
                Bieren = BierenToBO_Bieren(brouwer.Bieren)
            };
        }
        public static IList<BO_Bier> BierenToBO_Bieren(IList<Bier> bieren)
        {
            List<BO_Bier> bo_bieren = new List<BO_Bier>();
            foreach (Bier bier in bieren)
            {
                bo_bieren.Add(BierToBO_Bier(bier));
            }
            return bo_bieren;
        }
        public static BO_BierSoort BiersoortToBO_Biersoort(BierSoort bierSoort)
        {
            if (bierSoort == null) return null;
            return new BO_BierSoort()
            {
                SoortNr = bierSoort.SoortNr,
                SoortNaam = bierSoort.SoortNaam
            };
        }
    }
}
