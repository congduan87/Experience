using System.IO;

namespace MyWedding.Common
{
    public class Helper
    {
        public static string RootImage()
        {
            var file = Path.Combine(VariableGlobal.ContentRoot, "wwwroot", "uploads", "images");
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }
            return file;
        }
        public static string GetPathImage(string fileName = "")
        {
            return "/" + Path.Combine("uploads", "images", fileName??"").Replace("\\", "/");
        }
    }
}
