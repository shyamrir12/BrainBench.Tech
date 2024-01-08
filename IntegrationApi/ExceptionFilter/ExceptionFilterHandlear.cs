using IntegrationApi.Data.ExceptionDataServices;
using IntegrationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace IntegrationApi.ExceptionFilter
{
	public class ExceptionFilterHandlear : ExceptionFilterAttribute
	{

		private readonly IExceptionDataProvider _objIExceptionProvider;
		private readonly ILogger<ExceptionFilterHandlear> _logger;
		public ExceptionFilterHandlear(IExceptionDataProvider objIExceptionProvider, ILogger<ExceptionFilterHandlear> logger)
		{
			_objIExceptionProvider = objIExceptionProvider;
			_logger = logger;
		}
		
		public override void OnException(ExceptionContext context)
		{
			_logger.LogError(context.Exception, context.Exception.Message);

			try
			{
				LogEntry objLogEntry = new LogEntry();
				objLogEntry.Action = context.ActionDescriptor.DisplayName;
				objLogEntry.Controller = context.HttpContext.Request.Path;
				objLogEntry.ReturnType = "Integration API";
				objLogEntry.StackTrace = context.Exception.StackTrace;
				objLogEntry.ErrorMessage = context.Exception.Message;
				_objIExceptionProvider.ErrorList(objLogEntry);

			}
			catch (Exception ex)
			{

				_logger.LogError(ex, ex.Message);
				
			}
			
			
		}
	}
}
