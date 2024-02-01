using IntegrationModels;
using LoginModels;
using System.Net.Http.Json;

namespace IntigrationWeb.Models.UserAndErrorService
{
    public class UserAndErrorSubscriber : IUserAndErrorSubscriber
    {
        private HttpClient _httpClient;

        public UserAndErrorSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddExceptionData(LogEntry objLogEntry)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<LogEntry>($"ExceptionData/AddException", objLogEntry);

            return await httpMessageReponse.Content.ReadFromJsonAsync<string>();
        }

        public async Task<Result<UserLoginSession>> GetUserByJWT(AuthenticationResponse model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<AuthenticationResponse>($"auth/GetUserByJWT", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<UserLoginSession>>();
        }
    }
}
