using Fiorella.Areas.Admin.Controllers.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Fiorella.Areas.Admin.Utils
{
    public static class FileUtils
    {
        public static string FileCreate(IFormFile file)
        {
            var path = Path.Combine(FileConstants.ImagePath, Guid.NewGuid() + file.FileName);
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return file.FileName;
        }

        public static void FileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
