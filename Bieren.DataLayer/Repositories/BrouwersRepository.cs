﻿using Bieren.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.DataLayer.Repositories
{
    public class BrouwersRepository : IBrouwersRepository
    {
        private readonly BierenDbContext _context;

        public BrouwersRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public BrouwersRepository()
        //{
        //    _context = new BierenDbContext();
        //}

        public IList<DbBrouwer> GetAll()
        {
            return _context.DbBrouwers.ToList();
        }
        public DbBrouwer Add(DbBrouwer brouwer)
        {
            DbBrouwer dbBrouwer = _context.DbBrouwers.Add(brouwer).Entity;
            _context.SaveChanges();
            return dbBrouwer;
        }
        public DbBrouwer Update(DbBrouwer brouwer)
        {
            DbBrouwer dbBrouwer = FindById(brouwer.BrouwerNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBrouwer == null) throw new ArgumentNullException($"brouwer met BrouwerNr={brouwer.BrouwerNr} niet gevonden");

            _context.DbBrouwers.Update(brouwer);
            _context.SaveChanges();
            return dbBrouwer;
        }
        public DbBrouwer Remove(DbBrouwer brouwer)
        {
            DbBrouwer dbBrouwer = FindById(brouwer.BrouwerNr);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbBrouwer == null) throw new ArgumentNullException($"brouwer met BrouwerNr={brouwer.BrouwerNr} niet gevonden");

            _context.DbBrouwers.Remove(brouwer);
            _context.SaveChanges();
            return dbBrouwer;
        }
        public DbBrouwer FindByName(string naam)
        {
            DbBrouwer dbBrouwer = _context.DbBrouwers.Where(e => e.BrNaam.ToLower() == naam.ToLower()).FirstOrDefault();
            return dbBrouwer;
        }

        public DbBrouwer FindById(int brouwerId)
        {
            DbBrouwer dbBrouwer = (DbBrouwer)_context.DbBrouwers.Find(brouwerId);

            return dbBrouwer;
        }

    }
}

