using LoginModels;

namespace AdminPanelApp.Data.LoginServices
{
	public interface ILoginProvider
	{
		public Task<AuthenticationResponse> AuthenticateJWT(LoginEF model);
		public Task<Result<UserLoginSession>> GetUserByJWT(AuthenticationResponse model);
	}
}
