
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBierSoortenRepository
    {
        IList<Soort> GetAll();
        Soort Add(Soort biersoort);
        Soort Remove(Soort biersoort);
        Soort Update(Soort biersoort);
        Soort FindById(int Id);
        Soort FindByName(string naam);
    }
}