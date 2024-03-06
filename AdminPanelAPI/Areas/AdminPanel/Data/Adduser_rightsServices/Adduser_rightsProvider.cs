using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using Dapper;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.Adduser_rightsServices
{
    public class Adduser_rightsProvider : RepositoryBase, IAdduser_rightsProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public Adduser_rightsProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }
        public async Task<Result<List<MenuItem>>> GetMenuListByRole(CommanRequest model)
        {
            Result<List<MenuItem>> res = new Result<List<MenuItem>>();

            List<MenuItem> objListMenuEF = new List<MenuItem>();

            List<Category> categories = new List<Category>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 1,

                };

                var result = await Connection.QueryAsync<Category>("Proc_Get_MenuBy_UserRoll", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                            ActionName = item.ActionName,
                            url = item.url,
                            Area = item.Area,
                            DisplaySrNo = item.DisplaySrNo,
                            GifIcon = item.GifIcon,
                            ParentCategoryId = (item.ParentCategoryId != 0) ? item.ParentCategoryId : (int?)null
                        });


                    }

                    List<TreeNode> headerTree = FillRecursive(categories, null);

                    foreach (var item in headerTree)
                    {
                        var _google = new MenuItem()
                        {
                            MenuItemId = item.CategoryId,
                            MenuName = item.CategoryName,
                            MenuItemName = item.ControllerName,
                            MenuItemPath = item.ActionName,
                            url = item.url,
                            Area = item.Area,
                            DisplaySrNo = item.DisplaySrNo,
                            GifIcon = item.GifIcon,
                            ParentItemId = item.ParentCategoryId,
                        };
                        foreach (var item2 in item.Children)
                        {
                            _google.ChildMenuItems.Add(new MenuItem()
                            {
                                MenuItemId = item2.CategoryId,
                                MenuName = item2.CategoryName,
                                MenuItemName = item2.ControllerName,
                                MenuItemPath = item2.ActionName,//+"?page=" + item.CategoryId
                                url = item2.url,
                                Area = item2.Area,
                                DisplaySrNo = item2.DisplaySrNo,
                                GifIcon = item2.GifIcon,
                                ParentItemId = item2.ParentCategoryId,
                            });
                        }
                        //if (_google.MenuName.Equals("Dashboard"))
                        //	objListMenuEF.Insert(0, _google);
                        //else if (_google.MenuName.Equals("Master"))
                        //	objListMenuEF.Insert(1, _google);
                        //else
                        objListMenuEF.Add(_google);
                    }
                    res.Data = objListMenuEF;
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = false;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetMenuListByRole", Controller = "Adduser_rightsProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<MenuItem>>> GetMenuListFormate(CommanRequest model)
        {
            Result<List<MenuItem>> res = new Result<List<MenuItem>>();
          
            List<MenuItem> objListMenuEF = new List<MenuItem>();

            List<Category> categories = new List<Category>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 2,

                };

                var result = await Connection.QueryAsync<Category>("Proc_Get_MenuBy_UserRoll", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                            ActionName = item.ActionName,
                            url = item.url,
                            Area = item.Area,
                            DisplaySrNo = item.DisplaySrNo,
                            GifIcon = item.GifIcon,
                            ParentCategoryId = (item.ParentCategoryId != 0) ? item.ParentCategoryId : (int?)null
                        });


                    }

                    List<TreeNode> headerTree = FillRecursive(categories, null);

                    foreach (var item in headerTree)
                    {
                        var _google = new MenuItem()
                        {
                            MenuItemId = item.CategoryId,
                            MenuName = item.CategoryName,
                            MenuItemName = item.ControllerName,
                            MenuItemPath = item.ActionName,
                            url = item.url,
                            Area = item.Area,
                            DisplaySrNo = item.DisplaySrNo,
                            GifIcon = item.GifIcon,
                            ParentItemId = item.ParentCategoryId,
                        };
                        foreach (var item2 in item.Children)
                        {
                            _google.ChildMenuItems.Add(new MenuItem()
                            {
                                MenuItemId = item2.CategoryId,
                                MenuName = item2.CategoryName,
                                MenuItemName = item2.ControllerName,
                                MenuItemPath = item2.ActionName,//+"?page=" + item.CategoryId
                                url = item2.url,
                                Area = item2.Area,
                                DisplaySrNo = item2.DisplaySrNo,
                                GifIcon = item2.GifIcon,
                                ParentItemId = item2.ParentCategoryId,
                            });
                        }
                        //if (_google.MenuName.Equals("Dashboard"))
                        //	objListMenuEF.Insert(0, _google);
                        //else if (_google.MenuName.Equals("Master"))
                        //	objListMenuEF.Insert(1, _google);
                        //else
                        objListMenuEF.Add(_google);
                    }
                    res.Data = objListMenuEF;
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = false;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetMenuListFormate", Controller = "Adduser_rightsProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdateMenuByID(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    loginId = model.SubRoleId,
                   RoleId =model.id,
                   UserID = model.UserID,
                   MenuId =model.ids
                };
                var result = await Connection.QueryAsync<MessageEF>("Proc_Insert_Master_Role_Menu_Mvc", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = true;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message.Add("Exception Occurred! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UpdateMenuByID", Controller = "Adduser_rightsProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });

                return res;
            }
            return res;
        }


        private static List<TreeNode> FillRecursive(List<Category> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParentCategoryId.Equals(parentId)).Select(item => new TreeNode
            {

                CategoryName = item.CategoryName,
                ControllerName = item.ControllerName,
                ActionName = item.ActionName,
                CategoryId = item.CategoryId,
                url = item.url,
                Area = item.Area,
                DisplaySrNo = item.DisplaySrNo,
                GifIcon = item.GifIcon,
                ParentCategoryId = item.ParentCategoryId,

                Children = FillRecursive(flatObjects, item.CategoryId)
            }).ToList();
        }
       
        public async Task<Result<List<ListItems>>> GetUserList(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 4,

                };

                var result = await Connection.QueryAsync<ListItems>("Proc_Get_All_DropDown", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = false;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetUserList", Controller = "Adduser_rightsProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }
    }
}
