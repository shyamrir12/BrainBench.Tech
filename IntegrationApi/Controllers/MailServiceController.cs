using IntegrationApi.Data.MailServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/[controller]/{action}")]
	[ApiController]
	public class MailServiceController : ControllerBase
	{
		private readonly IMailProvider mailProvider;

		public MailServiceController(IMailProvider mailProvider)
		{
			this.mailProvider = mailProvider;
		}
		[HttpPost]
		public MessageEF SendCommonMail(CommonMail obj)
		{
			return mailProvider.SendCommonMail(obj);
		}
		[HttpPost]
		public List<GetUserAndEmail> GetUserAndEmail(GetUserAndEmail obj)
		{
			return mailProvider.GetUserAndEmail(obj);
		}
	}
}
