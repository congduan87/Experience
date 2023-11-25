using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.GiamKichSan.Common
{
    public class UploadHelper
    {
        public static string RootImage()
        {
            var file = Path.Combine(SessionGlobal.ContentRoot, "wwwroot", "uploads");
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }
            return file;
        }
        public static string GetPathImage(string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;
            else
                return "/" + Path.Combine("uploads", fileName ?? "").Replace("\\", "/");
        }
    }
}
