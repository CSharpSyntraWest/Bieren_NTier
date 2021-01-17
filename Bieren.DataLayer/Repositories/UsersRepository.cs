﻿using Bieren.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.DataLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BierenDbContext _context;

        public UsersRepository(BierenDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IList<DbUser> GetAll()
        {
            return _context.DbUsers.ToList();
        }
        public DbUser Add(DbUser bier)
        {
            DbUser dbUser = _context.DbUsers.Add(bier).Entity;
            _context.SaveChanges();
            return dbUser;
        }
        public DbUser Update(DbUser user)
        {
            DbUser dbUser = FindById(user.UserId);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbUser == null) throw new ArgumentNullException($"user met UserId={user.UserId} niet gevonden");

            _context.DbUsers.Update(user);
            _context.SaveChanges();
            return dbUser;
        }
        public DbUser Remove(DbUser user)
        {
            DbUser dbUser = FindById(user.UserId);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbUser == null) throw new ArgumentNullException($"user met UserId={user.UserId} niet gevonden");

            _context.DbUsers.Remove(user);
            _context.SaveChanges();
            return dbUser;
        }
        public DbUser FindByName(string familienaam)
        {
            DbUser dbUser = _context.DbUsers.Where(e => e.Familienaam.ToLower() == familienaam.ToLower()).FirstOrDefault();
            return dbUser;
        }

        public DbUser FindById(int bierId)
        {
            DbUser dbUser = (DbUser)_context.DbUsers.Find(bierId);

            return dbUser;
        }

    }
}

