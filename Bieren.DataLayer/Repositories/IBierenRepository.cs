using Bieren.DataLayer.Models;
using System.Collections.Generic;

namespace Bieren.DataLayer.Repositories
{
    public interface IBierenRepository
    {
        IList<DbBier> GetAll();
        DbBier Add(DbBier bier);
        DbBier FindById(int Id);
        DbBier FindByName(string naam);
    }
}