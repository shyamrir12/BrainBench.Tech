using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace IntigrationWeb.Models.Utility
{
    public class HttpWebClients : IHttpWebClients
    {
      
        private IConfiguration _configuration;
        public HttpWebClients(IConfiguration configuration)
        {
           
            _configuration = configuration;
        }

        public string PostRequest(string URI, string parameterValues)
        {
          
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
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
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
            string BaseURI = _configuration.GetValue<string>("KeyList:WebApiurl"); ;

            string URL = BaseURI + URI;
            string jsonString = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZURTZWN1cml0eTplX0RfQVBJLXVyaQ==");
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

    }
}
