using System.Threading.Tasks;

namespace PayPalTest.Service
{
    public interface IHttpService
    {
        Task<string> Post();
        Task<string> GetAccessToken();
    }
}
