using AuthServer.Factory;
using System.Data;

namespace AuthServer.Repository
{
	public abstract class RepositoryBase : IRepository
	{

		protected IDbConnection ConnectionAuthDB;
		
		private bool _disposed;


		protected RepositoryBase(IConnectionFactoryAuthDB connectionFactoryAuthDB)
		{
			try
			{
				ConnectionAuthDB = connectionFactoryAuthDB.GetConnection;
			
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
					ConnectionAuthDB?.Dispose();
					
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
