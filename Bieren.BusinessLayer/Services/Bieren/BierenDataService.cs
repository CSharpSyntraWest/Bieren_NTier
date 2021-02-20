using AutoMapper;
using Bieren.BusinessLayer.Models;
using Bieren.DataLayer.Models;
using Bieren.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Bieren.BusinessLayer.Services
{
    public class BierenDataService : IDataService
    {
        private IBierenRepository _bierenRepository;
        private IBierSoortenRepository _bierSoortenRepository;
        private IBrouwersRepository _brouwersRepository;
        private IUsersRepository _usersRepository;
        private IMapper _mapper;

        public BierenDataService(IBierenRepository bierenRepository, IBierSoortenRepository bierSoortenRepository, IBrouwersRepository brouwersRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _bierenRepository = bierenRepository;
            _bierSoortenRepository = bierSoortenRepository;
            _brouwersRepository = brouwersRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public IList<BO_Bier> GeefAlleBieren()
        {
            return _mapper.Map<List<BO_Bier>>(_bierenRepository.GetAll());
        }


        public IList<BO_BierSoort> GeefAlleBierSoorten()
        {
            List<BO_BierSoort> boBiersooren = _mapper.Map<List<BO_BierSoort>>(_bierSoortenRepository.GetAll());
            return boBiersooren;
        }


        public IList<BO_Brouwer> GeefAlleBrouwers()
        {
            return _mapper.Map<List<BO_Brouwer>>(_brouwersRepository.GetAll());
        }


        public IList<BO_Bier> GeefBierenVoorBrouwer(BO_Brouwer brouwer)
        {
            List<DbBier> dbBierenVoorBrouwer = (List<DbBier>)_bierenRepository.GetAllForBrewer(brouwer.BrouwerNr);
            return _mapper.Map<List<BO_Bier>>(dbBierenVoorBrouwer);
        }

        public IList<BO_Bier> VerwijderBier(BO_Bier bier)
        {
            DbBier dbbier = _bierenRepository.Remove(_mapper.Map<DbBier>(bier));
            return GeefAlleBieren();
        }

        public IList<BO_Brouwer> VerwijderBrouwer(BO_Brouwer brouwer)
        {
            DbBrouwer dbbrouwer = _brouwersRepository.Remove(_mapper.Map<DbBrouwer>(brouwer));
            return GeefAlleBrouwers();
        }

        public IList<BO_BierSoort> VoegBierSoortToe(BO_BierSoort biersoort)
        {
            biersoort.SoortNr = 0;
            DbSoort dbBierSoort = _bierSoortenRepository.Add(_mapper.Map<DbSoort>(biersoort));
            return GeefAlleBierSoorten();
        }

        public IList<BO_Bier> VoegBierToe(BO_Bier bier)
        {
            //bier.BierNr = 0;
            DbBier dbBier = _bierenRepository.Add(_mapper.Map<DbBier>(bier));
            return GeefAlleBieren();
        }

        public IList<BO_Brouwer> VoegBrouwerToe(BO_Brouwer brouwer)
        {
            brouwer.BrouwerNr = 0;
            DbBrouwer dbBrouwer = _brouwersRepository.Add(_mapper.Map<DbBrouwer>(brouwer));
            return GeefAlleBrouwers();
        }

        public IList<BO_Bier> WijzigBier(BO_Bier bier)
        {
            DbBier DbBier = _bierenRepository.Update(_mapper.Map<DbBier>(bier));
            return GeefAlleBieren();
        }

        public IList<BO_Brouwer> WijzigBrouwer(BO_Brouwer brouwer)
        {
            DbBrouwer dbBrouwer = _brouwersRepository.Update(_mapper.Map<DbBrouwer>(brouwer));
            return GeefAlleBrouwers();
        }

        public IList<BO_BierSoort> WijzigBierSoort(BO_BierSoort biersoort)
        {
            DbSoort dbBierSoort = _bierSoortenRepository.Update(_mapper.Map<DbSoort>(biersoort));
            return GeefAlleBierSoorten();
        }

        public IList<BO_BierSoort> VerwijderBierSoort(BO_BierSoort biersoort)
        {
            DbSoort dbBierSoort = _bierSoortenRepository.Remove(_mapper.Map<DbSoort>(biersoort));

            return GeefAlleBierSoorten();
        }

        public IList<BO_User> GeefAlleUsers()
        {
            return _mapper.Map<List<BO_User>>(_usersRepository.GetAll());
 
        }
    }
}
