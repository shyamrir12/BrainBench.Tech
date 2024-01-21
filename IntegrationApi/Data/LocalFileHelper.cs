using IntegrationModels;
using LoginModels;
using System.Text;

namespace IntegrationApi.Data
{
	public class LocalFileHelper
	{
		private readonly IConfiguration _configuration;
		public LocalFileHelper(IConfiguration configuration)
		{
			this._configuration = configuration;
		}
		public async Task<MessageEF> SaveFileToLocaBase64(MyFileRequest myFileRequest)
		{
			MessageEF msg = new MessageEF();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:LocalRootDir"]);// "AzurRootDir";
				byte[] data = Convert.FromBase64String(myFileRequest.FileContenctBase64);
				Stream stream = new MemoryStream(data);
				string replacedPath = myFileRequest.FolderName.Replace(@"/", @"\").Trim();
				myFileRequest.FolderName = replacedPath;
				myFileRequest.FolderName = RootDir + "\\" + replacedPath;
				if (_configuration["AzurSettings:AllowNewDirectory"] == "Yes")
				{
					if (!Directory.Exists(myFileRequest.FolderName))
					{
						Directory.CreateDirectory(myFileRequest.FolderName);
					}
				}
				if (Directory.Exists(myFileRequest.FolderName))
				{
					if (System.IO.File.Exists(myFileRequest.FolderName + "\\" + myFileRequest.FileName))
					{
						msg.Msg = "File already exists with same name";
						msg.Satus = "False";
					}
					else
					{
						File.WriteAllBytes(myFileRequest.FolderName + "\\" + myFileRequest.FileName, data);
						msg.Msg = "Success";
						msg.Satus = "True";
					}
				}
				else
				{
					msg.Msg = "DIractory Not Found";
					msg.Satus = "False";
				}
			}
			catch (Exception ex)
			{
				msg.Msg = ex.Message;
				msg.Satus = "False";
			}
			return msg;
		}

		public MessageEF SaveFileToLocaBase64s(MyFileRequest myFileRequest)
		{
			MessageEF msg = new MessageEF();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:LocalRootDir"]);// "AzurRootDir";
				byte[] data = Convert.FromBase64String(myFileRequest.FileContenctBase64);
				Stream stream = new MemoryStream(data);
				string replacedPath = myFileRequest.FolderName.Replace(@"/", @"\").Trim();
				myFileRequest.FolderName = replacedPath;
				myFileRequest.FolderName = RootDir + "\\" + replacedPath;
				if (_configuration["AzurSettings:AllowNewDirectory"] == "Yes")
				{
					if (!Directory.Exists(myFileRequest.FolderName))
					{
						Directory.CreateDirectory(myFileRequest.FolderName);
					}
				}
				if (Directory.Exists(myFileRequest.FolderName))
				{
					if (System.IO.File.Exists(myFileRequest.FolderName + "\\" + myFileRequest.FileName))
					{
						msg.Msg = "File already exists with same name";
						msg.Satus = "False";
					}
					else
					{
						File.WriteAllBytes(myFileRequest.FolderName + "\\" + myFileRequest.FileName, data);
						msg.Msg = "Success";
						msg.Satus = "True";
					}
				}
				else
				{
					msg.Msg = "DIractory Not Found";
					msg.Satus = "False";
				}
			}
			catch (Exception ex)
			{
				msg.Msg = ex.Message;
				msg.Satus = "False";
			}
			return msg;
		}


