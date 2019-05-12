using System.Threading.Tasks;

namespace FacebookPageGetter.Services.FacebookClient
{
    public interface IFacebookClient : IService
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);

        Task PostAsync(string accessToken, string endpoint, object data, string args = null);
    }
}