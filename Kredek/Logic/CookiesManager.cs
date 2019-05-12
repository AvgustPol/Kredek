using Kredek.Global;
using Microsoft.AspNetCore.Http;
using System;

namespace Kredek.Logic
{
    public class CookiesManager : ICookiesManager
    {
        private readonly int _expireTime = DefaultVariables.CookieLifeTime;

        /// <summary>
        /// set the cookie for the HttpResponse
        /// </summary>
        /// <param name="response">HttpResponse</param>
        /// <param name="key">key (unique indentifier)</param>
        /// <param name="value">value to store in cookie object</param>
        /// <param name="expireTime">expiration time</param>
        public void Set(HttpResponse response, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(_expireTime);

            response.Cookies.Append(key, value, option);
        }
    }
}