using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IntegrationApi.Data.SMSServices
{
	public class SMSProvider : RepositoryBase, ISMSProvider
	{
		private readonly IConfiguration configuration;
		public SMSProvider(IConnectionFactory connectionFactory,IConfiguration configuration) : base(connectionFactory)
		{
			this.configuration = configuration;

		}
		//#region User Details
		//static String username = "CGCHIPS-CHIMMS";
		//static String password = "CHiMMS@54321";
		//static String senderid = "DIGSEC";
		//static String secureKey = "e0a83fe1-9943-42d1-a4a5-84cee2c9e87f";
		//static String message = "Hi......";
		//static String mobileNo = "7069042466";
		//// static String mobileNos = "9856XXXXX, 9856XXXXX ";
		//static String scheduledTime = "20110819 13:26:00";
		//#endregion


		public List<SMSTemplate> GetAllApprovedSMSTemplate(SMSTemplate sMS)
		{
			List<SMSTemplate> objLst = new List<SMSTemplate>();
			try
			{

				DynamicParameters param = new DynamicParameters();

				var paramList = new
				{
					Check = 1,
					TemplateID = sMS.TemplateID
				};
				var result = Connection.Query<SMSTemplate>("CRUD_SMSTemplateMaster", paramList, commandType: System.Data.CommandType.StoredProcedure);
				objLst = result.ToList();
			}

			catch (Exception ex)
			{
				throw ex;
			}
			return objLst;
		}

		
		public MessageEF Main(SMS sMS)
		{
			List<SMSTemplate> lst = GetAllApprovedSMSTemplate(new SMSTemplate() { TemplateID = sMS.templateid });
			if ((sMS.parameterlist != null && sMS.parameterlist.Count > 0) && (lst != null && lst.Count > 0))
			{
				if (lst != null && lst.Count > 0)
				{
					sMS.message = TemplateBuilder.BuildTemplate(lst[0].ContentRegistered, sMS.parameterlist);
				}
			}

			MessageEF messageEF = new MessageEF();
			if (sMS != null && sMS.message != null)
			{
				if (!string.IsNullOrEmpty(sMS.mobileNo))
				{
					try
					{
						bool isSMS = true;

						if (sMS.UserId > 0 && sMS.SMS_SENT == "0")
						{
							isSMS = false;
						}

						if (isSMS == true)
						{
							HttpWebRequest request;


							System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
							//request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequest");
							request = (HttpWebRequest)WebRequest.Create(configuration.GetSection("SMSConfiguration").GetValue<string>("requesturl"));
							request.ProtocolVersion = HttpVersion.Version10;
							request.KeepAlive = false;
							request.ServicePoint.ConnectionLimit = 1;
							//((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
							((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
							request.Method = "POST";
							//Console.WriteLine("Before Calling Method");
							//ServicePointManager.CertificatePolicy = new MyPolicy();

							var handler = new HttpClientHandler();
							handler.ServerCertificateCustomValidationCallback = delegate { return true; };
							var http = new HttpClient(handler);

							// added by Avneesh on 12-04-2021//modified on 230922
							var p = new DynamicParameters();
							var dr = Connection.ExecuteScalar("select top 1 EMAIL_SENT from EXPIRY_DATE_ALERT", p, commandType: System.Data.CommandType.Text);
							if (Convert.ToBoolean(dr) == true)
							{
								sendOTPMSG(sMS.UserId,
									configuration.GetSection("SMSConfiguration").GetValue<string>("username")
									, configuration.GetSection("SMSConfiguration").GetValue<string>("password")
									, configuration.GetSection("SMSConfiguration").GetValue<string>("senderid")
									, sMS.mobileNo, sMS.message, configuration.GetSection("SMSConfiguration").GetValue<string>("secureKey")
									, request, sMS.templateid);
							}
							messageEF.Satus = "success";
							//sendOTPMSG(username, password, senderid, mobileNo, message, secureKey, request);
						}
					}
					catch (Exception)
					{
						messageEF.Satus = "failure";
					}

				}
			}
			else
			{
				messageEF.Satus = "failure";
			}
			return messageEF;
		}

		public void sendOTPMSG(int? UserId, String username, String password, String senderid, String mobileNo, String message, String secureKey, HttpWebRequest request, string templateid)
		{

			try
			{
				Stream dataStream;
				String encryptedPassword = string.Empty;
				byte[] encPwd = Encoding.UTF8.GetBytes(password);
				//static byte[] pwd = new byte[encPwd.Length];
				HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
				byte[] pp = sha1.ComputeHash(encPwd);
				// static string result = System.Text.Encoding.UTF8.GetString(pp);
				StringBuilder sb = new StringBuilder();
				foreach (byte b in pp)
				{
					sb.Append(b.ToString("x2"));
				}
				encryptedPassword = sb.ToString();
				StringBuilder sbHash = new StringBuilder();
				sbHash.Append(username).Append(senderid).Append(message).Append(secureKey);
				byte[] genkey = Encoding.UTF8.GetBytes(sbHash.ToString());
				//static byte[] pwd = new byte[encPwd.Length];
				HashAlgorithm shaHas1 = HashAlgorithm.Create("SHA512");
				byte[] sec_key = shaHas1.ComputeHash(genkey);
				StringBuilder sbHash11 = new StringBuilder();
				for (int i = 0; i < sec_key.Length; i++)
				{
					sbHash11.Append(sec_key[i].ToString("x2"));
				}
				String Key = sbHash11.ToString();
				// String smsservicetype = "otpmsg"; //For OTP message.
				String smsservicetype = "singlemsg"; //For single message.
													 //String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
													 //                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
													 //                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
													 //                "&content=" + HttpUtility.UrlEncode(message.Trim()) +
													 //                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
													 //                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
													 //                "&key=" + HttpUtility.UrlEncode(Key.Trim());





				// added by Avneesh on 12-04-2021
				String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
								"&password=" + HttpUtility.UrlEncode(encryptedPassword) +
								"&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
								"&content=" + HttpUtility.UrlEncode(message.Trim()) +
								"&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
								"&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
								"&key=" + HttpUtility.UrlEncode(Key.Trim()) +
								"&templateid=" + HttpUtility.UrlEncode(templateid);
				//"&templateid=" + HttpUtility.UrlEncode("khanijonline02");





				byte[] byteArray = Encoding.ASCII.GetBytes(query);
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = byteArray.Length;
				//dataStream = request.GetRequestStream();
				//dataStream.Write(byteArray, 0, byteArray.Length);
				using (dataStream = request.GetRequestStream())
				{
					dataStream.Write(byteArray, 0, byteArray.Length);
				}
				dataStream.Close();
				WebResponse response = request.GetResponse();
				String Status = ((HttpWebResponse)response).StatusDescription;
				dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				String responseFromServer = reader.ReadToEnd();
				reader.Close();
				dataStream.Close();
				response.Close();

				#region OTP Log
				var paramList = new
				{
					Area = "SMSService.cs",
					Controller = "SMSHttpPostClient.cs",
					Action = "SMSService.cs",
					ReturnType = "SUCCESS",
					ErrorMessage = message,
					StackTrace = responseFromServer,
					UserId = UserId,
					UserLoginID = UserId
				};
				var resultSuccess = Connection.Execute("ReportErrorLog", paramList, commandType: System.Data.CommandType.StoredProcedure);


				#endregion
			}
			catch (Exception ex)
			{
				#region OTP Log
				var paramList = new
				{
					Area = "SMSService.cs",
					Controller = mobileNo,
					Action = "SMSService.cs",
					ReturnType = "FAILURE",
					ErrorMessage = ex.Message,
					StackTrace = message,
					UserId = UserId,
					UserLoginID = UserId
				};
				var resultFailure = Connection.Execute("ReportErrorLog", paramList, commandType: System.Data.CommandType.StoredProcedure);

				#endregion
			}
			//return responseFromServer;
		}

	

		protected String encryptedPasswod(String password)
		{

			byte[] encPwd = Encoding.UTF8.GetBytes(password);
			//static byte[] pwd = new byte[encPwd.Length];
			HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
			byte[] pp = sha1.ComputeHash(encPwd);
			// static string result = System.Text.Encoding.UTF8.GetString(pp);
			StringBuilder sb = new StringBuilder();
			foreach (byte b in pp)
			{

				sb.Append(b.ToString("x2"));
			}
			return sb.ToString();

		}

	

		protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
		{

			StringBuilder sb = new StringBuilder();
			sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
			byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
			//static byte[] pwd = new byte[encPwd.Length];
			HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
			byte[] sec_key = sha1.ComputeHash(genkey);

			StringBuilder sb1 = new StringBuilder();
			for (int i = 0; i < sec_key.Length; i++)
			{
				sb1.Append(sec_key[i].ToString("x2"));
			}
			return sb1.ToString();
		}

		#region Test SMS
		public SMSResponseData TestSMS(string MobileNo)
		{
			Random generator = new Random();
			string number = generator.Next(1, 10000).ToString("D4");
			string message = "Your One time code for Khanij Online application is : " + number + " CHiMMS, GoCG";
			string templateid = "1307161883520738720";
			SMS sMS = new SMS() { mobileNo = MobileNo, message = message, templateid = templateid };
			SMSResponseData sMSResponseModel = new SMSResponseData();
			if (!string.IsNullOrEmpty(sMS.mobileNo))
			{

				bool isSMS = true;

				if (sMS.UserId > 0 && sMS.SMS_SENT == "0")
				{
					isSMS = false;
				}

				if (isSMS == true)
				{
					HttpWebRequest request;


					System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

					request = (HttpWebRequest)WebRequest.Create(configuration.GetSection("SMSConfiguration").GetValue<string>("requesturl"));
					request.ProtocolVersion = HttpVersion.Version10;
					request.KeepAlive = false;
					request.ServicePoint.ConnectionLimit = 1;
					//((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
					((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
					request.Method = "POST";
					//Console.WriteLine("Before Calling Method");
					//ServicePointManager.CertificatePolicy = new MyPolicy();

					var handler = new HttpClientHandler();
					handler.ServerCertificateCustomValidationCallback = delegate { return true; };
					var http = new HttpClient(handler);

					// added by Avneesh on 12-04-2021
					sMSResponseModel = sendTESTOTPMSG(sMS.UserId,
									configuration.GetSection("SMSConfiguration").GetValue<string>("username")
									, configuration.GetSection("SMSConfiguration").GetValue<string>("password")
									, configuration.GetSection("SMSConfiguration").GetValue<string>("senderid"),
									sMS.mobileNo, sMS.message,
									 configuration.GetSection("SMSConfiguration").GetValue<string>("secureKey"), request, sMS.templateid);

					//sendOTPMSG(username, password, senderid, mobileNo, message, secureKey, request);
				}


			}
			return sMSResponseModel;
		}

		public SMSResponseData sendTESTOTPMSG(int? UserId, String username, String password, String senderid, String mobileNo, String message, String secureKey, HttpWebRequest request, string templateid)
		{
			SMSResponseData sMSResponseModel = new SMSResponseData();

			try
			{
				Stream dataStream;
				String encryptedPassword = string.Empty;
				byte[] encPwd = Encoding.UTF8.GetBytes(password);
				//static byte[] pwd = new byte[encPwd.Length];
				HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
				byte[] pp = sha1.ComputeHash(encPwd);
				// static string result = System.Text.Encoding.UTF8.GetString(pp);
				StringBuilder sb = new StringBuilder();
				foreach (byte b in pp)
				{
					sb.Append(b.ToString("x2"));
				}
				encryptedPassword = sb.ToString();
				StringBuilder sbHash = new StringBuilder();
				sbHash.Append(username).Append(senderid).Append(message).Append(secureKey);
				byte[] genkey = Encoding.UTF8.GetBytes(sbHash.ToString());
				//static byte[] pwd = new byte[encPwd.Length];
				HashAlgorithm shaHas1 = HashAlgorithm.Create("SHA512");
				byte[] sec_key = shaHas1.ComputeHash(genkey);
				StringBuilder sbHash11 = new StringBuilder();
				for (int i = 0; i < sec_key.Length; i++)
				{
					sbHash11.Append(sec_key[i].ToString("x2"));
				}
				String Key = sbHash11.ToString();
				// String smsservicetype = "otpmsg"; //For OTP message.
				String smsservicetype = "singlemsg"; //For single message.
													 //String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
													 //                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
													 //                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
													 //                "&content=" + HttpUtility.UrlEncode(message.Trim()) +
													 //                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
													 //                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
													 //                "&key=" + HttpUtility.UrlEncode(Key.Trim());





				// added by Avneesh on 12-04-2021
				String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
								"&password=" + HttpUtility.UrlEncode(encryptedPassword) +
								"&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
								"&content=" + HttpUtility.UrlEncode(message.Trim()) +
								"&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
								"&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
								"&key=" + HttpUtility.UrlEncode(Key.Trim()) +
								"&templateid=" + HttpUtility.UrlEncode(templateid);
				//"&templateid=" + HttpUtility.UrlEncode("khanijonline02");





				byte[] byteArray = Encoding.ASCII.GetBytes(query);
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = byteArray.Length;
				//dataStream = request.GetRequestStream();
				//dataStream.Write(byteArray, 0, byteArray.Length);
				using (dataStream = request.GetRequestStream())
				{
					dataStream.Write(byteArray, 0, byteArray.Length);
				}
				dataStream.Close();
				WebResponse response = request.GetResponse();
				String Status = ((HttpWebResponse)response).StatusDescription;
				dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				String responseFromServer = reader.ReadToEnd();
				reader.Close();
				dataStream.Close();
				response.Close();
				sMSResponseModel.Status = "success";
				sMSResponseModel.SMSResponse = Status;
				sMSResponseModel.ResponseFromSMSSide = responseFromServer;
				sMSResponseModel.RequestSMSUrl = configuration.GetSection("SMSConfiguration").GetValue<string>("requesturl");
			}
			catch (Exception ex)
			{
				sMSResponseModel.Status = "failure";
				sMSResponseModel.SMSResponse = ex.Message;
				sMSResponseModel.RequestSMSUrl = configuration.GetSection("SMSConfiguration").GetValue<string>("requesturl");
			}
			return sMSResponseModel;
		}
		#endregion
	}
}
