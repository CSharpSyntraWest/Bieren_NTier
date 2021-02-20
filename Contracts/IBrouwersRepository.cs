using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBrouwersRepository
    {
        Brouwer Add(Brouwer brouwer);
        Brouwer Remove(Brouwer brouwer);
        Brouwer Update(Brouwer brouwer);
        Brouwer FindById(int brouwerId);
        Brouwer FindByName(string naam);
        IList<Brouwer> GetAll();
    }
}