using IntegrationApi.Factory;
using System.Data;

namespace IntegrationApi.Repository
{
    public class RepositoryBase
    {
		protected IDbConnection Connection;

		private bool _disposed;


		protected RepositoryBase(IConnectionFactory connectionFactoryAuthDB)
		{
			try
			{
				Connection = connectionFactoryAuthDB.GetConnection;

				//Not required to open the connection, it will automatically managed by DAPPER
				//Connection.Open();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Connection?.Dispose();

				}
				_disposed = true;
			}
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="RepositoryBase{TEntity}"/> class.
		/// </summary>
		~RepositoryBase()
		{
			Dispose(false);
		}
	}
}
