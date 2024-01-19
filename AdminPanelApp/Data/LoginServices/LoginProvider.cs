using LoginModels;
using System.Net.Http.Json;


namespace AdminPanelApp.Data.LoginServices
{
	public class LoginProvider : ILoginProvider
	{
		private HttpClient _httpClient;

		public LoginProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<AuthenticationResponse> AuthenticateJWT(LoginEF model)
		{

			var httpMessageReponse = await _httpClient.PostAsJsonAsync<LoginEF>($"auth/authenticatejwt", model);

			return await  httpMessageReponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
		
		}

		public async Task<Result<UserLoginSession>> GetUserByJWT(AuthenticationResponse model)
		{
			var httpMessageReponse = await _httpClient.PostAsJsonAsync<AuthenticationResponse>($"auth/GetUserByJWT", model);

			return await httpMessageReponse.Content.ReadFromJsonAsync<Result<UserLoginSession>>();
		}

		
	}
}
