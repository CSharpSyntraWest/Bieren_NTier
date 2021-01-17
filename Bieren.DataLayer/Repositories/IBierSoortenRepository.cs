using Bieren.DataLayer.Models;
using System.Collections.Generic;

namespace Bieren.DataLayer.Repositories
{
    public interface IBierSoortenRepository
    {
        IList<DbSoort> GetAll();
        DbSoort Add(DbSoort biersoort);
        DbSoort FindById(int Id);
        DbSoort FindByName(string naam);
    }
}