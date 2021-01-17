using Bieren.DataLayer.Models;
using System.Collections.Generic;

namespace Bieren.DataLayer.Repositories
{
    public interface IUsersRepository
    {
        DbUser Add(DbUser bier);
        DbUser Remove(DbUser user);
        DbUser Update(DbUser user);
        DbUser FindById(int bierId);
        DbUser FindByName(string familienaam);
        IList<DbUser> GetAll();
    }
}