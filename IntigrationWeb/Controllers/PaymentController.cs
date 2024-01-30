using IntegrationModels;
using IntigrationWeb.Models.EncryptDecrypt;
using IntigrationWeb.Models.MailSMSServices;
using IntigrationWeb.Models.PaymentResponsesService;
using IntigrationWeb.Models.sbiIncriptDecript;
using LoginModels;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;

namespace IntigrationWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        #region Variable Declaration
        EncryptDecrypts objencdec = new EncryptDecrypts();
        MessageEF messageEF = new MessageEF();
        SMS sMS = new SMS();
        CommonMail commonMail = new CommonMail();
        PaymentTransaction response = new PaymentTransaction();
        List<FinalPaymentModel> objList = new List<FinalPaymentModel>();
        UserMasterModel userMasterModel=new UserMasterModel ();
        string strHEX, strPGActualReponseWithChecksum, strPGActualReponseEncrypted, strPGActualReponseDecrypted, strPGresponseChecksum, strPGTxnStatusCode;
        string PayableRoyalty, TCS, Cess, eCess, DMF, NMET, MonthlyPeriodicAmount, LicenseAmount, GIB_TXN_NO;
        
        string[] strPGChecksum, strPGTxnString;
        bool isDecryptable = false;
        string strPG_TxnStatus = string.Empty, strPG_TxnStatusDesc = string.Empty,
               strPG_ClintTxnRefNo = string.Empty,
              strPG_TPSLTxnBankCode = string.Empty, strPG_Paymode = string.Empty,
              strPG_TPSLTxnID = string.Empty,
              strPG_TxnAmount = string.Empty,
              strPG_TxnDateTime = string.Empty,
              strPG_TxnDate = string.Empty,
              strPG_TxnTime = string.Empty;

        string strPGResponse;
        string strPG_MerchantCode;
        string[] strSplitDecryptedResponse;
        string[] strArrPG_TxnDateTime;
       
        #endregion
        private readonly IPaymentResponsesSubscriber _paymentResponsesSubscriber;
        private readonly IMailSMSSubscriber _mailSMSSubscriber;
        private readonly IsbiIncriptDecript _isbiIncriptDecript;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public PaymentController(IConfiguration configuration, IPaymentResponsesSubscriber paymentResponsesSubscriber, IMailSMSSubscriber mailSMSSubscriber, IsbiIncriptDecript isbiIncriptDecript, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _paymentResponsesSubscriber = paymentResponsesSubscriber;
            _mailSMSSubscriber = mailSMSSubscriber;
            _isbiIncriptDecript = isbiIncriptDecript;
            _httpContextAccessor = httpContextAccessor;
        }
        #region----------------SBI Bank Response Code------------------------------
        public IActionResult PaymentResponse()
        {
            //ShyamSir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userid = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            UserLoginSession profile = new UserLoginSession();//get hear user by id
            //ShyamSir
           
            try
            {
                string strPaymentResponseID = string.Empty;
                string strDecryptedVal = string.Empty;
                //Decrypting the PG response
                string strIsKey = string.Empty;
                string strIsIv = string.Empty;
                string documentContents = null;
                string sessionUserName = string.Empty;
                //Verify Response using Key and Iv 
                #region Insert Payment Response Received in request from the bank Site
                try
                {

                    var bnk = "";

                    using (Stream receiveStream = HttpContext.Request.Body)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8))
                        {
                            documentContents = readStream.ReadToEnd();
                        }
                    }


                    if (!string.IsNullOrEmpty(documentContents) && documentContents.Contains("encdata"))
                    {
                        bnk = "SBI";
                    }

                    if (!string.IsNullOrEmpty(documentContents))
                    {
                        messageEF = _paymentResponsesSubscriber.GetPaymentResponseID(new PaymentResponse() { SessionBank = bnk, DocContent = documentContents });
                        strPaymentResponseID = messageEF.Satus;
                    }

                }
                catch { }

                #endregion

                #region NEW SBI

                if ((documentContents != null || documentContents != ""))
                {
                    response.TPSL_BANK_CD = "SBI";
                    documentContents = HttpUtility.UrlDecode(documentContents.Replace("encdata=", string.Empty));
                    strDecryptedVal = _isbiIncriptDecript.DecryptWithKey(documentContents);
                    strSplitDecryptedResponse = strDecryptedVal.Split('|');
                    SBINETGetPGRespnseData(strSplitDecryptedResponse);
                }

                #endregion

               
                #region Update responce Data

                if (strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc.ToUpper().Contains("NEFT") || strPG_TxnStatusDesc.ToUpper() == "P" || strPG_TxnStatusDesc.ToUpper() == "RECEIVED" || strPG_TxnStatusDesc.ToUpper() == "H")
                {
                    strPG_TxnStatusDesc = "PENDING";
                    response.TXN_STATUS = "PENDING";
                }
                if (strPG_TxnStatusDesc.ToUpper().Contains("COMPLETED") || strPG_TxnStatusDesc.ToUpper().Contains("SUCCESS"))
                {
                    strPG_TxnStatusDesc = "SUCCESS";
                    response.TXN_STATUS = "SUCCESS";
                }

                if (response.TXN_STATUS == "SUCCESS" || response.TXN_STATUS == "AWAITED" || strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc == "NEFT" || strPG_TxnStatusDesc.ToUpper() == "RECEIVED" || strPG_TxnStatusDesc.ToUpper() == "H") // if transaction is success then only update record for bulkpermit and sent sms and email.
                {
                    if (response.CLNT_TXN_REF.StartsWith("LP"))
                    {
                        string strLICTXNID = response.CLNT_TXN_REF.Trim();
                        string paymentStatus = response.TXN_STATUS;
                         userMasterModel = _paymentResponsesSubscriber.AddLicensePaymentResponce(
                           new PaymentResponse()
                           {
                               PaymentRecieptId = strLICTXNID,
                               UserId = profile.UserID,
                               ChallanNumber = response.TPSL_TXN_ID,
                               PaidAmount = response.TXN_AMT,
                               PaymentStatus = paymentStatus
                           });
                        
                    }
                    if (strPG_TxnStatus == "SUCCESS" || strPG_TxnStatusDesc.ToUpper().Contains("COMPLETED"))//Success
                    {
                        #region Send SMS

                        try
                        {
                            string message = "Dear " + userMasterModel.ApplicantName + Environment.NewLine + "Your payment has been proceed successfully and your payment status is " + response.TXN_STATUS
                                                 + Environment.NewLine +
                                                 "Your Transaction Reference Number is " + response.CLNT_TXN_REF + Environment.NewLine +
                                                 "Use this for Future reference. Please login to Khanij Online portal for further. CHiMMS, GoCG";


                            string templateid = "1307161883628029116";
                          _mailSMSSubscriber.Main(new SMS() { mobileNo = userMasterModel.MobileNo, message = message, templateid = templateid });
                        }
                        catch (Exception)
                        {
                        }
                        #endregion

                        #region Send Mail
                        try
                        {
                            _mailSMSSubscriber.SendCommonMail(new CommonMail()
                            {
                                //PaymentReceiptID = profile.UserID.ToString(),
                                //EmailID = userMasterModel.EMailId,
                                //ForwardDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                //TransactionId = profile.UserID.ToString(),
                                //ApplicantName = userMasterModel.ApplicantName,
                                //PaymentType = "payment status is " + response.TXN_STATUS + " ",
                                //PayableAmount = Convert.ToString(response.TXN_AMT)
                            });
                        }
                        catch (Exception)
                        {
                            TempData["AckMessage"] = "0";
                        }
                        #endregion
                    }
                    else if (response.TXN_STATUS == "AWAITED" || strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc == "NEFT")//Awaited
                    {
                        #region Send SMS
                        try
                        {

                            string message = "Dear " + userMasterModel.ApplicantName + Environment.NewLine + "Your payment has been proceed successfully and your payment status is " + response.TXN_STATUS
                                                + Environment.NewLine +
                                                "Your Transaction Reference Number is " + response.CLNT_TXN_REF + Environment.NewLine +
                                                "Use this for Future reference. Please login to Khanij Online portal for further. CHiMMS, GoCG";


                            string templateid = "1307161883628029116";
                            _mailSMSSubscriber.Main(new SMS() { mobileNo = userMasterModel.MobileNo, message = message, templateid = templateid });
                        }
                        catch (Exception)
                        {

                        }
                        #endregion

                        #region Send Mail
                        try
                        {
                            _mailSMSSubscriber.SendCommonMail(new CommonMail()
                            {
                                //EmailId = userMasterModel.EMailId,
                                //CLNT_TXN_REF = response.CLNT_TXN_REF,
                                //TransporterName = userMasterModel.ApplicantName
                            });
                        }
                        catch (Exception)
                        {
                            TempData["AckMessage"] = "0";
                        }
                        #endregion
                    }
                }

                #endregion


                ViewBag.PaymentDetails = objList;
                return View(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            ViewBag.PaymentDetails = objList;
            return View();
        }
        #endregion--------------------------------------------------------------------

        #region SBI Bifurcation
        public void SBINETGetPGRespnseData(string[] parameters)
        {
            try
            {
                string[] strGetMerchantParamForCompare;
                strPG_Paymode = "ONLINE";
                strPG_TxnDateTime = System.DateTime.Now.ToString();
                strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);

                for (int i = 0; i < parameters.Length; i++)
                {
                    strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                    if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_NO")
                    {
                        strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }
                    else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "AMOUNT") // TXN_AMOUNT
                    {
                        strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }
                    else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXREFNO") // Bank Transaction Number
                    {
                        strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }
                    else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXSTATUS")
                    {
                        strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }
                    else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXSTATUSDESC")
                    {
                        strPG_TxnStatusDesc = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }

                    else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "PAYMENT_BIFURCATION")
                    {
                        string metaData = Convert.ToString(strGetMerchantParamForCompare[1]);
                        string[] obj = strGetMerchantParamForCompare[1].Split(':');

                        if (obj != null && obj.Length > 0)
                        {
                            for (int j = 0; j < obj.Length; j++)
                            {
                                try
                                {
                                    FinalPaymentModel objModel = new FinalPaymentModel();
                                    objModel.PaymentAmount = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    objModel.PaymentType = Convert.ToString(obj[j].Split('~')[0]).Trim();
                                    objList.Add(objModel);

                                    if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO01" || Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO02")
                                    {
                                        PayableRoyalty = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO03")
                                    {
                                        TCS = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO04")
                                    {
                                        Cess = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO05")
                                    {
                                        eCess = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "DMF")
                                    {
                                        DMF = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO31")
                                    {
                                        NMET = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }
                                    else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO19")
                                    {
                                        MonthlyPeriodicAmount = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                    }

                                }
                                catch { }
                            }
                        }
                    }


                }
            }
            catch (Exception Exception)
            {
            }

        }
        #endregion

        #region----------------ICICI Bank Response Code------------------------------
        public ActionResult PaymentResponseICICI()
        {

          
            //ShyamSir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userid = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            UserLoginSession profile = new UserLoginSession();//get hear user by id
            //ShyamSir
            try
            {
                PaymentTransaction response = new PaymentTransaction();
                string strPaymentResponseID = string.Empty;
                string strDecryptedVal = string.Empty;
                //Decrypting the PG response
                string strIsKey = string.Empty;
                string strIsIv = string.Empty;
                string documentContents = null;
                string sessionUserName = string.Empty;
                #region Insert Payment Response Received in request from the bank Site
                try
                {

                    var bnk = "";
                    using (Stream receiveStream = HttpContext.Request.Body)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8))
                        {
                            documentContents = readStream.ReadToEnd();
                        }
                    }



                    if (!string.IsNullOrEmpty(documentContents) && documentContents.Contains("encdata"))
                    {
                        bnk = "ICIC";
                    }

                    if (!string.IsNullOrEmpty(documentContents))
                    {
                       
                        messageEF = _paymentResponsesSubscriber.GetPaymentResponseID(new PaymentResponse() { SessionBank = bnk, DocContent = documentContents });
                        strPaymentResponseID = messageEF.Satus;
                    }
                }
                catch { }
                #endregion

                #region NEW ICICI

                response.TPSL_BANK_CD = "ICICI";

                if (!string.IsNullOrEmpty(documentContents) && documentContents.Contains("encdata")) //// For Online Net Banking
                {

                    strDecryptedVal = objencdec.DecryptAES(HttpUtility.UrlDecode(documentContents.Replace("encdata=", "")));

                    strSplitDecryptedResponse = strDecryptedVal.Split('|');

                    ICICINETGetPGRespnseData(strSplitDecryptedResponse);
                }
                else  // For NEFT / RTGS
                {
                    strIsKey = Convert.ToString(_configuration.GetValue<string>("PaymentKeyList:NEFTICICIKey"));
                    strIsIv = Convert.ToString(_configuration.GetValue<string>("PaymentKeyList:NEFTICICIIV"));

                    strDecryptedVal = objencdec.DecryptAES(HttpUtility.UrlDecode(documentContents.Replace("encdata=", "")));

                    strSplitDecryptedResponse = strDecryptedVal.Split('|');
                    ICICIGetPGRespnseData(strSplitDecryptedResponse);
                    strPG_TxnStatusDesc = strPG_TxnStatusDesc == "P" ? "NEFT" : strPG_TxnStatusDesc;
                }


                #endregion

                #region Payment Data Updated on specific request

                if (strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc.ToUpper().Contains("NEFT") || strPG_TxnStatusDesc.ToUpper() == "P" || strPG_TxnStatusDesc.ToUpper() == "RECEIVED" || strPG_TxnStatusDesc.ToUpper() == "H")
                {
                    strPG_TxnStatusDesc = "PENDING";
                    response.TXN_STATUS = "PENDING";
                }
                //Note:Difference Sunil-change (compare with 1.0)
                if (strPG_TxnStatusDesc.ToUpper().Contains("COMPLETED") || strPG_TxnStatusDesc.ToUpper().Contains("SUCCESS"))
                {
                    strPG_TxnStatusDesc = "SUCCESS";
                    response.TXN_STATUS = "SUCCESS";
                }
                if (response.TXN_STATUS == "SUCCESS" || response.TXN_STATUS == "AWAITED" || strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc == "NEFT" || strPG_TxnStatusDesc.ToUpper() == "RECEIVED" || strPG_TxnStatusDesc.ToUpper() == "H") // if transaction is success then only update record for bulkpermit and sent sms and email.
                {
                    if (response.CLNT_TXN_REF.StartsWith("LP"))
                    {
                        string strLICTXNID = response.CLNT_TXN_REF.Trim();
                        string paymentStatus = response.TXN_STATUS;
                        userMasterModel = _paymentResponsesSubscriber.AddLicensePaymentResponce(
                          new PaymentResponse()
                          {
                              PaymentRecieptId = strLICTXNID,
                              UserId = profile.UserID,
                              ChallanNumber = response.TPSL_TXN_ID,
                              PaidAmount = response.TXN_AMT,
                              PaymentStatus = paymentStatus
                          });

                    }
                    if (strPG_TxnStatus == "SUCCESS" || strPG_TxnStatusDesc.ToUpper().Contains("COMPLETED"))//Success
                    {
                        #region Send SMS

                        try
                        {
                            string message = "Dear " + userMasterModel.ApplicantName + Environment.NewLine + "Your payment has been proceed successfully and your payment status is " + response.TXN_STATUS
                                                 + Environment.NewLine +
                                                 "Your Transaction Reference Number is " + response.CLNT_TXN_REF + Environment.NewLine +
                                                 "Use this for Future reference. Please login to Khanij Online portal for further. CHiMMS, GoCG";


                            string templateid = "1307161883628029116";
                            _mailSMSSubscriber.Main(new SMS() { mobileNo = userMasterModel.MobileNo, message = message, templateid = templateid });
                        }
                        catch (Exception)
                        {
                        }
                        #endregion

                        #region Send Mail
                        try
                        {
                            _mailSMSSubscriber.SendCommonMail(new CommonMail()
                            {
                                //PaymentReceiptID = profile.UserID.ToString(),
                                //EmailID = userMasterModel.EMailId,
                                //ForwardDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                //TransactionId = profile.UserID.ToString(),
                                //ApplicantName = userMasterModel.ApplicantName,
                                //PaymentType = "payment status is " + response.TXN_STATUS + " ",
                                //PayableAmount = Convert.ToString(response.TXN_AMT)
                            });
                        }
                        catch (Exception)
                        {
                            TempData["AckMessage"] = "0";
                        }
                        #endregion
                    }
                    else if (response.TXN_STATUS == "AWAITED" || strPG_TxnStatusDesc.ToUpper().Contains("PENDING") || strPG_TxnStatusDesc == "NEFT")//Awaited
                    {
                        #region Send SMS
                        try
                        {

                            string message = "Dear " + userMasterModel.ApplicantName + Environment.NewLine + "Your payment has been proceed successfully and your payment status is " + response.TXN_STATUS
                                                + Environment.NewLine +
                                                "Your Transaction Reference Number is " + response.CLNT_TXN_REF + Environment.NewLine +
                                                "Use this for Future reference. Please login to Khanij Online portal for further. CHiMMS, GoCG";


                            string templateid = "1307161883628029116";
                            _mailSMSSubscriber.Main(new SMS() { mobileNo = userMasterModel.MobileNo, message = message, templateid = templateid });
                        }
                        catch (Exception)
                        {

                        }
                        #endregion

                        #region Send Mail
                        try
                        {
                            _mailSMSSubscriber.SendCommonMail(new CommonMail()
                            {
                                //EmailId = userMasterModel.EMailId,
                                //CLNT_TXN_REF = response.CLNT_TXN_REF,
                                //TransporterName = userMasterModel.ApplicantName
                            });
                        }
                        catch (Exception)
                        {
                            TempData["AckMessage"] = "0";
                        }
                        #endregion
                    }
                }
                #endregion
                ViewBag.PaymentDetails = objList;
               
                return View(response);
               
            }
            catch (Exception ex) { }

            ViewBag.PaymentDetails = objList;

            return View();

        }

        #endregion--------------------------------------------------------------------


        #region ICICI Bifurcation
        public void ICICINETGetPGRespnseData(string[] parameters)
        {
            string[] strGetMerchantParamForCompare;
            strPG_TxnDateTime = System.DateTime.Now.ToString();
            strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
            strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
            strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
            for (int i = 0; i < parameters.Length; i++)
            {
                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "REF_NO")
                {
                    strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "AMOUNT")
                {
                    strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "PAYMODE")
                {
                    strPG_Paymode = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "ICICI_REF_NO")
                {
                    strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "STATUS_DESC")
                {
                    strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "STATUS")
                {
                    if (Convert.ToString(strGetMerchantParamForCompare[1]).ToUpper() == "S")
                    {
                        strPG_TxnStatusDesc = "SUCCESS";
                    }
                    else
                    {
                        strPG_TxnStatusDesc = Convert.ToString(strGetMerchantParamForCompare[1]);
                    }

                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "GIB_TXN_ID")
                {
                    GIB_TXN_NO = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "PRN")
                {
                    string metaData = Convert.ToString(strGetMerchantParamForCompare[1]);
                    string[] obj = strGetMerchantParamForCompare[1].Split(',');

                    if (obj != null && obj.Length > 0)
                    {
                        for (int j = 0; j < obj.Length; j++)
                        {
                            try
                            {
                                FinalPaymentModel objModel = new FinalPaymentModel();
                                objModel.PaymentAmount = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                objModel.PaymentType = Convert.ToString(obj[j].Split(':')[0]).Trim();
                                objList.Add(objModel);

                                if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO01" || Convert.ToString(obj[j].Split('~')[0]).Trim() == "KO02")
                                {
                                    PayableRoyalty = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO03")
                                {
                                    TCS = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO04")
                                {
                                    Cess = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO05")
                                {
                                    eCess = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "DMF")
                                {
                                    DMF = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO31")
                                {
                                    NMET = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO19")
                                {
                                    MonthlyPeriodicAmount = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "REF_NO")
                                {
                                    strPG_ClintTxnRefNo = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        public void ICICIGetPGRespnseData(string[] parameters)
        {
            string[] strGetMerchantParamForCompare;
            strPG_TxnDateTime = System.DateTime.Now.ToString();
            strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
            strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
            strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
            for (int i = 0; i < parameters.Length; i++)
            {
                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "REF_NO")
                {
                    strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "AMOUNT")
                {
                    strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "PAYMODE")
                {
                    strPG_Paymode = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "ICICI_REF_NO")
                {
                    strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "STATUS_DESC")
                {
                    strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "STATUS")
                {
                    strPG_TxnStatusDesc = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "GIB_TXN_ID")
                {
                    GIB_TXN_NO = Convert.ToString(strGetMerchantParamForCompare[1]);
                }




                //else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
                //{
                //    strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                //    strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                //    strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                //    strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                //}
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "SRC_ITC")
                {
                    string metaData = Convert.ToString(strGetMerchantParamForCompare[1]);
                    string[] obj = strGetMerchantParamForCompare[1].Split(',');

                    if (obj != null && obj.Length > 0)
                    {
                        for (int j = 0; j < obj.Length; j++)
                        {
                            try
                            {
                                FinalPaymentModel objModel = new FinalPaymentModel();
                                objModel.PaymentAmount = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                objModel.PaymentType = Convert.ToString(obj[j].Split(':')[0]).Trim();
                                objList.Add(objModel);

                                //if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "PayableRoyalty")
                                //{
                                //    PayableRoyalty = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "TCS")
                                //{
                                //    TCS = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "Cess")
                                //{
                                //    Cess = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "eCess")
                                //{
                                //    eCess = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "DMF")
                                //{
                                //    DMF = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "NMET")
                                //{
                                //    NMET = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}
                                //else if (Convert.ToString(obj[j].Split('~')[0]).Trim() == "Monthly Periodic Amount")
                                //{
                                //    MonthlyPeriodicAmount = Convert.ToString(obj[j].Split('~')[1]).ToString();
                                //}


                                if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO01" || Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO02")
                                {
                                    PayableRoyalty = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO03")
                                {
                                    TCS = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO04")
                                {
                                    Cess = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO05")
                                {
                                    eCess = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "DMF")
                                {
                                    DMF = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO31")
                                {
                                    NMET = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }
                                else if (Convert.ToString(obj[j].Split(':')[0]).Trim() == "KO19")
                                {
                                    MonthlyPeriodicAmount = Convert.ToString(obj[j].Split(':')[1]).ToString();
                                }



                            }
                            catch { }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
