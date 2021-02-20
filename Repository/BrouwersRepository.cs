
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class BrouwersRepository : IBrouwersRepository
    {
        private readonly BierenDbContext _context;

        public BrouwersRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
        public IList<Brouwer> GetAll()
        {
            return _context.Brouwers.ToList();
        }
        public Brouwer Add(Brouwer brouwer)
        {
            Brouwer dbBrouwer = _context.Brouwers.Add(brouwer).Entity;
            SaveChanges();
            return dbBrouwer;
        }
        public Brouwer Update(Brouwer brouwer)
        {
            Brouwer dbBrouwer = FindById(brouwer.BrouwerNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBrouwer == null) throw new ArgumentNullException($"brouwer met BrouwerNr={brouwer.BrouwerNr} niet gevonden");

            _context.Brouwers.Update(brouwer);
            SaveChanges();
            return dbBrouwer;
        }
        public Brouwer Remove(Brouwer brouwer)
        {
            Brouwer dbBrouwer = FindById(brouwer.BrouwerNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBrouwer == null) throw new ArgumentNullException($"brouwer met BrouwerNr={brouwer.BrouwerNr} niet gevonden");

            _context.Brouwers.Remove(brouwer);
            SaveChanges();
            return dbBrouwer;
        }
        public Brouwer FindByName(string naam)
        {
            Brouwer dbBrouwer = _context.Brouwers.Where(e => e.BrNaam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbBrouwer;
        }

        public Brouwer FindById(int brouwerId)
        {
            Brouwer dbBrouwer = (Brouwer)_context.Brouwers.Find(brouwerId);

            return dbBrouwer;
        }

    }
}

