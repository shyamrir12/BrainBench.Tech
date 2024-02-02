using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace IntigrationWeb.Models.Utility
{
    public class HttpWebClients : IHttpWebClients
    {
      
        private IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        string jwt = "";

      
        public HttpWebClients(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string PostRequest(string URI, string parameterValues)
        { 
            
            //ShyamSir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            jwt = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
            //ShyamSir

            string BaseURI = _configuration.GetValue<string>("KeyList:WebApiurl");
            string URL = BaseURI + URI;
            string jsonString = null;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
          
            client.Timeout = TimeSpan.FromMinutes(30);
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            //GET Method  
            HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, c).Result;
            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync()
                                               .Result
                                               .Replace("\\", "")
                                               //.Replace("\r\n", "'")
                                               .Trim(new char[1] { '"' });
            }

            // }
            return jsonString;
        }

        public string GetRequest(string URI, object parameterValues)
        {
            //ShyamSir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            jwt = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
            //ShyamSir
            string BaseURI = _configuration.GetValue<string>("KeyList:WebApiurl"); ;

            string URL = BaseURI + URI;
            string jsonString = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                //GET Method  
                HttpResponseMessage response = client.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   .Replace("\\", "")
                                                   .Trim(new char[1] { '"' });

                }
            }
            return jsonString;
        }


        public async Task<string> AwaitPostRequest(string URI, string parameterValues)
        {
            //Shyam Sir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            jwt = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
            //Shyam Sir
            string BaseURI = _configuration.GetValue<string>("KeyList:WebApiurl");
            string URL = BaseURI + URI;
            string jsonString = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                //GET Method  
                HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, c).Result;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   //.Replace("\r\n", "'")
                                                   .Trim(new char[1] { '"' });
                }
            }
            return await Task.FromResult(jsonString);
        }


    }
}
