
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBierenRepository
    {
        IList<Bier> GetAll();
        Bier Add(Bier bier);
        Bier Remove(Bier bier);
        Bier Update(Bier bier);
        Bier FindById(int Id);
        Bier FindByName(string naam);
        IList<Bier> GetAllForBrewer(int Id);
    }
}