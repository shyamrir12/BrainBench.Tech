using System.Data;

namespace AuthServer.Factory
{
	public interface IConnectionFactoryAuthDB
	{
		IDbConnection GetConnection { get; }
	}
}
