using AdminPanelApp.Data.UserSessionIndexDB;

using Blazored.LocalStorage;
using LoginModels;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;
using static MudBlazor.Defaults;


namespace AdminPanelApp.Handlers
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorageService;
		private readonly UserDb _usermanager;

        public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService,UserDb usermanager)
		{
			_httpClient = httpClient;
			_localStorageService = localStorageService;
			_usermanager = usermanager;
          

        }
		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			UserLoginSession currentUser = await GetUserByJWTAsync(); //_httpClient.GetFromJsonAsync<User>("user/getcurrentuser");
           
            if (currentUser != null && currentUser.EmailId != null)
			{
				//create a claims
				var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailId);
				var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserID));
				var claimRole = new Claim(ClaimTypes.Role, currentUser.Role == null ? "" : currentUser.Role);
				//create claimsIdentity
				var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimRole }, "serverAuth");
				//create claimsPrincipal
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				return new AuthenticationState(claimsPrincipal);
			}
			else
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}
		public async Task<UserLoginSession> GetUserByJWTAsync()
		{
            //pulling the token from localStorage
            var jwtToken = await _localStorageService.GetItemAsStringAsync("jwt_token");
			if (jwtToken == null)
			{
				return new UserLoginSession();
			}
			else
			{
                //preparing the http request
                var list = await _usermanager.GetAllAsync();
                if (list.Any())
                {

                    var obj = JsonConvert.DeserializeObject<Result<UserLoginSession>>(list.FirstOrDefault().Email);
                    UserLoginSession d = obj.Data;
                    return  d;
                }
                else
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "auth/GetUserByJWT");
                    requestMessage.Content = new StringContent(jwtToken);

                    requestMessage.Content.Headers.ContentType
                        = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    //making the http request
                    var response = await _httpClient.SendAsync(requestMessage);

                    var responseStatusCode = response.StatusCode;
                    var returnedUser = await response.Content.ReadFromJsonAsync<Result<UserLoginSession>>();

                    //returning the user if found
                    if (returnedUser != null)
                    {
                        await AddCustomers(response);
                        return await Task.FromResult(returnedUser.Data);

                    }

                    else
                    {
                        return new UserLoginSession();
                    }
                }

                

                
            }
           


			
		}

        private async Task AddCustomers(HttpResponseMessage response)
        {
            await _usermanager.DeleteAllAsync();
            var responseString = await response.Content.ReadAsStringAsync();
          
            await _usermanager.InsertAsync(new Customer
            {
                Id = 1,
                Name = "Data",
                Email = responseString.ToString(),
            });

            
        }

        
    }
		

	}

