using Hangfire;
using IntegrationApi.Data.SchedulerMailSMSServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulerEmailSMSController : ControllerBase
	{
		private readonly ISchedulerMailSMSProvider _sheduleEmailSMS;
		public SchedulerEmailSMSController(ISchedulerMailSMSProvider sheduleEmailSMS)
		{
			_sheduleEmailSMS = sheduleEmailSMS;
		}

		[HttpGet]
		public string SendEmailSMS()
		{
			//_sheduleEmailSMS.SendEmail("Direct Call", DateTime.Now.ToLongTimeString());

			//   BackgroundJob.Enqueue(() => _sheduleEmailSMS.SendEmailSMS("Fire-and-Forget Job", DateTime.Now.ToLongTimeString()));

			//  BackgroundJob.Schedule(() => _sheduleEmailSMS.SendEmailSMS("SMS Email Job", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(30));


			RecurringJob.AddOrUpdate("easyjob", () => _sheduleEmailSMS.SendEmailSMS("Recurring Job", DateTime.Now.ToLongTimeString()), Cron.Minutely);


			//var jobId = BackgroundJob.Schedule(() => _sheduleEmailSMS.SendEmailSMS("Continuation Job 1", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(45));
			// BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 2 - Email Reminder - " + DateTime.Now.ToLongTimeString()));

			return "Email Initiated";
		}
	}
}
