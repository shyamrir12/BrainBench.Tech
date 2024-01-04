namespace AuthServer.Models
{
	public class LoginEF
	{
		public int? UserTypeId { get; set; }
		public string? UserType { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string? Mail { get; set; }
		public int? UserID { get; set; }
		public string? MobileNo { get; set; }
		public string? Remoteid { get; set; }
		public string? Localip { get; set; }
		public string? Browserinfo { get; set; }
	}

	public class MenuEF
	{

		public MenuEF()
		{
			Items = new List<MenuItem>();
		}

		// User Information
		public string role { get; set; }
		public string name { get; set; }
		public string username { get; set; }
		public string designation { get; set; }
		public string userimage { get; set; }
		public string department { get; set; }


		public List<MenuItem> Items { get; set; }


	}
	

	public class MenuItem
	{
		public MenuItem()
		{
			this.ChildMenuItems = new List<MenuItem>();
		}

		public int MenuItemId { get; set; }
		public string MenuItemName { get; set; }
		public string MenuItemPath { get; set; }
		public string MenuName { get; set; }
		public Nullable<int> ParentItemId { get; set; }

		public string url { get; set; }
		public string Area { get; set; }
		public string DisplaySrNo { get; set; }
		public string GifIcon { get; set; }

		public virtual ICollection<MenuItem> ChildMenuItems { get; set; }

	}
	public class TreeNode
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public TreeNode ParentCategory { get; set; }
		public List<TreeNode> Children { get; set; }
	}
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public System.Nullable<int> ParentCategoryId { get; set; }
	}
}
