using System.Data;

namespace IntegrationApi.Factory
{
	public interface IConnectionFactory
	{
		IDbConnection GetConnection { get; }
	}
}
