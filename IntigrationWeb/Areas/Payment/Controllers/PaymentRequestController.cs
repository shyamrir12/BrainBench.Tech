using DotNetIntegrationKit;
using IntegrationModels;
using IntigrationWeb.Areas.Payment.Data;
using IntigrationWeb.Models.EncryptDecrypt;
using IntigrationWeb.Models.sbiIncriptDecript;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Security.Claims;

namespace IntigrationWeb.Areas.Payment.Controllers
{
    public class PaymentRequestController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentRequestSubscriber _paymentRequestSubscriber;
        private readonly IsbiIncriptDecript _sbiIncriptDecript;
        private readonly IHttpContextAccessor _httpContextAccessor;
        RequestURL objRequestURL = new RequestURL();
        PaymentModel payment = new PaymentModel();
        MessageEF messageEF = new MessageEF();
        EncryptDecrypts objencdec = new EncryptDecrypts();
        public PaymentRequestController( IConfiguration configuration, IPaymentRequestSubscriber paymentRequestSubscriber, IsbiIncriptDecript sbiIncriptDecript, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _paymentRequestSubscriber = paymentRequestSubscriber;
            _sbiIncriptDecript = sbiIncriptDecript;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult LicensePayment(string Licensee_Id, string TotalPayment, string PaymentType)
        {
            PaymentRequestModel obj = new PaymentRequestModel();
            obj.Licensee_Id = Convert.ToInt32(Licensee_Id);
            decimal amount = decimal.Parse(TotalPayment);
            obj.TotalPayable = decimal.Round(amount, 2);
            obj.PaymentType = PaymentType;
            return View(obj);
        }

        [HttpPost]
        public IActionResult LicenseePayment(PaymentRequestModel obj)
        {
            //ShyamSir
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userid = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //ShyamSir
           
            obj.UserId = userid!=null?Convert.ToInt32(userid) :0;
            messageEF = _paymentRequestSubscriber.AddLicensePayment(obj);
            if (string.IsNullOrEmpty(messageEF.Satus))
            {
                TempData["Status"] = "0";
                TempData["Message"] = "Some error occured. Please try after sometime!";
                return Redirect(_configuration.GetValue<string>("KeyList:RedirecToMaster") + "/LicenseeTransfer/TransferRequest/TransferRequestView");
            }
            else
            {
                string response = string.Empty;
                obj.UserId = userid != null ? Convert.ToInt32(userid) : 0;
                //obj.UserLoginId = profile.UserLoginId;
                obj.TotalPayment = obj.TotalPayable;
                obj.PaymentBank = obj.PaymentBank;
                obj.PaymentMode = obj.PaymentMode;
                obj.PaymentRefId = messageEF.Satus;
                payment = _paymentRequestSubscriber.GetPaymentGateway(obj);

                #region SBI
                if (obj.PaymentBank == "SBI")
                {
                    var SBIRequestString = string.Empty;


                    objRequestURL.TPSLService = Convert.ToString(_configuration.GetSection("Path").GetSection("SBIService").Value); //Convert.ToString(ConfigurationManager.AppSettings["SBIService"]);
                    objRequestURL.LogFilePath = string.Empty;
                    objRequestURL.ErrorFile = string.Empty;

                    payment.TPSLTXNID = obj.PaymentRefId;
                    payment.Amount = Convert.ToDecimal(obj.TotalPayable).ToString("0.00");
                    payment.Shoppingcartdetails = "TEST_" + obj.TotalPayable + "_0.0";


                    SBIRequestString = "ApplicantName=" + payment.customerName.ToString() //ApplicantName
                                                          + "|EmailId=" + payment.Email.ToString() //EmailId
                                                          + "|MobileNo=" + payment.mobileNo.ToString() //MobileNo
                                                          + "|CLNT_TXN_NO=" + payment.MerchantTxnRefNumber.ToString() //CLNT_TXN_NO
                                                          + "|Amount=" + Convert.ToDecimal(obj.TotalPayable).ToString("0.00") //TXN_AMOUNT
                                                          + "|Payment_Bifurcation=" + payment.ITC
                                                          + "|ReturnUrl=" + Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrl").Value);








                    string checksum = _sbiIncriptDecript.GetSHA256(SBIRequestString);
                    SBIRequestString += "|checkSum=" + checksum;
                    string SBIPostRequest = _sbiIncriptDecript.EncryptWithKey(SBIRequestString);

                    string SBIURL = Convert.ToString(_configuration.GetSection("Path").GetSection("SBIService").Value);

                    #region Insert Payment Request Data
                    payment.UserId = userid != null ? Convert.ToInt32(userid) : 0; ;
                    payment.returnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrl").Value);
                    payment.S2SReturnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("S2SReturnURL").Value);
                    payment.PropertyPath = Convert.ToString(_configuration.GetSection("Path").GetSection("SBIPropertyPath").Value);
                    payment.PaymentEncryptedData = SBIPostRequest;
                    payment.Currencycode = "INR";
                    _paymentRequestSubscriber.InsertPaymentRequest(payment);
                    #endregion

                    var msgdisp = "<html><form name='s1_2' id='s1_2' action='" + SBIURL + "' method='post'> ";
                    msgdisp += "<input name='encdata' value='" + SBIPostRequest + "' type='hidden'>";
                    msgdisp += "<input name='merchant_code' value='MINERAL_DEPT' type='hidden'>";
                    msgdisp += "<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();";
                    msgdisp += "</script>";
                    msgdisp += "<script language='javascript' >";
                    msgdisp += "</ script >";
                    msgdisp += "</form></html> ";
                    return Content(msgdisp, "text/html");
                }
                #endregion

                #region--------------ICIC PaymentGateway Integration-----------------------
                if (obj.PaymentBank.ToUpper() == "ICICI")
                {
                    #region ICICI

                    var isNEFT = false;
                    if (obj.PaymentMode.ToUpper() == "NEFT")
                    {
                        isNEFT = true;
                    }
                    else
                    {

                        //string Nmode = NetBanking_mode;
                        isNEFT = false;

                        //------------------------------
                    }


                    objRequestURL.TPSLService = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICITPSLService").Value);
                    objRequestURL.LogFilePath = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICILogFilePath").Value);
                    objRequestURL.ErrorFile = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICIErrorFile").Value);



                    // payment.TPSLTXNID = "NA";
                    payment.ITC = "KO12:" + Convert.ToDecimal(obj.TotalPayable).ToString("0.00");
                    payment.MerchantTxnRefNumber = payment.MerchantTxnRefNumber.ToString();
                    payment.Amount = Convert.ToDecimal(obj.TotalPayable).ToString("0.00");
                    payment.Shoppingcartdetails = ("test_" + Convert.ToDecimal(obj.TotalPayable).ToString("0.00") + "_0.0").ToUpper();

                    #region NEFT / RTGS
                    if (isNEFT)
                    {

                        string finalSent = string.Empty;
                        var dat = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                        var dat1 = dat.Replace("-", "/");

                        #region New ICICI Bank Code 
                        string GIB_URL = "https://gibtaxcug.icicibank.com/corp/AuthenticationController?FORMSGROUP_ID__=AuthenticationFG&__START_TRAN_FLAG__=Y&FG_BUTTONS__=LOAD&ACTION.LOAD=Y&AuthenticationFG.LOGIN_FLAG=1&BANK_ID=ICI&JS_ENABLED_FLAG=Y&ITC=00000015&CRN=INR&IS_GIB_SESSION=Y&MD=P&AppType=corporate&URLencoding=Y&CG=Y&STATFLG=H&CYBERR=Y&CATEGORY_ID=264&PID=220";
                        string GIB_Parm = "Paymode=NEFT&DEP_CODE=MDGC&REF_NO=" + payment.MerchantTxnRefNumber + "&AMT=" + payment.Amount + "&RU=" + _configuration.GetSection("Path").GetSection("ReturnUrlICICI").Value.ToString() + "&SRC_ITC=" + payment.ITC + ",ApplicantName:" + payment.customerName.Trim();
                        string PlainString = GIB_URL + GIB_Parm;
                        string GIB_encrpt = objencdec.EncryptAES(GIB_Parm);
                        finalSent = GIB_URL + "&encdata=" + GIB_encrpt;
                        #endregion

                        #region Insert Payment Request Data
                        payment.UserId = userid != null ? Convert.ToInt32(userid) : 0;
                        payment.returnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrlICICI").Value);
                        payment.S2SReturnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("S2SReturnURLICICI").Value);
                        payment.PropertyPath = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICIPropertyPath").Value);
                        payment.PaymentEncryptedData = GIB_encrpt;
                        payment.Currencycode = "INR";
                        _paymentRequestSubscriber.InsertPaymentRequest(payment);

                        #endregion

                        var msgdisp1 = "<html><form name='s1_2' id='s1_2_3' action='" + finalSent + "' method='post'> ";
                        msgdisp1 += "<script type='text/javascript' language='javascript' >document.getElementById('s1_2_3').submit();";
                        msgdisp1 += "</script>";
                        msgdisp1 += "<script language='javascript' >";
                        msgdisp1 += "</ script >";
                        msgdisp1 += "</form></html> ";
                        return Content(msgdisp1, "text/html");



                        return null;

                    }
                    #endregion

                    #region NetBanking
                    else
                    {

                        //// Follwoing code for NetBanking
                        var PID = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICIPID").Value);
                        var ICICIreqString = "Action.ShoppingMall.Login.Init=Y&BankId=ICI&USER_LANG_ID=001&AppType=corporate&UserType=2?ShowOnSamePage=Y&MD=P&PID=" + PID + "&PRN=" + payment.ITC.Replace(" ", "") + ",REF_NO:" + payment.MerchantTxnRefNumber + "&ITC=" + Convert.ToString(_configuration.GetSection("Path").GetSection("ITC").Value) + "&AMT=" + payment.Amount + "&CRN=INR&CG=Y&CYBERR=Y&RU=" + Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrlICICI").Value);
                        var ICICINETBankingURL = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICITPSLService").Value) + ICICIreqString;

                        #region New ICICI Bank Code
                        ICICIreqString = "FORMSGROUP_ID__=AuthenticationFG&__START_TRAN_FLAG__=Y&FG_BUTTONS__=LOAD&ACTION.LOAD=Y&AuthenticationFG.LOGIN_FLAG=1&BANK_ID=ICI&JS_ENABLED_FLAG=Y&ITC=00000015&PRN=11&CRN=INR&MD=P&PID=220&IS_GIB_SESSION=Y&CATEGORY_ID=264&AppType=corporate&URLencoding=Y&CG=Y&CYBERR=Y";
                        ICICINETBankingURL = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICITPSLService").Value) + ICICIreqString;
                        string GIB_URL = ICICINETBankingURL;
                        string GIB_Parm = "PRN=" + payment.ITC + ",REF_NO:" + payment.MerchantTxnRefNumber + ",ApplicantName:" + payment.customerName.Trim() + "&AMT=" + payment.Amount + "&RU=" + Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrlICICI").Value);
                        string PlainURl = GIB_URL + GIB_Parm;
                        string GIB_encrpt = objencdec.EncryptAES(GIB_Parm);
                        ICICINETBankingURL = GIB_URL + "&encdata=" + GIB_encrpt;
                        #endregion


                        #region Insert Payment Request Data
                        payment.UserId = userid != null ? Convert.ToInt32(userid) : 0; ;
                        payment.returnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("ReturnUrlICICI").Value);
                        payment.S2SReturnURL = Convert.ToString(_configuration.GetSection("Path").GetSection("S2SReturnURLICICI").Value);
                        payment.PropertyPath = Convert.ToString(_configuration.GetSection("Path").GetSection("ICICIPropertyPath").Value);
                        payment.PaymentEncryptedData = GIB_encrpt;
                        payment.Currencycode = "INR";
                        _paymentRequestSubscriber.InsertPaymentRequest(payment);

                        #endregion

                        var msgdisp1 = "<html><form name='s1_2' id='s1_2' action='" + ICICINETBankingURL + "' method='post'> ";
                        msgdisp1 += "<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();";
                        msgdisp1 += "</script>";
                        msgdisp1 += "<script language='javascript' >";
                        msgdisp1 += "</ script >";
                        msgdisp1 += "</form></html> ";
                        return Content(msgdisp1, "text/html");

                        return null;
                    }
                    #endregion

                    #endregion
                }
                #endregion----------------------------------------------------------------


                return null;
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
