using IntegrationApi.Data.AzureHelperServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/{controller}/{action}")]
	[ApiController]
	public class AzureFileController : ControllerBase
	{
		private readonly IAzureHelperProvider iAzureFileOperation;
		public AzureFileController(IAzureHelperProvider _iAzureFileOperation)
		{
			iAzureFileOperation = _iAzureFileOperation;
		}

		[HttpPost]
		public async Task<MessageEF> SaveFile(MyFileRequest request)
		{
			
			return await iAzureFileOperation.SaveFile(request);
		}

		[HttpPost]
		public async Task<MessageEF> DeleteFile(MyFileRequest fileNameWithPath)
		{
			
			return await iAzureFileOperation.DeleteFile(fileNameWithPath);
		}

		[HttpPost]
		public async Task<MessageEF> CheckFileExistance(MyFileRequest fileNameWithPath)
		{
		
			return await iAzureFileOperation.CheckFileExistance(fileNameWithPath);
		}
		[HttpPost]
		public async Task<MyFileResult> DownloadFileResult(MyFileRequest fileNameWithPath)
		{
			

			MyFileResult fr = new MyFileResult();
			fr = await iAzureFileOperation.DownloadFile(fileNameWithPath);
			fr.FileBase64 = Convert.ToBase64String(ReadFully(fr.Filestream));
			fr.Filestream = null;
			return fr;

		}

		[HttpPost]
		public async Task<FileResult> DownloadFile(MyFileRequest fileNameWithPath)
		{
			

			MyFileResult fr = new MyFileResult();
			fr = await iAzureFileOperation.DownloadFile(fileNameWithPath);
			return File(fr.Filestream, fr.ContentType, fr.FileName);

		}

		[HttpPost]
		public async Task<MyFileResult> DownloadFileBase64(MyFileRequest fileNameWithPath)
		{
			MyFileResult fr = new MyFileResult();
			fr = await iAzureFileOperation.DownloadFile(fileNameWithPath);
			//Byte[] bytes = fr.Filestream.reada;
			String file = Convert.ToBase64String(ReadFully(fr.Filestream));
			fr.FileBase64 = file;
			fr.Filestream = null;
			//return File(fr.Filestream, fr.ContentType, fr.FileName);
			return fr;
		}

		public static byte[] ReadToEnd(System.IO.Stream stream)
		{
			long originalPosition = 0;

			if (stream.CanSeek)
			{
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try
			{
				byte[] readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
				{
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length)
					{
						int nextByte = stream.ReadByte();
						if (nextByte != -1)
						{
							byte[] temp = new byte[readBuffer.Length * 2];
							Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
							Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				byte[] buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead)
				{
					buffer = new byte[totalBytesRead];
					Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			}
			finally
			{
				if (stream.CanSeek)
				{
					stream.Position = originalPosition;
				}
			}
		}
		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		[HttpPost]
		public MessageEF SaveFiles(MyFileRequest request)
		{
			return iAzureFileOperation.SaveFiles(request);
		}


	}
}
