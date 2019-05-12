using Microsoft.AspNetCore.Http;

namespace Kredek.Logic
{
    public interface ICookiesManager
    {
        void Set(HttpResponse response, string key, string value, int? expireTime);
    }
}