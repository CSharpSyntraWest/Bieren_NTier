using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IBierenRepository Bier { get; }
        IBrouwersRepository Brouwer { get; }
        IBierSoortenRepository BierSoort { get; }
        IUsersRepository User { get; }
        void Save();
    }
}
