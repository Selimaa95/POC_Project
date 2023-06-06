using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace POC_Project.BL.Helper
{
    public static class FileUploader
    {
        public static string UploadFile(string folderName, IFormFile file)
        {
			try
			{
				string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/" + folderName;
				
				string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);

				string FinalPath = Path.Combine(FolderPath, FileName);

				using(var stream = new FileStream(FinalPath, FileMode.Create))
				{
					file.CopyTo(stream);
				}

				return FileName;

			}
			catch (Exception ex)
			{
				return ex.Message;
			}

        }

		public static string RemoveFile(string folderName, string file)
		{
			try
			{
				var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName, file);
				if (File.Exists(directory))
				{
					File.Delete(directory);
				    return "File Deleted";
				}
                return "File Not Deleted";

            }
            catch (Exception ex)
			{

				return ex.Message;
			}
		}
    }
}
