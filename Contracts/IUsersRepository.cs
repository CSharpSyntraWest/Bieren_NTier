using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IUsersRepository
    {
        User Add(User bier);
        User Remove(User user);
        User Update(User user);
        User FindById(int bierId);
        User FindByName(string familienaam);
        IList<User> GetAll();
    }
}