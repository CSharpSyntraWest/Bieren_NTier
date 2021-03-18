using System.Threading.Tasks;
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


        public async Task<IList<Bier>> GetAllAsync()
        { 
            var dbbieren=  await _context.Bieren.Include(b => b.Soorten).Include(b =>b.Brouwers).ToListAsync();
            return dbbieren;
        }
        private void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
        public async Task<Bier> AddAsync(Bier bier)
        {
            Bier dbBier = await _context.Bieren.AddAsync(bier);
            SaveChangesAsync();
            return dbBier;
        }

        public Bier Update(Bier bier)
        {
            Bier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");
      
            _context.Bieren.Update(bier);
            SaveChangesAsync();
            return dbBier;
        }
        public Bier Remove(Bier bier)
        {
            Bier dbBier = FindById(bier.BierNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBier == null) throw new ArgumentNullException($"bier met BierNr={bier.BierNr} niet gevonden");

            _context.Bieren.Remove(bier);
            SaveChangesAsync();
            return dbBier;
        }
        public Bier FindByName(string naam)
        {
            Bier dbBier = _context.Bieren.Where(e => e.Naam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbBier;
        }

        public Bier FindById(int bierId)
        {
            Bier dbBier = (Bier)_context.Bieren.FindAsync(bierId);

            return dbBier;
        }

        public async Task<IList<Bier>> GetAllForBrewerAsync(int Id)
        {
            return await _context.Bieren.Where(e => e.BrouwerNr == Id).ToListAsync();
        }
    }

}
