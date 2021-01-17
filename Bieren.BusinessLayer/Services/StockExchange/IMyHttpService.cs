using System.Threading.Tasks;

namespace Bieren.BusinessLayer.Services
{
    public interface IMyHttpService
    {
        Task<string> Get(string url);
    }
}