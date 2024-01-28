using IntegrationModels;
using IntigrationWeb.Areas.OldDSC.Data;
using LoginModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using IntigrationWeb.Models.AzureHelperServices;

namespace IntigrationWeb.Areas.OldDSC.Controllers
{
    public class DSCResponseVerifyController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IDSCResponseVerifySubscriber _IDSCResponseVerifyModel;    
        private readonly IAzureFileSubscriber _azureFileOperation;
        string OutputResult = "";
        string CommonName = string.Empty;
        string SerialNo = string.Empty;
        string IssuerCommonName = string.Empty;
        string IssuedDate = string.Empty;
        string ExpiryDate = string.Empty;
        string Email = string.Empty;
        string Country = string.Empty;
        string CertificateClass = string.Empty;
        string Signature = string.Empty;

        public DSCResponseVerifyController(IConfiguration configuration, IAzureFileSubscriber azureFileOperation, IDSCResponseVerifySubscriber IDSCResponseVerifySubscriber)
        {
           
            _configuration = configuration;         
            _azureFileOperation = azureFileOperation;
            _IDSCResponseVerifyModel= IDSCResponseVerifySubscriber;
        }
        public bool getDSCResponse(string response, string DSCFor, int DSCForId)
        {
            var UserId = "1";//share this id as param
            DSCResponse objResponse = new DSCResponse();
           
            response = response.Replace("IssuerCommonName", " Issuer");
            response = response.Replace("CommonName", " CommonName").Replace("SerialNo", " SerialNo").Replace("IssuedDate", " IssuedDate").Replace("ExpiryDate", " ExpiryDate").Replace("Email", " Email").Replace("Country", " Country").Replace("CertificateClass", " CertificateClass").Replace("\r\n", "");
            string[] tokens = response.Split(new[] { " " }, StringSplitOptions.None);
            GetDSCRespnseData(tokens);
            objResponse.DSCCertificateClass = CertificateClass;
            objResponse.DSCSerialNo = SerialNo;
            objResponse.DSCCommonName = CommonName;
            objResponse.DSCIssuerCommonName = IssuerCommonName;
            objResponse.DSCCountry = Country;
            objResponse.DSCEmail = Email;
            objResponse.DSCExpiredDate = ExpiryDate;
            objResponse.DSCIssuedDate = IssuedDate;
            objResponse.Response = Signature;
            objResponse.DSCUsedBy = Convert.ToInt32(UserId);
            objResponse.DSCFor = DSCFor;
            objResponse.DSCForId = DSCForId;

            var ret = Convert.ToBoolean(_IDSCResponseVerifyModel.InsertDSCRespnseData(objResponse));
            return ret;

        }

        [HttpPost]
        public string CheckVerifyResponse(string contentType, string signDataBase64Encoded, string responseType)
        {
            try
            {
                if (signDataBase64Encoded.Contains("signing canceled"))
                {
                    OutputResult = "";
                }
                else
                {
                    signDataBase64Encoded = signDataBase64Encoded.Replace("IssuerCommonName", " Issuer");
                    signDataBase64Encoded = signDataBase64Encoded.Replace("CommonName", " CommonName").Replace("SerialNo", " SerialNo").Replace("IssuedDate", " IssuedDate").Replace("ExpiryDate", " ExpiryDate").Replace("Email", " Email").Replace("Country", " Country").Replace("CertificateClass", " CertificateClass").Replace("\r\n", "");
                    string[] tokens = signDataBase64Encoded.Split(new[] { " " }, StringSplitOptions.None);

                    string strSign = GetDSCRespnseData(tokens);

                    TempData["DSCResponse"] = signDataBase64Encoded;
                    TempData["Base64Data"] = strSign;
                    OutputResult = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                OutputResult = "EXCEPTION";
                //throw;
            }
            return OutputResult.ToUpper();
        }

        public string GetDSCRespnseData(string[] parameters)
        {
            string strSign = string.Empty;
            string[] strGetMerchantParamForCompare;
            for (int i = 0; i < parameters.Length; i++)
            {
                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "SIGNATURE")
                {
                    Signature = Convert.ToString(strGetMerchantParamForCompare[1]);
                    break;
                }
            }
            strSign = Signature;
            return strSign;
        }


