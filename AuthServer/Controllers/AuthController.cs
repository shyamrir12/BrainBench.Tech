
using AuthServer.Services;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Controllers
{
	[Route("api/[controller]/[action]")]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private IUserService _usercontext;
		private IOptions<Audience> _settings;
		public AuthController(IOptions<Audience> settings, IConfiguration configuration, IUserService usercontext)
		{
			this._settings = settings;
			this._configuration = configuration;
			this._usercontext = usercontext;
		}

		
		//Migrating to JWT Authorization...
		private string GenerateJwtToken(UserLoginSession user)
		{
			//getting the secret key
			string tokenTime = _configuration["Audience:TokenTime"];
			int _tokenTime = 20;
			//try converting token time  to minut (By Sunil)
			if (!string.IsNullOrEmpty(tokenTime))
			{
				try
				{
					_tokenTime = Convert.ToInt32(tokenTime);
				}
				catch { }
			}

			string secretKey = _configuration["Audience:Secret"];
			var key = Encoding.ASCII.GetBytes(secretKey);

			//create claims
			var claimEmail = new Claim(ClaimTypes.Email, user.EmailId);
			string UserSubUserid = "";
			if (user.subroleid!=null&&user.subroleid>0)
			{
				UserSubUserid = user.subroleid.ToString();
			}
			else
			{
				UserSubUserid = user.UserID.ToString();
			}
			var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, UserSubUserid);

			var claimRole = new Claim(ClaimTypes.Role, user.Role == null ? "" : user.Role);
			var UserName = new Claim(ClaimTypes.Name, user.Name == null ? "" : user.Name);

			//create claimsIdentity
			var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimRole, UserName }, "serverAuth");

			// generate token that is valid for 7 days
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Expires = DateTime.UtcNow.AddMinutes(_tokenTime),//.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			//creating a token handler
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			//returning the token back
			return tokenHandler.WriteToken(token);
		}

		[HttpPost]
		public async Task<ActionResult<AuthenticationResponse>> AuthenticateJWT(LoginEF authenticationRequest)
		{
			string token = string.Empty;
			//LoginEF authenticationRequest = new LoginEF();
			//authenticationRequest.UserName = Request.EmailId;
			//authenticationRequest.Password = Request.password;
			//checking if the user exists in the database
			// authenticationRequest.Password = MyUtility.Encrypt(authenticationRequest.Password);
			UserLoginSession loggedInUser = await _usercontext.Authenticate(authenticationRequest);
			if (loggedInUser != null)
			{
				if (loggedInUser.Name == "-1")
				{
					token = "";
				}
				else
				{
					//generating the token
					token = GenerateJwtToken(loggedInUser);
				}
			}
			else
			{
				token = "";
			}
			return await Task.FromResult(new AuthenticationResponse() { Token = token });
		}
		[HttpPost]
		public async Task<Result<UserLoginSession>> GetUserByJWT(AuthenticationResponse jwtToken)
		{
			Result<UserLoginSession> res = new Result<UserLoginSession>();
			double seconds = 0;
			try
			{

				//getting the secret key
				string secretKey = _configuration["Audience:Secret"];
				var key = Encoding.ASCII.GetBytes(secretKey);

				//preparing the validation parameters
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
				var tokenHandler = new JwtSecurityTokenHandler();
				SecurityToken securityToken;

				//validating the token
				var principle = tokenHandler.ValidateToken(jwtToken.Token, tokenValidationParameters, out securityToken);
				var jwtSecurityToken = (JwtSecurityToken)securityToken;

				DateTime validTo = jwtSecurityToken.Payload.ValidTo;
				DateTime currentUTC = DateTime.UtcNow;
				var diffInSeconds = (validTo - currentUTC).TotalSeconds;
				//if (diffInSeconds <= 0)
				//{
				//    context.HttpContext.Session.Clear();
				//    context.Result = new RedirectResult(_objKeyList.Value.LoginUrl);
				//}
				seconds = diffInSeconds;

				if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase) && seconds > 0)
				{
					//returning the user if found
					var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
					var UserType = principle.FindFirst(ClaimTypes.Role)?.Value;
					//var UserName = principle.FindFirst(ClaimTypes.UserData)?.Value;
					LoginEF model = new LoginEF();
					model.UserID = int.Parse(userId);
					model.UserType = UserType.ToString();
					//model.UserName = UserName.ToString();
					res.Data = await _usercontext.GetUserById(model);
					res.Status = true;
					res.Message = new System.Collections.Generic.List<string>() { "Success" };
				}
				else
				{
					res.Status = false;
					res.Message = new System.Collections.Generic.List<string>() { "Token Expired" };
					res.Data = null;
				}

			}
			catch (Exception ex)
			{
				//logging the error and returning null
				//Console.WriteLine("Exception : " + ex.Message);
				//return null;
				res.Status = false;
				res.Message = new System.Collections.Generic.List<string>() { "Exception : " + ex.Message };
				res.Data = null;
			}
			return res;
		}
	}
}
