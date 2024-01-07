using Dapper;
using IntegrationApi.Data.MailServices;
using IntegrationApi.Data.SMSServices;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using System.Data;

namespace IntegrationApi.Data.SchedulerMailSMSServices
{
	public class SchedulerMailSMSProvider : RepositoryBase, ISchedulerMailSMSProvider
	{
		private readonly ISMSProvider sMSProvider;
		private readonly IMailProvider mailProvider;
		public SchedulerMailSMSProvider(ISMSProvider sMSProvider, IMailProvider mailProvider, IConnectionFactory connectionFactory) : base(connectionFactory)
		{
			this.sMSProvider = sMSProvider;
			this.mailProvider = mailProvider;
		}
		//start manually first time
		//http://localhost/IntegrationApi/api/SchedulerEmailSMS/SendEmailSMS
		public void SendEmailSMS(string backGroundJobType, string startTime)

		{

			var ListChecklist = SelectList();

			foreach (var item in ListChecklist)
			{
				if (item.IsEmailSend == 0)
				{
					CommonMail obj = new CommonMail();
					MessageEF rep = new MessageEF();
					obj.TemplateID = item.TemplateID;
					obj.Subject = "Khanij " + item.SchedulerFor + "  Reminder 1";
					obj.ParameterList = item.ParameterList.Split(',').ToList();
					obj.To = item.EmailId;
					rep = mailProvider.SendCommonMail(obj);
					int milliseconds = 5000;// 5 second 
					Thread.Sleep(milliseconds);
					if (rep.Satus == "success")
					{
						UpdateStatus(item.SchedulerId, "Email");
					}
				}

				if (item.IsSMSSend == 0)
				{

					SMS sMS = new SMS();
					MessageEF rep = new MessageEF();
					sMS.mobileNo = item.MobileNo;
					sMS.templateid = item.TemplateID;
					sMS.parameterlist = item.ParameterList.Split(',').ToList(); ;
					rep = sMSProvider.Main(sMS);
					int milliseconds = 5000;// 5 second 
					Thread.Sleep(milliseconds);
					if (rep.Satus == "success")
					{
						UpdateStatus(item.SchedulerId, "SMS");
					}
				}



			}



			// Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
		}

		public List<EmailSMSScheduler> SelectList()
		{
			List<EmailSMSScheduler> ListChecklist = new List<EmailSMSScheduler>();
			try
			{
				var paramList = new
				{
					P_VCH_ACTION = "SELECT",
					//P_INT_SHEDULE_ID = 1,

				};
				var result = Connection.Query<EmailSMSScheduler>("USP_EmailSMSScheduler", paramList, commandType: System.Data.CommandType.StoredProcedure);
				if (result.Count() > 0)
				{
					ListChecklist = result.ToList();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return ListChecklist;
		}
		public void UpdateStatus(int SHEDULE_ID, string type)
		{
			try
			{
				var p = new DynamicParameters();
				if (type == "SMS")
				{
					p.Add("P_VCH_ACTION", "UPDATESMS");
				}
				else if (type == "Email")
				{
					p.Add("P_VCH_ACTION", "UPDATEEmail");
				}


				p.Add("P_INT_SHEDULE_ID", SHEDULE_ID);

				p.Add("P_INT_RESULT", dbType: DbType.Int32, direction: ParameterDirection.Output);

				var result = Connection.Query<int>("USP_EmailSMSScheduler", p, commandType: System.Data.CommandType.StoredProcedure);
				int newID = p.Get<int>("P_INT_RESULT");


			}
			catch (Exception ex)
			{
				throw ex;
			}


		}
	}
}
