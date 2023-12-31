using AuthServer.Factory;
using AuthServer.Models;
using AuthServer.Repository;
using Dapper;
using System.Data;

namespace AuthServer.Services
{
	public class UserService : RepositoryBase, IUserService
	{

		public UserService(IConnectionFactoryAuthDB connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
		{

		}
		public async Task<UserLoginSession> Authenticate(LoginEF model)
		{
			UserLoginSession objUserLoginSession = new UserLoginSession();
			try
			{
				var paramList = new
				{
					UserName = model.UserName,
					Password = model.Password,
					EncryptPassword = model.Password,
					LoginUserType = model.UserType,
					Check = "1",
					RemoteIp = model.Remoteid,
					LocalIp = model.Localip,
					UserAgent = model.Browserinfo
				};

				var result = ConnectionAuthDB.Query<UserLoginSession>("uspValidateUser", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{

					objUserLoginSession = result.FirstOrDefault();
				}
				else
				{
					objUserLoginSession.ApplicantName = "-1";
				}

				return objUserLoginSession;

			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{

				objUserLoginSession = null;
			}
		}


		public async Task<UserLoginSession> GetUserById(LoginEF model)
		{


			UserLoginSession objUserLoginSession = new UserLoginSession();
			try
			{
				var paramListPass = new
				{
					UserId = model.UserID,
					UserName = model.UserName,
					Check = "1",
				};

				var resultPass = ConnectionAuthDB.Query<UserLoginSession>("uspUserdata", paramListPass, commandType: System.Data.CommandType.StoredProcedure);

				var paramList = new
				{
					UserName = resultPass.FirstOrDefault().UserName,
					Password = resultPass.FirstOrDefault().Password,
					EncryptPassword = resultPass.FirstOrDefault().EncryptPassword,
					LoginUserType = model.UserType,
					Check = "1",
					RemoteIp = model.Remoteid,
					LocalIp = model.Localip,
					UserAgent = model.Browserinfo
				};

				var result = ConnectionAuthDB.Query<UserLoginSession>("uspValidateUser", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{
					objUserLoginSession = result.FirstOrDefault();
					//if (objUserLoginSession.IsSubUser) // changed for sub user login with prakash(27-03-23)
					//{
					//    objUserLoginSession.Listmenu = MenuList(new menuonput() { MineralId = (int)objUserLoginSession.MineralId, UserID = (int)objUserLoginSession.SubUserID, MineralName = objUserLoginSession.MineralName, MMenuId = null });
					//}
					//else
					//{
					//    objUserLoginSession.Listmenu = MenuList(new menuonput() { MineralId = (int)objUserLoginSession.MineralId, UserID = (int)objUserLoginSession.UserId, MineralName = objUserLoginSession.MineralName, MMenuId = null });
					//}
					menuonput objmenu = new menuonput();
					List<MenuEF> Listmenu = new List<MenuEF>();
					if (objUserLoginSession.IsSubUser) // changed for sub user login with prakash(27-03-23)
					{
						objmenu.UserID = Convert.ToInt32(objUserLoginSession.SubUserID);
					}
					else
					{
						objmenu.UserID = Convert.ToInt32(objUserLoginSession.UserId);
					}

					objmenu.MineralId = Convert.ToInt32(objUserLoginSession.MineralId);
					objmenu.MineralName = objUserLoginSession.MineralName;
					objmenu.MMenuId = null;
					Listmenu = MenuList(objmenu);
					objUserLoginSession.Listmenu = Listmenu;
				}

				return objUserLoginSession;



			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{

				objUserLoginSession = null;
			}
		}


		public List<MenuEF> MenuList(menuonput objmenuonput)
		{
			string result = "";
			List<MenuEF> objListMenuEF = new List<MenuEF>();
			List<childMenu> objListchildMenu;
			try
			{
				var paramList = new
				{
					UserID = objmenuonput.UserID,
					MineralId = objmenuonput.MineralId,
					MineralName = objmenuonput.MineralName,
					MMenuId = objmenuonput.MMenuId
				};
				IDataReader dr = ConnectionAuthDB.ExecuteReader("GetUserWiseMenuDataAfterLogin_New_backup", paramList, commandType: System.Data.CommandType.StoredProcedure);
				DataSet ds = new DataSet();
				ds = ConvertDataReaderToDataSet(dr);
				string menu = "";
				if (ds.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow dro in ds.Tables[0].Rows)
					{
						MenuEF objMenuEF = new MenuEF();
						objMenuEF.MenuId = int.Parse(dro["MenuId"].ToString());
						objMenuEF.MenuName = dro["MenuName"].ToString();
						objMenuEF.ParentId = dro["ParentId"].ToString();
						objMenuEF.Controller = dro["Controller"].ToString();
						objMenuEF.url = dro["url"].ToString();
						objMenuEF.Area = dro["Area"].ToString();
						objMenuEF.ActionName = dro["ActionName"].ToString();
						objMenuEF.IsView = dro["IsView"].ToString();
						objMenuEF.IsAdd = dro["IsAdd"].ToString();
						objMenuEF.IsEdit = dro["IsEdit"].ToString();
						objMenuEF.IsDelete = dro["IsDelete"].ToString();
						objMenuEF.DisplaySrNo = int.Parse(dro["DisplaySrNo"].ToString());
						objMenuEF.CssClass = dro["CssClass"].ToString();
						objMenuEF.MenuLevel = dro["MenuLevel"].ToString();
						objMenuEF.ParentMenuName = dro["ParentMenuName"].ToString();
						objMenuEF.LinkPath = dro["LinkPath"].ToString();
						objMenuEF.SvgPath = dro["SvgPath"].ToString();
						objMenuEF.divclass = dro["divclass"].ToString();
						objMenuEF.MobileMenu = int.Parse(dro["MobileMenu"].ToString());
						objListchildMenu = new List<childMenu>();
						foreach (DataRow dro1 in ds.Tables[1].Rows)
						{
							if (dro1["ParentId"].ToString() == objMenuEF.MenuId.ToString())
							{
								childMenu objchildMenu = new childMenu();
								objchildMenu.MenuId = int.Parse(dro1["MenuId"].ToString());
								objchildMenu.MenuName = dro1["MenuName"].ToString();
								objchildMenu.ParentId = int.Parse(dro1["ParentId"].ToString());
								objchildMenu.Controller = dro1["Controller"].ToString();
								objchildMenu.url = dro1["url"].ToString();
								objchildMenu.Area = dro1["Area"].ToString();
								objchildMenu.ActionName = dro1["ActionName"].ToString();
								objchildMenu.IsView = dro1["IsView"].ToString();
								objchildMenu.IsAdd = dro1["IsAdd"].ToString();
								objchildMenu.IsEdit = dro1["IsEdit"].ToString();
								objchildMenu.IsDelete = dro1["IsDelete"].ToString();
								objchildMenu.DisplaySrNo = dro1["DisplaySrNo"].ToString();
								objchildMenu.CssClass = dro1["CssClass"].ToString();
								objchildMenu.MenuLevel = dro1["MenuLevel"].ToString();
								objchildMenu.ParentMenuName = dro1["ParentMenuName"].ToString();
								objchildMenu.GifIcon = dro1["GifIcon"].ToString();
								objchildMenu.Description = dro1["Description"].ToString();
								objListchildMenu.Add(objchildMenu);
							}
						}
						objMenuEF.childMenuList = objListchildMenu;

						objListMenuEF.Add(objMenuEF);
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}

			return objListMenuEF;
		}

		public DataSet ConvertDataReaderToDataSet(IDataReader data)
		{
			DataSet ds = new DataSet();
			int i = 0;
			while (!data.IsClosed)
			{
				ds.Tables.Add("Table" + (i + 1));
				ds.EnforceConstraints = false;
				ds.Tables[i].Load(data);
				i++;
			}
			return ds;
		}
	}
}
