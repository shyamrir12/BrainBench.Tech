using Dapper;
using IntegrationApi.ExceptionFilter;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.ExceptionDataServices
{
	public class ExceptionDataProvider : RepositoryBase, IExceptionDataProvider
	{
		private readonly ILogger<ExceptionDataProvider> _logger;

		public ExceptionDataProvider(IConnectionFactory connectionFactory, ILogger<ExceptionDataProvider> logger) : base(connectionFactory)
		{
			_logger = logger;

		}
		public string ErrorList(LogEntry objLogEntry)
		{
			try
			{
				var paramList = new
				{
					Controller = objLogEntry.Controller,
					Action = objLogEntry.Action,
					ReturnType = objLogEntry.ReturnType,
					ErrorMessage = objLogEntry.ErrorMessage,
					StackTrace = objLogEntry.StackTrace,
					UserId = objLogEntry.UserID,
					UserLoginID = objLogEntry.UserLoginID
				};
				var result = Connection.Execute("ReportErrorLog", paramList, commandType: System.Data.CommandType.StoredProcedure);
				return "1";
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public string ErrorLoger(Exception exception, HttpContext context)
		{
			_logger.LogError(exception, exception.Message);
			try
			{
				var paramList = new
				{
					Controller = context.Request.RouteValues["controller"].ToString(),
					Action = context.Request.RouteValues["controller"].ToString(),
					ReturnType = "globel error",
					ErrorMessage = exception.Message,
					StackTrace = exception.StackTrace,

				};
				var result = Connection.Execute("ReportErrorLog", paramList, commandType: System.Data.CommandType.StoredProcedure);
				
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
			}
			return "1";
		}
	}
}
