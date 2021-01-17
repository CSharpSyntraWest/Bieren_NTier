using Bieren.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.DataLayer.Repositories
{
    public class BierenRepository : IBierenRepository
    {
        private readonly BierenDbContext _context;

        public BierenRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public BierenRepository()
        //{
        //    _context = new BierenDbContext();
        //}

        public IList<DbBier> GetAll()
        { 
            return  _context.DbBiers.Include(b => b.SoortNrNavigation).Include(b =>b.BrouwerNrNavigation).ToList();
        }
        public DbBier Add(DbBier bier)
        {
            DbBier dbBier = _context.DbBiers.Add(bier).Entity;
            _context.SaveChanges();
            return dbBier;
        }
        public DbBier Update(DbBier bier)
        {
            DbBier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");
      
            _context.DbBiers.Update(bier);
            _context.SaveChanges();
            return dbBier;
        }
        public DbBier Remove(DbBier bier)
        {
            DbBier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");

            _context.DbBiers.Remove(bier);
            _context.SaveChanges();
            return dbBier;
        }
        public DbBier FindByName(string naam)
        {
            DbBier dbBier = _context.DbBiers.Where(e => e.Naam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbBier;
        }

        public DbBier FindById(int bierId)
        {
            DbBier dbBier = (DbBier)_context.DbBiers.Find(bierId);

            return dbBier;
        }

        public IList<DbBier> GetAllForBrewer(int Id)
        {
            return _context.DbBiers.Where(e => e.BrouwerNr == Id).ToList();
        }
    }

}
