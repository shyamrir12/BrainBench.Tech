using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using AdminPanelAPI.Repository;
using AdminPanelAPI.Factory;
using LoginModels;
using System.Data;
using AdminPanelAPI.Extension;

namespace AdminPanelAPI.Areas.AdminPanel.Data.SMSTempateMasterServices
{
	public class SMSTempateMasterProvider: RepositoryBase, ISMSTempateMasterProvider
	{
		public SMSTempateMasterProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
		{

		}
		public Result< MessageEF> AddSMSTemplateMaster(SMSTemplateMaster objStatemaster)
		{
			Result<MessageEF> res = new Result<MessageEF>();
			MessageEF objMessage = new MessageEF();
			try
			{
				var p = new DynamicParameters();
				p.Add("TemplateName", objStatemaster.TemplateName);
				p.Add("TemplateID", objStatemaster.TemplateID);
				p.Add("InternalTemplateID", objStatemaster.InternalTemplateID);
				p.Add("TemplateStatus", objStatemaster.TemplateStatus);
				p.Add("ContentRegistered", objStatemaster.ContentRegistered);
				p.Add("ReferencNo", objStatemaster.ReferencNo);
				p.Add("EmailTemplate", objStatemaster.EmailTemplate);
				p.Add("Remark", objStatemaster.Remark);


				p.Add("CreatedBy", objStatemaster.UserID);
				p.Add("IsActive", objStatemaster.IsActiveBool);
				//p.Add("UserLoginId", objStatemaster.UserLoginID);
				p.Add("Check", "4");
				p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
				Connection.Query<int>("CRUD_SMSTemplateMaster", p, commandType: CommandType.StoredProcedure);
				int newID = p.Get<int>("Result");
				string response = newID.ToString();
				if (response == "1")
				{
					objMessage.Satus = "1";
				}
				else
				{
					objMessage.Satus = "2";
				}
				res.Data = objMessage;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return res;
		}

	
		public Result<List<SMSTemplateMaster>> ViewSMSTemplateMaster(SMSTemplateMaster objStatemaster)
		{
			Result<List<SMSTemplateMaster>> res = new Result<List<SMSTemplateMaster>>();
			List<SMSTemplateMaster> ListStatemaster = new List<SMSTemplateMaster>();
			try
			{
				var paramList = new
				{
					//StateName = objStatemaster.StateName,
					Check = "2",
					//StateID = objStatemaster.StateID
				};
				var result = Connection.Query<SMSTemplateMaster>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{

					ListStatemaster = result.ToList();
				}
				res.Data = ListStatemaster;	
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return res;
		}
		public Result<SMSTemplateMaster >EditSMSTemplatemaster(SMSTemplateMaster objStatemaster)
		{
			Result<SMSTemplateMaster> res=new Result<SMSTemplateMaster> ();
			SMSTemplateMaster LobjMStatemaster = new SMSTemplateMaster();
			try
			{
				var paramList = new
				{
					RecordID = objStatemaster.RecordID,
					// StateID = objStatemaster.StateID,
					Check = "6"
				};
				DynamicParameters param = new DynamicParameters();
				var result = Connection.Query<SMSTemplateMaster>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);
				if (result.Count() > 0)
				{
					LobjMStatemaster = result.FirstOrDefault();
				}
				res.Data = LobjMStatemaster;
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return res;
		}
		public Result<MessageEF> DeleteSMSTemplatemaster(SMSTemplateMaster objStatemaster)
		{
			Result<MessageEF> res=new Result<MessageEF> ();
			MessageEF objMessage = new MessageEF();
			try
			{
				object[] objArray = new object[] {
					   "RecordID",objStatemaster.RecordID,
						"Check",5
			};
				DynamicParameters _param = new DynamicParameters();
				_param = objArray.ToDynamicParameters("Result");
				var result = Connection.Query<string>("CRUD_SMSTemplateMaster", _param, commandType: System.Data.CommandType.StoredProcedure);
				string response = _param.Get<string>("Result");
				if (response == "1")
				{
					objMessage.Satus = "1";
				}
				else
				{
					objMessage.Satus = "2";
				}
				res.Data = objMessage;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return res;
		}
		public Result<MessageEF> UpdateSMSTemplatemaster(SMSTemplateMaster objStatemaster)
		{
			Result<MessageEF> res = new Result<MessageEF>();
			MessageEF objMessage = new MessageEF();
			try
			{
				var p = new DynamicParameters();
				p.Add("TemplateName", objStatemaster.TemplateName);
				p.Add("TemplateID", objStatemaster.TemplateID);
				p.Add("InternalTemplateID", objStatemaster.InternalTemplateID);
				p.Add("TemplateStatus", objStatemaster.TemplateStatus);
				p.Add("ContentRegistered", objStatemaster.ContentRegistered);
				p.Add("ReferencNo", objStatemaster.ReferencNo);
				p.Add("EmailTemplate", objStatemaster.EmailTemplate);
				p.Add("Remark", objStatemaster.Remark);
				p.Add("RecordID", objStatemaster.RecordID);
				p.Add("IsActive", objStatemaster.IsActiveBool);
				p.Add("Check", "3");
				p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
				Connection.Query<int>("CRUD_SMSTemplateMaster", p, commandType: CommandType.StoredProcedure);
				int newID = p.Get<int>("Result");
				string response = newID.ToString();
				objMessage.Satus = response;
				res.Data = objMessage;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return res;
		}
	}
}
