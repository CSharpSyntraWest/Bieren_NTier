
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class BierSoortenRepository : IBierSoortenRepository
    {
        private readonly BierenDbContext _context;

        public BierSoortenRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IList<Soort> GetAll()
        { 
            return  _context.Soorten.ToList();
        }
        public Soort Add(Soort soort)
        {
            Soort dbBierSoort = _context.Soorten.Add(soort).Entity;
            SaveChanges();
            return dbBierSoort;
        }
        public Soort Update(Soort soort)
        {
            Soort dbSoort = FindById(soort.SoortNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbSoort == null) throw new ArgumentNullException($"biersoort met SoortNr={soort.SoortNr} niet gevonden");
      
            _context.Soorten.Update(soort);
            SaveChanges();
            return dbSoort;
        }
        public Soort Remove(Soort soort)
        {
            Soort dbSoort = FindById(soort.SoortNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbSoort == null) throw new ArgumentNullException($"soort bier met SoortNr={soort.SoortNr} niet gevonden");

            _context.Soorten.Remove(soort);
            SaveChanges();
            return dbSoort;
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        public Soort FindByName(string naam)
        {
            Soort dbSoort = _context.Soorten.AsNoTracking().Where(e => e.SoortNaam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbSoort;
        }

        public Soort FindById(int Id)
        {
            Soort dbSoort = (Soort)_context.Soorten.Find(Id);

            return dbSoort;
        }

    }

}
