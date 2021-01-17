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
            var dbbieren=  _context.DbBiers.Include(b => b.SoortNrNavigation).Include(b =>b.BrouwerNrNavigation).ToList();
            return dbbieren;
        }
        private void SaveChanges()
        {
            _context.SaveChanges();
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
        public DbBier Add(DbBier bier)
        {
            DbBier dbBier = _context.DbBiers.Add(bier).Entity;
            SaveChanges();
            return dbBier;
        }

        public DbBier Update(DbBier bier)
        {
            DbBier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");
      
            _context.DbBiers.Update(bier);
            SaveChanges();
            return dbBier;
        }
        public DbBier Remove(DbBier bier)
        {
            DbBier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");

            _context.DbBiers.Remove(bier);
            SaveChanges();
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
