using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace IntegrationApi.Data.MailServices
{
	public class MailProvider : RepositoryBase, IMailProvider
	{
		
		private readonly IConfiguration configuration;

		public MailProvider(IConnectionFactory connectionFactory, IConfiguration configuration) : base(connectionFactory)
		{
			
			this.configuration = configuration;
		}
		private SmtpClient CreateMailClient()
		{
			SmtpClient client = new SmtpClient();

			// Configure client.

			return client;
		}
		public MessageEF SendCommonMail(CommonMail obj)
		{
			MessageEF messageEF = new MessageEF();
			try
			{
				string body = null;
				if (obj.ParameterList != null && obj.ParameterList.Count > 0 && obj.TemplateID != null)
				{
					List<SMSTemplate> lst = GetAllApprovedSMSTemplate(new SMSTemplate() { TemplateID = obj.TemplateID });
					if ((obj.ParameterList != null && obj.ParameterList.Count > 0) && (lst != null && lst.Count > 0))
					{
						if (lst != null && lst.Count > 0)
						{
							obj.Body = TemplateBuilder.BuildEmailTemplate(lst[0].EmailTemplate, obj.ParameterList);
							body = obj.Body;
							if (obj.Subject == null)
							{
								obj.Subject = "Khanij Online";
							}
						}
					}
				}
				else
				{
					// body = GenerateBody(obj.Salutation, obj.Body);
					body = obj.Body;
					obj.Subject = "Khanij Online";
				}
				CommonMailSend(obj.To, obj.Subject, body);
				messageEF.Satus = "success";

			}
			catch (Exception)
			{
				messageEF.Satus = "success";
			}
			return messageEF;
		}
		public List<SMSTemplate> GetAllApprovedSMSTemplate(SMSTemplate sMS)
		{
			List<SMSTemplate> objLst = new List<SMSTemplate>();
			try
			{

				DynamicParameters param = new DynamicParameters();

				var paramList = new
				{
					Check = 1,
					TemplateID = sMS.TemplateID
				};
				var result = Connection.Query<SMSTemplate>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);
				objLst = result.ToList();
			}

			catch (Exception ex)
			{
				throw ex;
			}
			return objLst;
		}

		public async Task CommonMailSend(string To, string Subject, string Body)
		{
			try
			{
				var p = new DynamicParameters();
				var dr = Connection.ExecuteScalar("select top 1 EMAIL_SENT from EXPIRY_DATE_ALERT", p, commandType: System.Data.CommandType.Text);
				if (Convert.ToBoolean(dr) == true)
				{
					MailMessage mail = new MailMessage();
					string[] ToMailIDs = To.Split(',');
					foreach (string emailid in ToMailIDs)
					{
						mail.To.Add(emailid);
					}
					mail.From = new MailAddress(configuration.GetSection("MailConfiguration").GetValue<string>("smtpEmail"),
						configuration.GetSection("MailConfiguration").GetValue<string>("smtpAliase"));

					mail.Subject = Subject;
					mail.SubjectEncoding = System.Text.Encoding.UTF8;
					mail.Body = Body;
					mail.BodyEncoding = System.Text.Encoding.UTF8;
					mail.IsBodyHtml = true;
					mail.Priority = System.Net.Mail.MailPriority.High;
					using (SmtpClient client = CreateMailClient())
					{
                       
                        client.Port = Convert.ToInt32(configuration.GetSection("MailConfiguration").GetValue<int>("smtpPort"));
						client.Host = Convert.ToString(configuration.GetSection("MailConfiguration").GetValue<string>("smtpHost"));
						client.Credentials = new System.Net.NetworkCredential(configuration.GetSection("MailConfiguration").GetValue<string>("smtpLoginId").ToString(),
							configuration.GetSection("MailConfiguration").GetValue<string>("smtpPassword"));
						client.EnableSsl = Convert.ToBoolean(configuration.GetSection("MailConfiguration").GetValue<bool>("SSLRequire"));
						await client.SendMailAsync(mail);

					}
				}

			}
			catch (Exception ex)
			{
				var paramList = new
				{
					Area = "MailService.cs",
					Controller = "MailService.cs",
					Action = "MailService.cs",
					ReturnType = ex.InnerException.Message,
					ErrorMessage = ex.Message,
					StackTrace = ex.StackTrace,
					UserId = 0,
					UserLoginID = 0
				};
				var result = Connection.Execute("ReportErrorLog", paramList, commandType: System.Data.CommandType.StoredProcedure);

			}
		}
		//sp not created
		public List<GetUserAndEmail> GetUserAndEmail(GetUserAndEmail objRaiseTicket)
		{
			{
				List<GetUserAndEmail> list = new List<GetUserAndEmail>();
				try
				{
					var paramList = new
					{
						CommaUserStr = objRaiseTicket.commaSeparatedVal,
						Check = objRaiseTicket.Check,
						ForwardToStr = objRaiseTicket.ForwardToStr
					};

					var Output = Connection.Query<GetUserAndEmail>("Usp_GetUserAndEmail", paramList, commandType: System.Data.CommandType.StoredProcedure);
					if (Output.Count() > 0)
					{
						list = Output.ToList();
					}
					return list;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
	}
}
