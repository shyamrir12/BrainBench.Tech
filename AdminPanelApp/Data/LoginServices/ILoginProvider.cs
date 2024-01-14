using LoginModels;

namespace AdminPanelApp.Data.LoginServices
{
	public interface ILoginProvider
	{
		public Task<AuthenticationResponse> AuthenticateJWT(LoginEF model);
		public Result<UserLoginSession> GetUserByJWT(AuthenticationResponse model);
	}
}
