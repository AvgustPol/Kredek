using System.Threading.Tasks;

namespace B2CGraph
{
    public interface IB2CGraphClient
    {
        Task<string> GetAllUsers(string query);

        Task<string> SendGraphGetRequest(string api, string query);
    }
}