        //usee api to save file
        public async Task<JsonResult> SavePdfFileAsync(string base64BinaryStr, string fileName, int ID, string UpdateIn, string Month_Year)
        {
            string OutputResult = "SUCCESS";
            try
            {
                var folderpath = _configuration.GetValue<string>("KeyList:FNDSCCreateFilePATH");
                try
                {
                    string strFinalString = string.Empty;
                    if (base64BinaryStr.Contains("Signature="))
                    {
                        string strWithoutSign = base64BinaryStr.Replace("Signature=", string.Empty);
                        strFinalString = strWithoutSign.Substring(0, strWithoutSign.IndexOf("CommonName"));
                        strFinalString = strFinalString.Replace("==", string.Empty).Replace("\r", string.Empty);
                    }
                    else if (base64BinaryStr.IndexOf("CommonName") > 0)
                    {
                        strFinalString = base64BinaryStr.Substring(0, base64BinaryStr.IndexOf("CommonName"));
                        strFinalString = strFinalString.Replace("==", string.Empty).Replace("\r", string.Empty);
                    }
                     else
                    {
                        strFinalString = base64BinaryStr;
                    }
                    //if ((System.IO.File.Exists(savepath)))
                    //{
                    //    string file = "File Already Exists";
                    //}
                    //else
                    {
                        //byte[] bytes = Convert.FromBase64String(strFinalString);
                        //System.IO.FileStream stream = new FileStream(savepath, FileMode.CreateNew);
                        //System.IO.BinaryWriter writer = new BinaryWriter(stream);
                        //writer.Write(bytes, 0, bytes.Length);
                        //writer.Close();


                        MyFileRequest myFileRequest = new MyFileRequest();
                        myFileRequest.FileContenctBase64 = base64BinaryStr;
                        myFileRequest.FileName = fileName;
                        myFileRequest.FolderName = folderpath;
                        myFileRequest.FileNameWithPath = folderpath + "\\" + fileName;
                        MessageEF e = await _azureFileOperation.SaveFile(myFileRequest);
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    OutputResult = "PDF File Save failed. Please try after some time";
                    return Json(OutputResult.ToUpper());
                }

           
            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
            }
            return Json(OutputResult.ToUpper());
        }

        //usee api to save file
        [HttpPost]
        public async Task<JsonResult> SaveNormalPdfFileAsync(string base64BinaryStr, string fileName)
        {
            string OutputResult = "SUCCESS";
            Item result = null;
            try
            {
                var folderpath = _configuration.GetValue<string>("KeyList:FNDSCReadFilePATH");
                //var RootPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["FNDSCReadFilePATH"]);
                //var savepath = Path.Combine(RootPath, fileName);
                //using (FileStream fs = new FileStream(savepath, FileMode.Create))
                //{
                //    using (BinaryWriter bw = new BinaryWriter(fs))
                //    {
                //        byte[] data = Convert.FromBase64String(base64BinaryStr);
                //        bw.Write(data);
                //        bw.Close();
                //    }
                //    fs.Close();
                //}
                MyFileRequest myFileRequest = new MyFileRequest();
                myFileRequest.FileContenctBase64 = base64BinaryStr;
                myFileRequest.FileName = fileName;
                myFileRequest.FolderName = folderpath;
                myFileRequest.FileNameWithPath = folderpath + "\\" + fileName;
                MessageEF e = await _azureFileOperation.SaveFile(myFileRequest);
                result = new Item { Message = OutputResult, FilePath = myFileRequest.FileNameWithPath };
                //result = new Item { Message = OutputResult, FilePath = configuration["DSCReadFilePath"] + "LTP_TRN_ID_0013172_2772022_16_48_48.pdf_2772022_16_48_55.pdf", FileName = fileName };

            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
                result = new Item { Message = OutputResult, FilePath = "NA", FileName = "NA" };
            }

            return Json(result);
        }

        public ActionResult OldDSC()
        {
          
            return View();
        }


    }
    public class Item
    {
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
