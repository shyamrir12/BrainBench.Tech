using Microsoft.Data.SqlClient;
using System.Data;

namespace IntegrationApi.Factory
{
	public class ConnectionFactory: IConnectionFactory
	{
		private readonly string _connectionString;


		public ConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IDbConnection GetConnection => new SqlConnection(_connectionString);
	}
}
