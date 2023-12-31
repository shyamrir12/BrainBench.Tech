using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthServer.Factory
{
	public class ConnectionFactory : IConnectionFactoryAuthDB
	{
		private readonly string _connectionString;


		public ConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IDbConnection GetConnection => new SqlConnection(_connectionString);
	}
}
