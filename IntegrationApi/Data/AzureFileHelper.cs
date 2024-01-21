using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Files.Shares;
using Azure;
using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data
{
	public class AzureFileHelper
	{
		private readonly IConfiguration _configuration;
		public AzureFileHelper(IConfiguration configuration)
		{
			this._configuration = configuration;
		}


		public MessageEF SaveFileToAzure(Stream inputStream, string fileName, string folderName)
		{
			MessageEF msg = new MessageEF();
			string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
			string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
			string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

			folderName = folderName.Replace(@"\", "/").Trim();

			folderName = RootDir + folderName;
			ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);

			if (share.Exists())
			{
				ShareDirectoryClient directory = share.GetDirectoryClient(folderName);
				if (_configuration["AzurSettings:AllowNewDirectory"] == "Yes")
				{
					directory.CreateIfNotExists();
				}


				// Get a reference to a file and upload it
				ShareFileClient file = directory.GetFileClient(fileName);

				// Ensure that the share exists.  
				if (directory.Exists())
				{
					ShareFileClient fileExists = directory.GetFileClient(fileName);
					if (fileExists.Exists())
					{
						msg.Msg = "File already exists with same name";
					}
					else
					{
						file.Create(inputStream.Length);
						file.UploadRange(new HttpRange(0, inputStream.Length), inputStream);
						msg.Msg = "Successfully uploaded";
					}


				}
				else
				{
					msg.Msg = "DIractory Not Found";
				}
			}
			else
			{
				msg.Msg = "Not able to connect azure server";
			}
			return msg;
		}

		public async Task<MessageEF> SaveFileToAzureBase64(MyFileRequest myFileRequest)
		{
			MessageEF msg = new MessageEF();
			try
			{
				byte[] data = Convert.FromBase64String(myFileRequest.FileContenctBase64);
				Stream stream = new MemoryStream(data);

				string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
				string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
				string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

				myFileRequest.FolderName = myFileRequest.FolderName.Replace(@"\", "/").Trim();

				myFileRequest.FolderName = RootDir + myFileRequest.FolderName;
				ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);

				if (share.Exists())
				{
					ShareDirectoryClient directory = share.GetDirectoryClient(myFileRequest.FolderName);
					if (_configuration["AzurSettings:AllowNewDirectory"] == "Yes")
					{
						directory.CreateIfNotExists();
					}
					// Get a reference to a file and upload it
					ShareFileClient file = directory.GetFileClient(myFileRequest.FileName);

					// Ensure that the share exists.  
					if (directory.Exists())
					{
						ShareFileClient fileExists = directory.GetFileClient(myFileRequest.FileName);
						if (fileExists.Exists())
						{
							msg.Msg = "File already exists with same name";
							msg.Satus = "False";
						}
						else
						{
							file.Create(stream.Length);
							file.UploadRange(new HttpRange(0, stream.Length), stream);
							msg.Msg = "Successfully uploaded";
							msg.Satus = "True";
						}
					}
					else
					{
						msg.Msg = "DIractory Not Found";
						msg.Satus = "False";
					}
				}
				else
				{
					msg.Msg = "Not able to connect azure server";
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

		public MessageEF SaveFileToAzureBase64s(MyFileRequest myFileRequest)
		{
			MessageEF msg = new MessageEF();
			try
			{
				byte[] data = Convert.FromBase64String(myFileRequest.FileContenctBase64);
				Stream stream = new MemoryStream(data);

				string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
				string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
				string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

				myFileRequest.FolderName = myFileRequest.FolderName.Replace(@"\", "/").Trim();

				myFileRequest.FolderName = RootDir + myFileRequest.FolderName;
				ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);

				if (share.Exists())
				{
					ShareDirectoryClient directory = share.GetDirectoryClient(myFileRequest.FolderName);
					if (_configuration["AzurSettings:AllowNewDirectory"] == "Yes")
					{
						directory.CreateIfNotExists();
					}
					// Get a reference to a file and upload it
					ShareFileClient file = directory.GetFileClient(myFileRequest.FileName);

					// Ensure that the share exists.  
					if (directory.Exists())
					{
						ShareFileClient fileExists = directory.GetFileClient(myFileRequest.FileName);
						if (fileExists.Exists())
						{
							msg.Msg = "File already exists with same name";
							msg.Satus = "False";
						}
						else
						{
							file.Create(stream.Length);
							file.UploadRange(new HttpRange(0, stream.Length), stream);
							msg.Msg = "Successfully uploaded";
							msg.Satus = "True";
						}
					}
					else
					{
						msg.Msg = "DIractory Not Found";
						msg.Satus = "False";
					}
				}
				else
				{
					msg.Msg = "Not able to connect azure server";
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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		/// 

		public async Task<MyFileResult> DownloadFileFromAzure(string path)
		{
			MyFileResult fr = new MyFileResult();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
				string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
				string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

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
					folderName = RootDir + folderName;
					ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);
					//CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzurStorageConnectionString"));

					if (share.Exists())
					{
						ShareDirectoryClient directory = share.GetDirectoryClient(folderName);
						// Ensure that the share exists.  
						if (directory.Exists())
						{
							ShareFileClient file = directory.GetFileClient(fileName);

							if (file.Exists())
							{
								ShareFileDownloadInfo download = await file.DownloadAsync();
								//Stream stream = new MemoryStream();
								//download.Content.CopyTo(stream);
								fr.Filestream = download.Content;//file.OpenReadAsync().Result;
								fr.ContentType = download.ContentType;// file.GetType().ToString();
								fr.FileName = file.Name;
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
				byte[] fileBytes = Convert.FromBase64String("RmlsZSBOb3QgZm91bmQ=");
				Stream textstream = new MemoryStream(fileBytes);
				fr.Filestream = textstream;
				fr.ContentType = "text/plain";
				fr.FileName = "File_Not_Found.txt";
				fr.Status = "False";
				return fr;
			}

		}

		public async Task<MessageEF> DeleteFileFromAzure(string path)
		{
			MessageEF messageEF = new MessageEF();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
				string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
				string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

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
					folderName = RootDir + folderName;
					ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);
					//CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzurStorageConnectionString"));

					if (share.Exists())
					{
						ShareDirectoryClient directory = share.GetDirectoryClient(folderName);


						// Ensure that the share exists.  
						if (directory.Exists())
						{

							ShareFileClient file = directory.GetFileClient(fileName);

							if (file.Exists())
							{
								file.Delete();
								messageEF.Msg = "File Deleted";
								messageEF.Satus = "True";
							}
							else
							{
								messageEF.Msg = "File Not Found";
								messageEF.Satus = "False";
							}
						}
						else
						{
							messageEF.Msg = "Directory Not Found";
							messageEF.Satus = "False";
						}

					}
					else
					{
						messageEF.Msg = "Storage Not Found";
						messageEF.Satus = "False";
					}
				}
				else
				{
					messageEF.Msg = "Folder/File Name is empty";
					messageEF.Satus = "False";
				}
			}
			catch (Exception ex)
			{
				messageEF.Msg = ex.Message;
				messageEF.Satus = "False";
			}
			return messageEF;
		}


		public async Task<MessageEF> CheckFileAlreadyExistsFromAzure(string path)
		{
			MessageEF messageEF = new MessageEF();
			try
			{
				string RootDir = Convert.ToString(_configuration["AzurSettings:AzurRootDir"]);// "AzurRootDir";
				string FileShare = Convert.ToString(_configuration["AzurSettings:AzurFileShare"]);// "AzurRootDir";
				string AzurStorageConnectionString = Convert.ToString(_configuration["AzurSettings:AzurStorageConnectionString"]);// "AzurRootDir";

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
					folderName = RootDir + folderName;
					ShareClient share = new ShareClient(AzurStorageConnectionString, FileShare);
					//CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzurStorageConnectionString"));

					if (share.Exists())
					{
						ShareDirectoryClient directory = share.GetDirectoryClient(folderName);

						// Ensure that the share exists.  
						if (directory.Exists())
						{
							ShareFileClient file = directory.GetFileClient(fileName);
							if (file.Exists())
							{
								messageEF.Msg = "File Exist";
								messageEF.Satus = "True";
							}
							else
							{
								messageEF.Msg = "File Not Found";
								messageEF.Satus = "False";
							}
						}
						else
						{
							messageEF.Msg = "Directory Not Found";
							messageEF.Satus = "False";
						}
					}
					else
					{
						messageEF.Msg = "Storage Not Found";
						messageEF.Satus = "False";
					}

				}
				else
				{
					messageEF.Msg = "Folder/File Name is empty";
					messageEF.Satus = "False";
				}
			}
			catch (Exception ex)
			{
				messageEF.Msg = ex.Message;
				messageEF.Satus = "False";
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
