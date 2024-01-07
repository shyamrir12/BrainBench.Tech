namespace IntegrationApi.Data.SchedulerMailSMSServices
{
	public interface ISchedulerMailSMSProvider
	{
		void SendEmailSMS(string backGroundJobType, string startTime);
	}
}
