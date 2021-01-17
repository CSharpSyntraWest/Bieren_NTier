using Bieren.DataLayer.Models;
using System.Collections.Generic;

namespace Bieren.DataLayer.Repositories
{
    public interface IBierSoortenRepository
    {
        IList<DbSoort> GetAll();
        DbSoort Add(DbSoort biersoort);
        DbSoort Remove(DbSoort biersoort);
        DbSoort Update(DbSoort biersoort);
        DbSoort FindById(int Id);
        DbSoort FindByName(string naam);
    }
}