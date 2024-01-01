﻿using AuthServer.Factory;
using AuthServer.Models;
using AuthServer.Repository;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Services
{
	public class UserService : RepositoryBase, IUserService
	{

		public UserService(IConnectionFactoryAuthDB connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
		{

		}
		//Added for salt and password incription by shyam sir 14-9-23
		public static string ComputeSha256Hash(string rawData)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
				StringBuilder builder = new StringBuilder();

				for (int i = 0; i <= bytes.Length - 1; i++)
					builder.Append(bytes[i].ToString("x2"));

				return builder.ToString();
			}
		}
		//Added for salt and password incription by shyam sir 14-9-23

		public async Task<UserLoginSession> Authenticate(LoginEF model)
		{
			UserLoginSession objUserLoginSession = new UserLoginSession();
			try
			{

				//Added for salt and password incription by shyam sir 14-9-23
				var paramListPass = new
				{
					UserName = model.UserName,
					Check = "1",
				};

				var resultPass = ConnectionAuthDB.Query<UserLoginSession>("AdminPanel_uspUserdata", paramListPass, commandType: System.Data.CommandType.StoredProcedure);
				if (resultPass.Count() > 0)
				{
					string pwd2 = ComputeSha256Hash(model.UserID + resultPass.FirstOrDefault().EncryptPassword);
					if (model.Password.ToUpper() == pwd2.ToUpper())
					{
						model.Password = resultPass.FirstOrDefault().Password;
					}


				}
				//Added for salt and password incription by shyam sir 14-9-23


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

				var result = ConnectionAuthDB.Query<UserLoginSession>("AdminPanel_ValidateUser", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{

					objUserLoginSession = result.FirstOrDefault();
				}
				else
				{
					objUserLoginSession.Name = "-1";
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
				
				var paramList = new
				{
					UserId = model.UserID,
					UserName = model.UserName,
					Check = "2",
				};

				var result = ConnectionAuthDB.Query<UserLoginSession>("AdminPanel_uspUserdata", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{
					objUserLoginSession = result.FirstOrDefault();
					
			
					objUserLoginSession.Listmenu = MenuList(objUserLoginSession.UserID, objUserLoginSession.RoleId);
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


		public MenuEF MenuList(int UserID,int RoleId)
		{
			
			MenuEF objListMenuEF = new MenuEF();
			
			List<Category> categories = new List<Category>();
			try
			{
				var paramList = new
				{
					RoleId = UserID,
					UserID = RoleId,
					
				};
				var result = ConnectionAuthDB.Query<Category>("Proc_Get_MenuBy_UserRoll", paramList, commandType: System.Data.CommandType.StoredProcedure);

				if (result.Count() > 0)
				{
					foreach (var item in result)
					{

						categories.Add(
						new Category
						{
							CategoryId = item.CategoryId,
							CategoryName = item.CategoryName,
							ControllerName = item.ControllerName,
							ActionName =item.ActionName,
							ParentCategoryId = (item.ParentCategoryId != 0) ? item.ParentCategoryId : (int?)null
						});

						
					}

					List<TreeNode> headerTree = FillRecursive(categories, null);

					foreach (var item in headerTree)
					{
						var _google = new MenuItem()
						{
							MenuName = item.CategoryName,
							MenuItemName = item.ControllerName,
							MenuItemPath = item.ActionName,
						};
						foreach (var item2 in item.Children)
						{
							_google.ChildMenuItems.Add(new MenuItem()
							{
								MenuName = item2.CategoryName,
								MenuItemName = item2.ControllerName,
								MenuItemPath = item2.ActionName,//+"?page=" + item.CategoryId
							});
						}
						if (_google.MenuName.Equals("Dashboard"))
							objListMenuEF.Items.Insert(0, _google);
						else if (_google.MenuName.Equals("Master"))
							objListMenuEF.Items.Insert(1, _google);
						else
							objListMenuEF.Items.Add(_google);
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}

			return objListMenuEF;
		}


		private static List<TreeNode> FillRecursive(List<Category> flatObjects, int? parentId = null)
		{
			return flatObjects.Where(x => x.ParentCategoryId.Equals(parentId)).Select(item => new TreeNode
			{
				CategoryName = item.CategoryName,
				ControllerName = item.ControllerName,
				ActionName = item.ActionName,
				CategoryId = item.CategoryId,
				Children = FillRecursive(flatObjects, item.CategoryId)
			}).ToList();
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
