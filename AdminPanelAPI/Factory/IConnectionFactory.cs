using System.Data;

namespace AdminPanelAPI.Factory
{
	public interface IConnectionFactory
	{
		IDbConnection GetConnection { get; }
	}
}
