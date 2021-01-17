using AutoMapper;
using Bieren.DataLayer.Models;
using Bieren.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Bieren.BusinessLayer.Models
{
    public class BierenDataService : IDataService
    {
        private IBierenRepository _bierenRepository;
        private IMapper _mapper;
        public BierenDataService(IBierenRepository bierenRepository, IMapper mapper)
        {
            _bierenRepository = bierenRepository;
            _mapper = mapper;
        }
        public IList<BO_Bier> GeefAlleBieren()
        {
            //return DbBierenToBieren();
            return _mapper.Map<List<BO_Bier>>(_bierenRepository.GetAll());
        }

        //private IList<BO_Bier> DbBierenToBieren()
        //{
        //    IList<BO_Bier> bieren = new List<BO_Bier>();
        //    using (BierenDbContext bierenDb = new BierenDbContext())
        //    {
        //        var dbBieren = bierenDb.DbBiers.Include(b => b.BrouwerNrNavigation).Include(b => b.SoortNrNavigation);
        //        foreach (DbBier dbBier in dbBieren)
        //        {
        //            BO_Bier bier = DbBierToBier(dbBier);
        //            bieren.Add(bier);
        //        }
        //    }
        //    return bieren;
        //}

        //private BO_Bier DbBierToBier(DbBier dbBier)
        //{
        //    if (dbBier == null) return null;
        //    BO_Bier bier = new BO_Bier()
        //    {
        //        BierNr = dbBier.BierNr,
        //        Naam = dbBier.Naam,
        //        Alcohol = dbBier.Alcohol,
        //        Brouwer = DbBrouwerToBrouwer(dbBier.BrouwerNrNavigation),
        //        BierSoort = DbSoortToBierSoort(dbBier.SoortNrNavigation)
        //    };
        //    return bier;
        //}

        public IList<BO_BierSoort> GeefAlleBierSoorten()
        {
            return DbSoortenToBierSoorten();
        }

        private IList<BO_BierSoort> DbSoortenToBierSoorten()
        {
            IList<BO_BierSoort> bierSoorten = new List<BO_BierSoort>();
            using(BierenDbContext db = new BierenDbContext())
            {
                var dbBierSoorten = db.DbSoorts;
                foreach (DbSoort dbSoort in dbBierSoorten)
                {
                    bierSoorten.Add(DbSoortToBierSoort(dbSoort));     
                }
            }
            return bierSoorten;
        }
        private BO_BierSoort DbSoortToBierSoort(DbSoort dbSoort)
        {
            if (dbSoort == null) return null;
            BO_BierSoort bierSoort = new BO_BierSoort()
            { 
                 SoortNr = dbSoort.SoortNr,
                 SoortNaam = dbSoort.Soort
            };
            return bierSoort;
        }

        public IList<BO_Brouwer> GeefAlleBrouwers()
        {
            return DbBrouwersToBrouwers();
        }

        private IList<BO_Brouwer> DbBrouwersToBrouwers()
        {
            IList<BO_Brouwer> brouwers = new List<BO_Brouwer>();
            using (BierenDbContext db = new BierenDbContext())
            {
                List<DbBrouwer> dbBrouwers = db.DbBrouwers.ToList();//.Include(b => b.DbBiers).ToList();
                foreach (DbBrouwer dbBrouwer in dbBrouwers)
                    brouwers.Add(DbBrouwerToBrouwer(dbBrouwer));
            }
            return brouwers;
        }

        private BO_Brouwer DbBrouwerToBrouwer(DbBrouwer dbBrouwer)
        {
            if (dbBrouwer == null) return null;
            BO_Brouwer brouwer = new BO_Brouwer()
            {
                 BrouwerNr = dbBrouwer.BrouwerNr,
                 BrNaam = dbBrouwer.BrNaam,
                 Straat = dbBrouwer.Adres,
                 PostCode = dbBrouwer.PostCode,
                 Gemeente = dbBrouwer.Gemeente,
                 Omzet = dbBrouwer.Omzet,
                
            };

            return brouwer;
        }

        public IList<BO_Bier> GeefBierenVoorBrouwer(BO_Brouwer brouwer)
        {
            IList<BO_Bier> bieren = new List<BO_Bier>();
            using (BierenDbContext db = new BierenDbContext())
            {
                var dbBieren = db.DbBiers.Where(b => b.BrouwerNr == brouwer.BrouwerNr).Include(b => b.BrouwerNrNavigation);
                foreach(DbBier dbBier in dbBieren)
                {
                    bieren.Add(_mapper.Map<BO_Bier>(dbBier));
                }
            }
            return bieren;
        }

        public IList<BO_Bier> VerwijderBier(BO_Bier bier)
        {
            throw new NotImplementedException();
        }

        public IList<BO_Brouwer> VerwijderBrouwer(BO_Brouwer selectedBrouwer)
        {
            throw new NotImplementedException();
        }

        public IList<BO_BierSoort> VoegBierSoortToe(BO_BierSoort biersoort)
        {
            using (BierenDbContext db = new BierenDbContext())
            {
                DbSoort dbBierSoort = new DbSoort() { Soort = biersoort.SoortNaam };
                db.DbSoorts.Add(dbBierSoort);
                db.SaveChanges();
               
            }
            return DbSoortenToBierSoorten();
        }

        public IList<BO_Bier> VoegBierToe(BO_Bier bier)
        {
            throw new NotImplementedException();
        }

        public IList<BO_Brouwer> VoegBrouwerToe(BO_Brouwer brouwer)
        {
            throw new NotImplementedException();
        }

        public void WijzigBier(BO_Bier bier)
        {
            throw new NotImplementedException();
        }

        public void WijzigBrouwer(BO_Brouwer selectedBrouwer)
        {
            throw new NotImplementedException();
        }

        public IList<BO_BierSoort> WijzigBierSoort(BO_BierSoort selectedSoort)
        {
            using (BierenDbContext db = new BierenDbContext())
            {
                DbSoort dbSoort = db.DbSoorts.Where(s => s.SoortNr == selectedSoort.SoortNr).FirstOrDefault();
                dbSoort.Soort = selectedSoort.SoortNaam;
                db.DbSoorts.Update(dbSoort);
                db.SaveChanges();
            }

            return DbSoortenToBierSoorten();
        }

        public IList<BO_BierSoort> VerwijderBierSoort(BO_BierSoort selectedSoort)
        {
            using (BierenDbContext db = new BierenDbContext())
            {
                var dbSoort = db.DbSoorts.Where(s => s.SoortNr == selectedSoort.SoortNr).FirstOrDefault();
                db.DbSoorts.Remove(dbSoort);
                db.SaveChanges();
            }

            return DbSoortenToBierSoorten();
        }

        public IList<BO_User> GeefAlleUsers()
        {
            IList<BO_User> users = new List<BO_User>();
            using (BierenDbContext db = new BierenDbContext())
            {
                List<DbUser> dbUsers = db.DbUsers.ToList();
                foreach(DbUser dbuser in dbUsers)
                {
                    BO_User user = new BO_User() //Er bestaat een populaire NuGet Package:  AutoMapper (niet altijd gemakkelijk om te configureren)
                    {
                         UserNr= dbuser.UserId,
                         Voornaam = dbuser.Voornaam,
                         Familienaam =dbuser.Familienaam,
                         Email = dbuser.Email,
                         GeboorteDatum = dbuser.GeboorteDatum 
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
