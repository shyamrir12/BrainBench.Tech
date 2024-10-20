using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.MailServices
{
	public interface IMailProvider
	{
		MessageEF SendCommonMail(CommonMail obj);
		List<GetUserAndEmail> GetUserAndEmail(GetUserAndEmail objRaiseTicket);
	}
}
