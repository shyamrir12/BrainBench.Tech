using Azure;
using IntegrationApi.Data.ExceptionDataServices;
using IntegrationModels;
using System.Net;
using System.Text.Json;

namespace IntegrationApi.ExceptionFilter
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;
		private readonly IExceptionDataProvider _objIExceptionProvider;
		public ExceptionHandlingMiddleware(RequestDelegate next,IExceptionDataProvider objIExceptionProvider, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			//, ILogger<ExceptionHandlingMiddleware> logger 
			_logger = logger;
			_objIExceptionProvider = objIExceptionProvider;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = context.Response;

			//5.0 code
			//try
			//{
			//	LogEntry objLogEntry = new LogEntry();

			//objLogEntry.Action = context.Request.RouteValues["controller"].ToString();
			//objLogEntry.Controller = context.Request.RouteValues["controller"].ToString();
			//objLogEntry.ReturnType = "Integration API";
			//objLogEntry.StackTrace = exception.StackTrace;
			//objLogEntry.ErrorMessage = exception.Message;
			//_objIExceptionProvider.ErrorList(objLogEntry);
			//}
			//catch (Exception ex)
			//{

			//	throw ;
			//}
			//5.0 code
			var errorResponse = new LogEntry();
			//{
			//	Success = false
			//};
			switch (exception)
			{
				case ApplicationException ex:
					if (ex.Message.Contains("Invalid Token"))
					{
						response.StatusCode = (int)HttpStatusCode.Forbidden;
						errorResponse.ErrorMessage = ex.Message;
						break;
					}
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse.ErrorMessage = ex.Message;
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					errorResponse.ErrorMessage = "Internal server error!";
					break;
			}

			_logger.LogError(exception.Message);
			var result = JsonSerializer.Serialize(errorResponse);
			await context.Response.WriteAsync(result);
		}
	}
}
