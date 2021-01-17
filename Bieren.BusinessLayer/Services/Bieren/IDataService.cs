using Bieren.BusinessLayer.Models;
using System.Collections.Generic;

namespace Bieren.BusinessLayer.Services
{
    public interface IDataService
    {
        IList<BO_Bier> GeefAlleBieren();
        IList<BO_BierSoort> GeefAlleBierSoorten();
        IList<BO_Brouwer> GeefAlleBrouwers();
        IList<BO_User> GeefAlleUsers();
        IList<BO_Bier> GeefBierenVoorBrouwer(BO_Brouwer brouwer);
        IList<BO_Bier> VerwijderBier(BO_Bier bier);
        IList<BO_BierSoort> VerwijderBierSoort(BO_BierSoort biersoort);
        IList<BO_Brouwer> VerwijderBrouwer(BO_Brouwer brouwer);
        IList<BO_BierSoort> VoegBierSoortToe(BO_BierSoort biersoort);
        IList<BO_Bier> VoegBierToe(BO_Bier bier);
        IList<BO_Brouwer> VoegBrouwerToe(BO_Brouwer brouwer);
        IList<BO_Bier> WijzigBier(BO_Bier bier);
        IList<BO_BierSoort> WijzigBierSoort(BO_BierSoort biersoort);
        IList<BO_Brouwer> WijzigBrouwer(BO_Brouwer brouwer);
    }
}