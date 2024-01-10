using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using System.Data;

namespace IntegrationApi.Data.DSCResponseVerifyServices
{
	public class DSCResponseVerifyProvider : RepositoryBase, IDSCResponseVerifyProvider
	{
		public DSCResponseVerifyProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
		{

		}
		public async Task<MessageEF> InsertDSCRespnseData(DSCResponse objDSCResponseModel)
		{
			MessageEF objMessage = new MessageEF();
			{
				try
				{
					var p = new DynamicParameters();
					p.Add("@DSCCertificateClass", objDSCResponseModel.DSCCertificateClass);
					p.Add("@DSCUsedBy", objDSCResponseModel.DSCUsedBy);
					p.Add("@DSCForId", objDSCResponseModel.DSCForId);
					p.Add("@DSCFor", objDSCResponseModel.DSCFor);
					p.Add("@Response", objDSCResponseModel.Response);
					p.Add("@DSCIssuedDate", objDSCResponseModel.DSCIssuedDate);
					p.Add("@DSCExpiredDate", objDSCResponseModel.DSCExpiredDate);
					p.Add("@DSCEmail", objDSCResponseModel.DSCEmail);
					p.Add("@DSCCountry", objDSCResponseModel.DSCCountry);
					p.Add("@DSCIssuerCommonName", objDSCResponseModel.DSCIssuerCommonName);
					p.Add("@DSCCommonName", objDSCResponseModel.DSCCommonName);
					p.Add("@DSCSerialNo", objDSCResponseModel.DSCSerialNo);

					//p.Add("P_Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
					int newID = Convert.ToInt32(await Connection.QueryAsync<int>("InsertDSCResponseRecords", p, commandType: CommandType.StoredProcedure));

					objMessage.Satus = newID.ToString();
				}
				catch (Exception ex)
				{
					throw ex;
				}

				return objMessage;
			}
		}
	}
}
