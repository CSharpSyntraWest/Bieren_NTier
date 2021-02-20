
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class BierenRepository : IBierenRepository
    {
        private readonly BierenDbContext _context;

        public BierenRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IList<Bier> GetAll()
        { 
            var dbbieren=  _context.Bieren.Include(b => b.Soorten).Include(b =>b.Brouwers).ToList();
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
        public Bier Add(Bier bier)
        {
            Bier dbBier = _context.Bieren.Add(bier).Entity;
            SaveChanges();
            return dbBier;
        }

        public Bier Update(Bier bier)
        {
            Bier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");
      
            _context.Bieren.Update(bier);
            SaveChanges();
            return dbBier;
        }
        public Bier Remove(Bier bier)
        {
            Bier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");

            _context.Bieren.Remove(bier);
            SaveChanges();
            return dbBier;
        }
        public Bier FindByName(string naam)
        {
            Bier dbBier = _context.Bieren.Where(e => e.Naam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbBier;
        }

        public Bier FindById(int bierId)
        {
            Bier dbBier = (Bier)_context.Bieren.Find(bierId);

            return dbBier;
        }

        public IList<Bier> GetAllForBrewer(int Id)
        {
            return _context.Bieren.Where(e => e.BrouwerNr == Id).ToList();
        }
    }

}
