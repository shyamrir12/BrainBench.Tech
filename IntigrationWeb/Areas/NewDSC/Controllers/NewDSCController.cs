extern alias globalem;
using globalem::emBridge.Models.RequestData;
using globalem::emBridge.Models.ResponseData;
using globalem::emBridge;
using IntigrationWeb.Areas.NewDSC.Data;
using IntigrationWeb.Models.AzureHelperServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Areas.NewDSC.Controllers
{
    public class NewDSCController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly INewDSCSubscriber _newDSCModel;
        private readonly IAzureFileSubscriber _azureFileOperation;

        [Obsolete]
        public NewDSCController(IConfiguration configuration, INewDSCSubscriber newDSCModel, IAzureFileSubscriber azureFileOperation, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment=hostingEnvironment;
             _configuration = configuration;
            _newDSCModel = newDSCModel;
            _azureFileOperation = azureFileOperation;
        }
        // GET: /emBridgeArea/EmBridge/
        [Obsolete]
        [HttpPost]
        public ActionResult GetData(AjaxRetquest jsonData)
        {
            return View();
        }
        [Obsolete]
        [HttpPost]
        public ActionResult Decrypt(AjaxRetquest jsonData, string Type, string Data, string TempFilePath)
        {
            emBridgeLib bridge = new emBridgeLib(Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:licPath")), Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:logPath") ));
            try
            {
                var jsonReq = "";
                switch (Type)
                {
                    case "ListToken":
                        ResponseDataListProviderToken requestToken = bridge.decListToken(Data);
                        jsonReq = JsonConvert.SerializeObject(requestToken);
                        break;
                    case "ListCertificate":
                        CertificateFilter filter = new CertificateFilter();
                        filter.isNotExpired = true;
                        ResponseDataListPKCSCertificate requestCert = bridge.decListCertificate(Data, filter);
                        jsonReq = JsonConvert.SerializeObject(requestCert);
                        break;
                    case "PKCSSign":
                        ResponseDataPKCSSign requestSign = bridge.decPKCSSign(Data);
                        jsonReq = JsonConvert.SerializeObject(requestSign);
                        break;
                    case "PKCSBulkSign":
                        ResponseDataPKCSBulkSign requestBulkSign = bridge.decPKCSBulkSign(Data, TempFilePath);
                        System.IO.File.Delete(TempFilePath);
                        jsonReq = JsonConvert.SerializeObject(requestBulkSign);
                        break;
                }
                return Content(jsonReq);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
                //throw;
            }
        }
        [HttpPost]
        public ActionResult Encrypt(AjaxRetquest jsonData, string Data, string Type)
        {
            //_configuration.GetValue<string>("KeyList:FNDSCCreateFilePATH")
         
            emBridgeLib bridge = new emBridgeLib(Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:licPath")), Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:logPath")));

            try
            {
                
                var jsonReq = "";
                switch (Type)
                {
                    case "ListToken":
                        ListTokenRequest listTokenRequest = JsonConvert.DeserializeObject<ListTokenRequest>(Data);
                        Request requestToken = bridge.encListToken(listTokenRequest);
                        jsonReq = JsonConvert.SerializeObject(requestToken);
                        break;
                    case "ListCertificate":
                        ListCertificateRequest listCertificateRequest = JsonConvert.DeserializeObject<ListCertificateRequest>(Data);
                        Request requestCert = bridge.encListCertificate(listCertificateRequest);
                        jsonReq = JsonConvert.SerializeObject(requestCert);
                        break;
                    case "PKCSSign":
                        PKCSSignRequest pKCSSignRequest = JsonConvert.DeserializeObject<PKCSSignRequest>(Data);
                        Request requestSign = bridge.encPKCSSign(pKCSSignRequest);
                        jsonReq = JsonConvert.SerializeObject(requestSign);
                        break;
                    case "PKCSBulkSign":
                        string path = Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:FNDSCCreateFilePATH"));
                        PKCSBulkPdfHashSignRequest pKCSBulkPdfHashSignRequest = JsonConvert.DeserializeObject<PKCSBulkPdfHashSignRequest>(Data);
                        pKCSBulkPdfHashSignRequest.TempFolder = Path.Combine(_hostingEnvironment.WebRootPath, _configuration.GetValue<string>("KeyList:FNDSCReadFilePATH"););
                        List<emBridgeSignerInput> bulkInput = new List<emBridgeSignerInput>();
                        TestDSC testDSC = JsonConvert.DeserializeObject<TestDSC>(Data);
                        if (testDSC.PdfFileBase64.Length > 0)
                        {
                            emBridgeSignerInput input = new emBridgeSignerInput(testDSC.PdfFileBase64, testDSC.PDFName, "Bengaluru", "Testing", "eMudhra Limited", true, globalem.emBridge.Enum.PageTobeSigned.First, globalem.emBridge.Enum.Coordinates.BottomRight);
                            bulkInput.Add(input);
                        }
                        else if (testDSC.PDFName != null) // for single pdf sign
                        {
                            string docID = Path.GetFileName(path + "\\" + testDSC.PDFName);
                            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "\\" + testDSC.PDFName);
                            string fileBas64 = Convert.ToBase64String(fileBytes);
                           
                            emBridgeSignerInput input = new emBridgeSignerInput(fileBas64, docID, "Bengaluru", "Testing", "eMudhra Limited", true, globalem.emBridge.Enum.PageTobeSigned.First, globalem.emBridge.Enum.Coordinates.BottomRight);
                            bulkInput.Add(input);
                        }
                        
                        pKCSBulkPdfHashSignRequest.BulkInput = bulkInput;
                        Request requestPkcsBulkSign = bridge.encPKCSBulkSign(pKCSBulkPdfHashSignRequest);
                        jsonReq = JsonConvert.SerializeObject(requestPkcsBulkSign);

                        break;
                       
                }

                return Content(jsonReq);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FileResult> DownloadFileFromAzure(string path)
        {
            MyFileRequest fr = new MyFileRequest();
            fr.FileNameWithPath=path;
            MyFileResult fres = await _azureFileOperation.DownloadFile(fr);
            var bytes = Convert.FromBase64String(fres.FileBase64);
            var contents = new MemoryStream(bytes);
            return File(contents, fres.ContentType, fres.FileName);
        }

        public ActionResult NewDSC()
        {
          
            return View();
        }

        public ActionResult Test()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult SetViewBag(AjaxRetquest ajaxRetquest)
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SaveNormalPdfFile(string base64BinaryStr, string fileName, int ID, string UpdateIn)
        {
            string OutputResult = "SUCCESS";
            Item result = null;
            var folderpath = _configuration.GetValue<string>("KeyList:FNDSCReadFilePATH");
            try
            {
                //appsettings.json
                var RootPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["FNDSCReadFilePATH"]);
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


            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
                result = new Item { Message = OutputResult, FilePath = "NA", FileName = "NA" };
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SavePdfFileAsync(string base64BinaryStr, string fileName, int ID, string UpdateIn, string Month_Year)
        {
            string OutputResult = "SUCCESS";
            var UserId = "2";//send id when request
            var folderpath = _configuration.GetValue<string>("KeyList:FNDSCCreateFilePATH");
            try
            {
              
                try
                {
                    #region Old Code To Check base 64 file
                    string strFinalString = string.Empty;
                    string base64Finale = string.Empty;
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
                    #endregion
                    base64Finale = strFinalString.Replace("\n", "");

                    MyFileRequest myFileRequest = new MyFileRequest();
                    myFileRequest.FileContenctBase64 = base64Finale;
                    myFileRequest.FileName = fileName;
                    myFileRequest.FolderName = folderpath;
                    myFileRequest.FileNameWithPath = folderpath + "\\" + fileName;
                    MessageEF e = await _azureFileOperation.SaveFile(myFileRequest);
                  
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    OutputResult = "PDF File Save failed. Please try after some time";
                    return Json(OutputResult.ToUpper());
                }

              
                NewDSCRequest newDSC = new NewDSCRequest();
                newDSC.FileName = folderpath + "\\" + fileName; ;
                newDSC.ID = ID.ToString();
                newDSC.UpdateIn = UpdateIn;
                newDSC.UserId = UserId;
                newDSC.MonthYear = Month_Year;
                //api not aaded in intigration api
                MessageEF msgEF = _newDSCModel.SaveDSCFilePath(newDSC);
            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
            }
            return Json(OutputResult.ToUpper());
        }


    }
    public class Item
    {
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
