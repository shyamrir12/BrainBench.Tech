using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using AdminPanelAPI.Repository;
using AdminPanelAPI.Factory;
using LoginModels;
using System.Data;
using AdminPanelAPI.Extension;
using AdminPanelAPI.Data.ExceptionDataServices;

namespace AdminPanelAPI.Areas.AdminPanel.Data.SMSTemplateMasterServices
{
	public class SMSTemplateMasterProvider: RepositoryBase, ISMSTemplateMasterProvider
	{
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public SMSTemplateMasterProvider(IConnectionFactory connectionFactory, IExceptionDataProvider exceptionDataProvider) : base(connectionFactory)
		{
            _exceptionDataProvider = exceptionDataProvider;
        }
        public async Task<Result< MessageEF>> AddSMSTemplateMaster(SMSTemplateMaster model)
		{
			Result<MessageEF> res = new Result<MessageEF>();
			MessageEF objMessage = new MessageEF();
			try
			{
				var p = new DynamicParameters();
				p.Add("TemplateName", model.TemplateName);
				p.Add("TemplateID", model.TemplateID);
				p.Add("InternalTemplateID", model.InternalTemplateID);
				p.Add("TemplateStatus", model.TemplateStatus);
				p.Add("ContentRegistered", model.ContentRegistered);
				p.Add("ReferencNo", model.ReferencNo);
				p.Add("EmailTemplate", model.EmailTemplate);
				p.Add("Remark", model.Remark);


				p.Add("CreatedBy", model.UserID);
				p.Add("IsActive", model.IsActiveBool);
				//p.Add("UserLoginId", objStatemaster.UserLoginID);
				p.Add("Check", "4");
				p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await Connection.QueryAsync<int>("CRUD_SMSTemplateMaster", p, commandType: CommandType.StoredProcedure);
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
                res.Status = true;
                res.Message = new List<string>() { "Successful!" };
            }
			catch (Exception ex)
			{
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddSMSTemplateMaster", Controller = "SMSTempateMasterProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
		}
	    public async Task<Result<List<SMSTemplateMaster>>> ViewSMSTemplateMaster(CommanRequest model)
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
				var result =await Connection.QueryAsync<SMSTemplateMaster>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{

					ListStatemaster = result.ToList();
				}
				res.Data = ListStatemaster;
                res.Status = true;
                res.Message = new List<string>() { "Successful!" };

            }
			catch (Exception ex)
			{
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ViewSMSTemplateMaster", Controller = "SMSTempateMasterProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
			return res;
		}
		public async Task<Result<SMSTemplateMaster >>EditSMSTemplatemaster(CommanRequest model)
		{
			Result<SMSTemplateMaster> res=new Result<SMSTemplateMaster> ();
			SMSTemplateMaster LobjMStatemaster = new SMSTemplateMaster();
			try
			{
				var paramList = new
				{
					RecordID = model.id,
					// StateID = objStatemaster.StateID,
					Check = "6"
				};
				DynamicParameters param = new DynamicParameters();
				var result =await Connection.QueryAsync<SMSTemplateMaster>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);
				if (result.Count() > 0)
				{
					LobjMStatemaster = result.FirstOrDefault();
				}
				res.Data = LobjMStatemaster;
                res.Status = true;
                res.Message = new List<string>() { "Successful!" };
            }
			catch (Exception ex)
			{
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "EditSMSTemplatemaster", Controller = "SMSTempateMasterProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
			return res;
		}
		public async Task<Result<MessageEF>> DeleteSMSTemplatemaster(CommanRequest model)
		{
			Result<MessageEF> res=new Result<MessageEF> ();
			MessageEF objMessage = new MessageEF();
			try
			{
				object[] objArray = new object[] {
					   "RecordID",model.id,
						"Check",5
			};
				DynamicParameters _param = new DynamicParameters();
				_param = objArray.ToDynamicParameters("Result");
				var result =await Connection.QueryAsync<string>("CRUD_SMSTemplateMaster", _param, commandType: System.Data.CommandType.StoredProcedure);
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
                res.Status = true;
                res.Message = new List<string>() { "Successful!" };
            }
			catch (Exception ex)
			{
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "DeleteSMSTemplatemaster", Controller = "SMSTempateMasterProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
			return res;
		}
		public async Task<Result<MessageEF>> UpdateSMSTemplatemaster(SMSTemplateMaster model)
		{
			Result<MessageEF> res = new Result<MessageEF>();
			MessageEF objMessage = new MessageEF();
			try
			{
				var p = new DynamicParameters();
				p.Add("TemplateName", model.TemplateName);
				p.Add("TemplateID", model.TemplateID);
				p.Add("InternalTemplateID", model.InternalTemplateID);
				p.Add("TemplateStatus", model.TemplateStatus);
				p.Add("ContentRegistered", model.ContentRegistered);
				p.Add("ReferencNo", model.ReferencNo);
				p.Add("EmailTemplate", model.EmailTemplate);
				p.Add("Remark", model.Remark);
				p.Add("RecordID", model.RecordID);
				p.Add("IsActive", model.IsActiveBool);
				p.Add("Check", "3");
				p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
				await Connection.QueryAsync<int>("CRUD_SMSTemplateMaster", p, commandType: CommandType.StoredProcedure);
				int newID = p.Get<int>("Result");
				string response = newID.ToString();
				objMessage.Satus = response;
				res.Data = objMessage;
                res.Status = true;
                res.Message = new List<string>() { "Successful!" };
            }
			catch (Exception ex)
			{
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "DeleteSMSTemplatemaster", Controller = "UpdateSMSTemplatemaster", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
			return res;
		}
	}
}
