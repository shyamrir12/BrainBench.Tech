using IntegrationApi.Data.ExceptionDataServices;
using IntegrationModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntegrationApi.ExceptionFilter
{
	public class ExceptionFilterHandlear : ExceptionFilterAttribute
	{
		
		IExceptionDataProvider _objIExceptionProvider;
		
		public ExceptionFilterHandlear(IExceptionDataProvider objIExceptionProvider)
		{
			_objIExceptionProvider = objIExceptionProvider;
		}
		
		public override void OnException(ExceptionContext context)
		{
			try
			{
				LogEntry objLogEntry = new LogEntry();
				objLogEntry.Action = context.ActionDescriptor.DisplayName;
				objLogEntry.Controller = context.HttpContext.Request.Path;
				objLogEntry.ReturnType = "Integration API";
				objLogEntry.StackTrace = context.Exception.StackTrace;
				objLogEntry.ErrorMessage = context.Exception.Message;
				//_objIExceptionProvider.ErrorList(objLogEntry);

			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
