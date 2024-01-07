using Dapper;
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
			catch (System.Exception ex)
			{
				throw ex;
			}
		}
	}
}
