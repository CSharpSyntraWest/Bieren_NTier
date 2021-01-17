using Bieren.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bieren.DataLayer.Repositories
{
    public class GenericRepository<T> where T : DbBaseObject
    {
        private readonly BierenDbContext _context;

        public GenericRepository(BierenDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
                EntityEntry<T> createdResult = await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return createdResult.Entity;
        }

        public async Task<T> Update(int id, T entity)
        {

            entity.Id = id;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

             return entity;
            
        }

        public async Task<bool> Delete(int id)
        {
            T entity = await _context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<T> FindById(int id)
        {
            T result = await _context.Set<T>().FindAsync(id);

            return result;
        }

        public async Task<T> FindByName(string name)
        {
            T result = await _context.Set<T>().Where(e => e.Naam.ToLower() == name.ToLower()).SingleOrDefaultAsync();

            return result;
        }
    }
}
