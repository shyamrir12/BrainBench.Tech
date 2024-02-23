namespace LoginModels
{
	public class UserLoginSession
	{
		public UserLoginSession()
		{
			Items = new List<MenuItem>();
		}
		public int UserID { get; set; }
		public int RoleId { get; set; }
		public string Role { get; set; }
		public Nullable<int> subroleid { get; set; }
		public string Name { get; set; }
		public string OrganizationName { get; set; }
		public string EmailId { get; set; }
		public string Mobile_No { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public Nullable<System.DateTime> CreateDate { get; set; }
		public string Ipaddress { get; set; }
		public Nullable<int> loginattempt { get; set; }
		public Nullable<bool> Activation { get; set; }
		public Nullable<System.DateTime> logindate { get; set; }
		public string Designation { get; set; }
		public string photo { get; set; }
		public string Deptid { get; set; }
		public string DocSectionid { get; set; }
		public string DivisionId { get; set; }
		public string? WardId { get; set; }
		public string? RasoiId { get; set; }
        public string? jwttoken { get; set; }
        public DateTime validTo { get; set; }
        public List<MenuItem> Items { get; set; }


		//int? userId;
		//int? subUserId;
		//int? userTypeId;
		//int? roleId;
		//int? userLoginId;
		//int? isPasswordChange;
		//int? mineralId;
		//int? tPOffline;
		//int? leaseStatusId;
		//int? isLowGradeLimestone;
		//List<MenuEF> listmenu;
		//List<string> list;
		//string userName;
		//string password;
		//string applicantName;
		//string userType;
		//string mineralName;
		//string userRoleInfo;
		//string mobileNumber;
		//string emailID;
		//string districtName;
		//string lesseeStatus;
		//string mineralTypeName;
		//string isSidePanelHide;
		//string noticeFilePath;
		//string sMS_SENT;
		//string encryptPassword;
		//string photoPath;
		////ShyamSir
		//string jwttoken;
		////ShyamSir
		//string mmenuid;
		//string lesseeType;
		//int isActive;
		//public string Password { get; set; }
		//public int? UserId
		//{
		//	get
		//	{
		//		return userId == null ? 0 : userId;
		//	}
		//	set
		//	{
		//		userId = value == null ? 0 : value;
		//	}
		//}
		//public int? SubUserID
		//{
		//	get
		//	{
		//		return subUserId == null ? 0 : subUserId;
		//	}
		//	set
		//	{
		//		subUserId = value == null ? 0 : value;
		//	}
		//}
		//public int? UserTypeId
		//{
		//	get
		//	{
		//		return userTypeId == null ? 0 : userTypeId;
		//	}
		//	set
		//	{
		//		userTypeId = value == null ? 0 : value;
		//	}
		//}
		//public int? RoleId
		//{
		//	get
		//	{
		//		return roleId == null ? 0 : roleId;
		//	}
		//	set
		//	{
		//		roleId = value == null ? 0 : value;
		//	}
		//}
		//public int? UserLoginId
		//{
		//	get
		//	{
		//		return userLoginId == null ? 0 : userLoginId;
		//	}
		//	set
		//	{
		//		userLoginId = value == null ? 0 : value;
		//	}
		//}
		//public int? IsPasswordChange
		//{
		//	get
		//	{
		//		return isPasswordChange == null ? 0 : isPasswordChange;
		//	}
		//	set
		//	{
		//		isPasswordChange = value == null ? 0 : value;
		//	}
		//}
		//public int? MineralId
		//{
		//	get
		//	{
		//		return mineralId == null ? 0 : mineralId;
		//	}
		//	set
		//	{
		//		mineralId = value == null ? 0 : value;
		//	}
		//}

		//internal object MenuList(menuonput objmenu)
		//{
		//	throw new NotImplementedException();
		//}

		//public int? TPOffline
		//{
		//	get
		//	{
		//		return tPOffline == null ? 0 : tPOffline;
		//	}
		//	set
		//	{
		//		tPOffline = value == null ? 0 : value;
		//	}
		//}
		//public int? LeaseStatusId
		//{
		//	get
		//	{
		//		return leaseStatusId == null ? 0 : leaseStatusId;
		//	}
		//	set
		//	{
		//		leaseStatusId = value == null ? 0 : value;
		//	}
		//}
		//public int? IsLowGradeLimestone
		//{
		//	get
		//	{
		//		return isLowGradeLimestone == null ? 0 : isLowGradeLimestone;
		//	}
		//	set
		//	{
		//		isLowGradeLimestone = value == null ? 0 : value;
		//	}
		//}
		//public string UserName
		//{
		//	get
		//	{
		//		return userName == null ? "" : userName;
		//	}
		//	set
		//	{
		//		userName = value == null ? "" : value;
		//	}
		//}
		////public string Password
		////{
		////    get
		////    {
		////        return password == null ? "" : password;
		////    }
		////    set
		////    {
		////        password = value == null ? "" : value;
		////    }
		////}
		//public string ApplicantName
		//{
		//	get
		//	{
		//		return applicantName == null ? "" : applicantName;
		//	}
		//	set
		//	{
		//		applicantName = value == null ? "" : value;
		//	}
		//}
		//public string UserType
		//{
		//	get
		//	{
		//		return userType == null ? "" : userType;
		//	}
		//	set
		//	{
		//		userType = value == null ? "" : value;
		//	}
		//}
		//public string MineralName
		//{
		//	get
		//	{
		//		return mineralName == null ? "" : mineralName;
		//	}
		//	set
		//	{
		//		mineralName = value == null ? "" : value;
		//	}
		//}
		//public string UserRoleInfo
		//{
		//	get
		//	{
		//		return userRoleInfo == null ? "" : userRoleInfo;
		//	}
		//	set
		//	{
		//		userRoleInfo = value == null ? "" : value;
		//	}
		//}
		//public string MobileNumber
		//{
		//	get
		//	{
		//		return mobileNumber == null ? "" : mobileNumber;
		//	}
		//	set
		//	{
		//		mobileNumber = value == null ? "" : value;
		//	}
		//}
		//public string EmailID
		//{
		//	get
		//	{
		//		return emailID == null ? "" : emailID;
		//	}
		//	set
		//	{
		//		emailID = value == null ? "" : value;
		//	}
		//}
		//public string DistrictName
		//{
		//	get
		//	{
		//		return districtName == null ? "" : districtName;
		//	}
		//	set
		//	{
		//		districtName = value == null ? "" : value;
		//	}
		//}
		//public string LesseeStatus
		//{
		//	get
		//	{
		//		return lesseeStatus == null ? "" : lesseeStatus;
		//	}
		//	set
		//	{
		//		lesseeStatus = value == null ? "" : value;
		//	}
		//}
		//public string MineralTypeName
		//{
		//	get
		//	{
		//		return mineralTypeName == null ? "" : mineralTypeName;
		//	}
		//	set
		//	{
		//		mineralTypeName = value == null ? "" : value;
		//	}
		//}
		//public string IsSidePanelHide
		//{
		//	get
		//	{
		//		return isSidePanelHide == null ? "" : isSidePanelHide;
		//	}
		//	set
		//	{
		//		isSidePanelHide = value == null ? "" : value;
		//	}
		//}
		//public string NoticeFilePath
		//{
		//	get
		//	{
		//		return noticeFilePath == null ? "" : noticeFilePath;
		//	}
		//	set
		//	{
		//		noticeFilePath = value == null ? "" : value;
		//	}
		//}
		//public string SMS_SENT
		//{
		//	get
		//	{
		//		return sMS_SENT == null ? "" : sMS_SENT;
		//	}
		//	set
		//	{
		//		sMS_SENT = value == null ? "" : value;
		//	}
		//}
		//public string EncryptPassword
		//{
		//	get
		//	{
		//		return encryptPassword == null ? "" : encryptPassword;
		//	}
		//	set
		//	{
		//		encryptPassword = value == null ? "" : value;
		//	}
		//}

		//public bool IsSubUser { get; set; }
		//public bool IsOnlyTP { get; set; }
		//public string PhotoPath
		//{
		//	get
		//	{
		//		return photoPath == null ? "" : photoPath;
		//	}
		//	set
		//	{
		//		photoPath = value == null ? "" : value;
		//	}
		//}
		////ShyamSir
		//public string JwtToken
		//{
		//	get
		//	{
		//		return jwttoken == null ? "" : jwttoken;
		//	}
		//	set
		//	{
		//		jwttoken = value == null ? "" : value;
		//	}
		//}
		////ShyamSir

		//public string MMenuId
		//{
		//	get
		//	{
		//		return mmenuid == null ? "" : mmenuid;
		//	}
		//	set
		//	{
		//		mmenuid = value == null ? "" : value;
		//	}
		//}

		//public string LesseeType
		//{
		//	get
		//	{
		//		return lesseeType == null ? "" : lesseeType;
		//	}
		//	set
		//	{
		//		lesseeType = value == null ? "" : value;
		//	}
		//}

		//public bool IsActive
		//{
		//	get; set;
		//}

		////Added by Sunil as Discussed with Shyam for designation
		//public string Designation { get; set; }
		//public string Department { get; set; }
	}
}
