using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Models.UserAndErrorService
{
    public interface IUserAndErrorSubscriber
    {
        public string AddExceptionData(LogEntry objLogEntry);
        public Result<UserLoginSession> GetUserByJWT(AuthenticationResponse model);
    }
}
