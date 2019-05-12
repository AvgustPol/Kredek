using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace B2CGraph
{
    public class B2CGraphClient : IB2CGraphClient
    {
        private readonly AuthenticationContext _authContext;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly ClientCredential _credential;
        private readonly string _tenant;

        public B2CGraphClient(string clientId, string clientSecret, string tenant)
        {
            // The client_id, client_secret, and tenant are pulled in from the App.config file
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tenant = tenant;

            // The AuthenticationContext is ADAL's primary class, in which you indicate the direcotry to use.
            _authContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenant);

            // The ClientCredential is where you pass in your client_id and client_secret, which are
            // provided to Azure AD in order to receive an access_token using the app's identity.
            _credential = new ClientCredential(clientId, clientSecret);
        }

        public async Task<string> GetAllGroups(string query)
        {
            return await SendGraphGetRequest("/groups", query);
        }

        public async Task<string> GetAllUsers(string query)
        {
            return await SendGraphGetRequest("/users", query);
        }

        public async Task<string> GetUserByObjectId(string objectId)
        {
            return await SendGraphGetRequest("/users/" + objectId, null);
        }

        public async Task<string> SendGraphGetRequest(string api, string query)
        {
            string resource = Globals.ResourceUrl;

            // First, use ADAL to acquire a token using the app's identity (the credential)
            // The first parameter is the resource we want an access_token for; in this case, the Graph API.
            var result = _authContext.AcquireTokenAsync(resource, _credential).Result;

            // For B2C user managment, be sure to use the 1.6 Graph API version.
            HttpClient http = new HttpClient();
            string url = resource + _tenant + api + "?" + Globals.aadGraphVersion;
            if (!string.IsNullOrEmpty(query))
            {
                url += "&" + query;
            }

            // Append the access token for the Graph API to the Authorization header of the request, using the Bearer scheme.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                object formatted = JsonConvert.DeserializeObject(error);
                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}