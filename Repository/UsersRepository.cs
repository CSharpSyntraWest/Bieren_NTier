
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BierenDbContext _context;

        public UsersRepository(BierenDbContext context)
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
        public IList<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User Add(User bier)
        {
            User dbUser = _context.Users.Add(bier).Entity;
            SaveChanges();
            return dbUser;
        }
        public User Update(User user)
        {
            User dbUser = FindById(user.UserId);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbUser == null) throw new ArgumentNullException($"user met UserId={user.UserId} niet gevonden");

            _context.Users.Update(user);
            SaveChanges();
            return dbUser;
        }
        public User Remove(User user)
        {
            User dbUser = FindById(user.UserId);
            //foutmelding geven indien het id niet is teruggevonden
            if (dbUser == null) throw new ArgumentNullException($"user met UserId={user.UserId} niet gevonden");

            _context.Users.Remove(user);
            SaveChanges();
            return dbUser;
        }
        public User FindByName(string familienaam)
        {
            User dbUser = _context.Users.Where(e => e.Familienaam.ToLower() == familienaam.ToLower()).FirstOrDefault();
            return dbUser;
        }

        public User FindById(int bierId)
        {
            User dbUser = (User)_context.Users.Find(bierId);

            return dbUser;
        }

    }
}

