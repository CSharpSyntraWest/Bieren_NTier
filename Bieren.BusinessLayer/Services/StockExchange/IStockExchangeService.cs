using Bieren.BusinessLayer.Models;
using System.Threading.Tasks;

namespace Bieren.BusinessLayer.Services
{
    public interface IStockExchangeService
    {
        Task<BO_Aandeel> GeefAandeelInfo(string aandeelTicker);
    }
}