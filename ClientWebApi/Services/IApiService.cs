using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.Services
{
    public interface IApiService
    {
        Task<string> Call(string Uri, string bodyString, Encoding encoding, string mediaType);
    }
}
