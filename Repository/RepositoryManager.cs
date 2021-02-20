using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private BierenDbContext _repositoryContext;
        private IBierenRepository _bierenRepository;
        private IBrouwersRepository _brouwersRepository;
        private IBierSoortenRepository _biersoortenRepository;
        private IUsersRepository _usersRepository;

        public RepositoryManager(BierenDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IBierenRepository Bier
        {
            get
            {
                if (_bierenRepository == null)
                {
                    _bierenRepository = new BierenRepository(_repositoryContext);
                }
                return _bierenRepository;
            }
        }

        public IBrouwersRepository Brouwer
        {
            get
            {
                if (_brouwersRepository == null)
                {
                    _brouwersRepository = new BrouwersRepository(_repositoryContext);
                }
                return _brouwersRepository;
            }

        }

        public IBierSoortenRepository BierSoort 
        {
            get
            {
                if (_biersoortenRepository == null)
                {
                    _biersoortenRepository = new BierSoortenRepository(_repositoryContext);
                }
                return _biersoortenRepository;
            }
        }

        public IUsersRepository User 
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UsersRepository(_repositoryContext);
                }
                return _usersRepository;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
