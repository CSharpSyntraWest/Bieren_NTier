using Bieren.DataLayer.Models;
using System.Collections.Generic;

namespace Bieren.DataLayer.Repositories
{
    public interface IBrouwersRepository
    {
        DbBrouwer Add(DbBrouwer brouwer);
        DbBrouwer Remove(DbBrouwer brouwer);
        DbBrouwer Update(DbBrouwer brouwer);
        DbBrouwer FindById(int brouwerId);
        DbBrouwer FindByName(string naam);
        IList<DbBrouwer> GetAll();
    }
}