		public async Task<MyFileResult> DownloadFileFromLocal(string path)
		{
			MyFileResult fr = new MyFileResult();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:LocalRootDir"]);// "AzurRootDir";
				string folderName = "";
				string fileName = "";
				try
				{
					folderName = getFolderOrFileName(path, "folder");
					fileName = getFolderOrFileName(path, "file");
				}
				catch (Exception ex)
				{

				}
				if (folderName != "" && fileName != "")
				{
					folderName = RootDir + "\\" + folderName;
					string replacedFolderName = folderName.Replace(@"/", @"\").Trim();
					if (Directory.Exists(replacedFolderName))
					{
						//ShareFileDownloadInfo download = await file.DownloadAsync();
						////Stream stream = new MemoryStream();
						////download.Content.CopyTo(stream);

						Stream fs = File.OpenRead(replacedFolderName + "\\" + fileName);
						//System.IO.MemoryStream data = new System.IO.MemoryStream();
						//System.IO.Stream str = fs;

						//str.CopyTo(data);
						//data.Seek(0, SeekOrigin.Begin); // <-- missing line
						//byte[] buf = new byte[data.Length];
						//data.Read(buf, 0, buf.Length);
						//fr.Filestream = data;

						fr.Filestream = fs;
						fr.ContentType = Mime.GetMimeType(folderName + "\\" + fileName);
						fr.FileName = fileName;
						fr.Status = "True";
						return fr;
					}
					else
					{
						//return error file
						byte[] fileBytes = Convert.FromBase64String("RmlsZSBOb3QgZm91bmQ=");
						Stream textstream = new MemoryStream(fileBytes);
						fr.Filestream = textstream;
						fr.ContentType = "text/plain";
						fr.FileName = "File_Not_Found.txt";
						fr.Status = "False";
						return fr;
					}
				}
				else
				{
					//return error file
					byte[] fileBytes = Convert.FromBase64String("RmlsZSBOb3QgZm91bmQ=");
					Stream textstream = new MemoryStream(fileBytes);
					fr.Filestream = textstream;
					fr.ContentType = "text/plain";
					fr.FileName = "File_Not_Found.txt";
					fr.Status = "False";
					return fr;
				}
			}
			catch (Exception ex)
			{
				//return error file
				byte[] fileBytes = Encoding.ASCII.GetBytes(ex.Message);
				Stream textstream = new MemoryStream(fileBytes);
				fr.Filestream = textstream;
				fr.ContentType = "text/plain";
				fr.FileName = "File_Not_Found.txt";
				fr.Status = "False";
				return fr;
			}
			return fr;
		}
		public async Task<MessageEF> DeleteFileFromLocal(string path)
		{
			MessageEF messageEF = new MessageEF();
			MyFileResult fr = new MyFileResult();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:LocalRootDir"]);
				string folderName = "";
				string fileName = "";
				try
				{
					folderName = getFolderOrFileName(path, "folder");
					fileName = getFolderOrFileName(path, "file");
				}
				catch (Exception ex)
				{

				}
				if (folderName != "" && fileName != "")
				{
					folderName = RootDir + "\\" + folderName;
					string replacedFolderName = folderName.Replace(@"/", @"\").Trim();
					if (Directory.Exists(replacedFolderName))
					{
						if (File.Exists(replacedFolderName + "\\" + fileName))
						{
							File.Delete(replacedFolderName + "\\" + fileName);
							messageEF.Satus = "True";
							messageEF.Msg = "Success";
						}
						else
						{
							messageEF.Satus = "False";
							messageEF.Msg = "File not exist!";
						}
					}
					else
					{
						messageEF.Satus = "False";
						messageEF.Msg = "Folder not exist!";
					}
				}
				else
				{
					messageEF.Satus = "False";
					messageEF.Msg = "File/Folder not exist!";
				}
			}
			catch (Exception ex)
			{
				messageEF.Satus = "False";
				messageEF.Msg = ex.Message;
			}
			return messageEF;
		}
		public async Task<MessageEF> CheckFileAlreadyExistsFromAzure(string path)
		{
			MessageEF messageEF = new MessageEF();
			MyFileResult fr = new MyFileResult();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:LocalRootDir"]);
				string folderName = "";
				string fileName = "";
				try
				{
					folderName = getFolderOrFileName(path, "folder");
					fileName = getFolderOrFileName(path, "file");
				}
				catch (Exception ex)
				{

				}
				if (folderName != "" && fileName != "")
				{
					folderName = RootDir + "\\" + folderName;
					string replacedFolderName = folderName.Replace(@"/", @"\").Trim();
					if (Directory.Exists(replacedFolderName))
					{
						if (File.Exists(replacedFolderName + "\\" + fileName))
						{
							messageEF.Satus = "True";
							messageEF.Msg = "Success";
						}
						else
						{
							messageEF.Satus = "False";
							messageEF.Msg = "File not exist!";
						}
					}
					else
					{
						messageEF.Satus = "False";
						messageEF.Msg = "Folder not exist!";
					}
				}
				else
				{
					messageEF.Satus = "False";
					messageEF.Msg = "File/Folder not exist!";
				}
			}
			catch (Exception ex)
			{
				messageEF.Satus = "False";
				messageEF.Msg = ex.Message;
			}
			return messageEF;
		}


		public string getFolderOrFileName(string path, string type)
		{
			string name = "";
			var splittedArray = path.Replace(@"\", "//").Trim();
			string[] afterSplit = splittedArray.Split('/');
			string[] subArray = new string[] { };
			var list = new List<string>(afterSplit);
			afterSplit = list.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList().ToArray();

			bool check = false;
			if (type == "folder")
			{
				for (int i = 0; i < afterSplit.Length; i++)
				{
					if (afterSplit[i] == "DSC" || afterSplit[i] == "Upload" || afterSplit[i] == "App_Data" || afterSplit[i] == "UploadData" || afterSplit[i] == "UploadQuarry" || afterSplit[i] == "Uploadmining")
					{
						check = true;
						if (afterSplit[i] == "Upload")
						{
							subArray = afterSplit.Skip(i + 1).Take(afterSplit.Length).ToArray();
						}
						else
						{
							subArray = afterSplit.Skip(i).Take(afterSplit.Length).ToArray();

						}

						break;
					}
				}

				if (check == false)
				{
					subArray = afterSplit.Take(afterSplit.Length).ToArray();
				}


				for (int i = 0; i < subArray.Length; i++)
				{
					if (i != subArray.Length - 1)
					{
						name = name + subArray[i] + "/";
					}
					else

					{
						break;

					}
				}
			}
			else
			{

				name = afterSplit[afterSplit.Length - 1];
			}
			return name;
		}
	}
}
