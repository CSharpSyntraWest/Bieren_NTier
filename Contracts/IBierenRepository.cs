
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBierenRepository
    {
        Task<IList<Bier>> GetAllAsync();
        Task<Bier> AddAsync(Bier bier);
        Bier Remove(Bier bier);
        Bier Update(Bier bier);
        Bier FindById(int Id);
        Bier FindByName(string naam);
        Task<IList<Bier>> GetAllForBrewerAsync(int Id);
    }
}