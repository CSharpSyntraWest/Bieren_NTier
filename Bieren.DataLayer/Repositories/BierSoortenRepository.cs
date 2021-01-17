using Bieren.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.DataLayer.Repositories
{
    public class BierSoortenRepository : IBierSoortenRepository
    {
        private readonly BierenDbContext _context;

        public BierSoortenRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public BierSoortenRepository()
        {
            _context = new BierenDbContext();
        }

        public IList<DbSoort> GetAll()
        { 
            return  _context.DbSoorts.ToList();
        }
        public DbSoort Add(DbSoort soort)
        {
            DbSoort dbBierSoort = _context.DbSoorts.Add(soort).Entity;
            _context.SaveChanges();
            return dbBierSoort;
        }
        public DbSoort Update(DbSoort soort)
        {
            DbSoort dbSoort = FindById(soort.SoortNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbSoort == null) throw new ArgumentNullException($"biersoort met SoortNr={soort.SoortNr} niet gevonden");
      
            _context.DbSoorts.Update(soort);
            _context.SaveChanges();
            return dbSoort;
        }
        public DbSoort Remove(DbSoort soort)
        {
            DbSoort dbSoort = FindById(soort.SoortNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbSoort == null) throw new ArgumentNullException($"soort bier met SoortNr={soort.SoortNr} niet gevonden");

            _context.DbSoorts.Remove(soort);
            _context.SaveChanges();
            return dbSoort;
        }
        public DbSoort FindByName(string naam)
        {
            DbSoort dbSoort = _context.DbSoorts.Where(e => e.Soort.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbSoort;
        }

        public DbSoort FindById(int Id)
        {
            DbSoort dbSoort = (DbSoort)_context.DbSoorts.Find(Id);

            return dbSoort;
        }

    }

}
