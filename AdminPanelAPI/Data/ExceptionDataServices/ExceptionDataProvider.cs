using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using Dapper;

namespace AdminPanelAPI.Data.ExceptionDataServices
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

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return "1";
        }
    }
}
