using AuthServer.Models;

namespace AuthServer.Services
{
	public interface IUserService
	{
		Task<UserLoginSession> Authenticate(LoginEF model);
		Task<UserLoginSession> GetUserById(LoginEF model);
	}
}
