using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Models.UserAndErrorService
{
    public interface IUserAndErrorSubscriber
    {
        public Task<string> AddExceptionData(LogEntry objLogEntry);
        public Task<Result<UserLoginSession>> GetUserByJWT(AuthenticationResponse model);
    }
}
