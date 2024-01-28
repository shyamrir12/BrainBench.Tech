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
            //emBridgeLib bridge = new emBridgeLib(ConfigurationSettings.AppSettings.Get("licPath"), ConfigurationSettings.AppSettings.Get("logPath"));
            emBridgeLib bridge = new emBridgeLib(Path.Combine(_hostingEnvironment.WebRootPath, _configuration["licPath"]), Path.Combine(_hostingEnvironment.WebRootPath, _configuration["logPath"]));
            ;
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
            string s = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["licPath"]);
            string ss = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["logPath"]);
            emBridgeLib bridge = new emBridgeLib(Path.Combine(_hostingEnvironment.WebRootPath, _configuration["licPath"]), Path.Combine(hostingEnvironment.WebRootPath, configuration["logPath"]));
            try
            {
                //JObject jObject = JObject.Parse(jsonData);
                //var jData = jObject["datatosend"];
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
                        //string path = ConfigurationSettings.AppSettings.Get("PdfInputPath");
                        string path = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["PdfInputPath"]);
                        PKCSBulkPdfHashSignRequest pKCSBulkPdfHashSignRequest = JsonConvert.DeserializeObject<PKCSBulkPdfHashSignRequest>(Data);
                        //pKCSBulkPdfHashSignRequest.TempFolder = ConfigurationSettings.AppSettings.Get("tempDirectory");
                        pKCSBulkPdfHashSignRequest.TempFolder = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["tempDirectory"]);
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
                            //emBridgeSignerInput input = new emBridgeSignerInput(fileBas64, docID, "Bengaluru", "Testing", "eMudhra Limited", true, emBridge.Enum.PageTobeSigned.First, emBridge.Enum.Coordinates.BottomRight);
                            emBridgeSignerInput input = new emBridgeSignerInput(fileBas64, docID, "Bengaluru", "Testing", "eMudhra Limited", true, globalem.emBridge.Enum.PageTobeSigned.First, globalem.emBridge.Enum.Coordinates.BottomRight);
                            bulkInput.Add(input);
                        }
                        else
                        {
                            //if (Directory.Exists(path)) // for bulk pdf sign in folder
                            //{
                            //    string[] filePaths = Directory.GetFiles(path, "*.pdf");
                            //    foreach (string filePath in filePaths)
                            //    {
                            //        string docID = Path.GetFileName(filePath);
                            //        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                            //        string fileBas64 = Convert.ToBase64String(fileBytes);
                            //        //emBridgeSignerInput input = new emBridgeSignerInput(fileBas64, docID, "Bengaluru", "Testing", "eMudhra Limited", true, emBridge.Enum.PageTobeSigned.First, emBridge.Enum.Coordinates.BottomRight);
                            //        emBridgeSignerInput input = new emBridgeSignerInput(fileBas64, docID, "Bengaluru", "Testing", "eMudhra Limited", true, globalem.emBridge.Enum.PageTobeSigned.First, globalem.emBridge.Enum.Coordinates.BottomRight);
                            //        bulkInput.Add(input);
                            //    }
                            //}
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

            //save file in azure (Sunil DEshalahre 27-078-22)
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
        public JsonResult SaveNormalPdfFile(string base64BinaryStr, string fileName, int ID, string UpdateIn)
        {
            string OutputResult = "SUCCESS";
            try
            {
                //appsettings.json
                var RootPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["FNDSCReadFilePATH"]);
                var savepath = Path.Combine(RootPath, fileName);
                using (FileStream fs = new FileStream(savepath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64BinaryStr);
                        bw.Write(data);
                        bw.Close();
                    }
                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
            }
            return Json(OutputResult.ToUpper());
        }

        [HttpPost]
        public JsonResult SavePdfFile(string base64BinaryStr, string fileName, int ID, string UpdateIn, string Month_Year)
        {
            string OutputResult = "SUCCESS";
            var UserId = "2";//send id when request
            try
            {
                var ServerPath = "/DSC/WithDSC/" + fileName;
                var RootPath = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["FNDSCCreateFilePATH"]);
                var savepath = Path.Combine(RootPath, fileName);
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

                    #region New Code added by sunil deshalahre(on 02-07-22)
                    //string strFinalString = string.Empty;

                    #endregion

                    if ((System.IO.File.Exists(savepath)))
                    {
                        string file = "File Already Exists";
                    }
                    else
                    {
                        base64Finale = strFinalString.Replace("\n", "");
                        byte[] bytes = Convert.FromBase64String(base64Finale.Trim());
                        System.IO.FileStream stream = new FileStream(savepath, FileMode.CreateNew);
                        System.IO.BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(bytes, 0, bytes.Length);
                        writer.Close();
                    }
                    base64Finale = strFinalString.Replace("\n", "");
                    //save file in azure (Sunil DEshalahre 27-078-22)
                    System.Threading.Tasks.Task<MessageEF> e = _azureFileOperation.SaveFile(new MyFileRequest() { FileContenctBase64 = base64Finale, FileName = fileName, FolderName = "DSC/WithDSC" });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    OutputResult = "PDF File Save failed. Please try after some time";
                    return Json(OutputResult.ToUpper());
                }

                //update to DB code pending (02-07-22) Sunil Deshalahre
                //Update to DB code updated on (06-07-22) Sunil Deshalahre
                NewDSCRequest newDSC = new NewDSCRequest();
                newDSC.FileName = ServerPath;// fileName;
                newDSC.ID = ID.ToString();
                newDSC.UpdateIn = UpdateIn;
                newDSC.UserId = UserId;
                newDSC.MonthYear = Month_Year;
                MessageEF msgEF = _newDSCModel.SaveDSCFilePath(newDSC);
            }
            catch (Exception ex)
            {
                OutputResult = "FAILED";
            }
            return Json(OutputResult.ToUpper());
        }


    }
}
