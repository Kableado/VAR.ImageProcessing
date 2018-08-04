using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VAR.ImageProcessing.Toolbox.Code
{
    public class FileHelper
    {
        public static List<string> GetImageFilesOnDirectory(string directoryPath)
        {
            List<string> imageFiles = new List<string>();
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                string fileExtension = Path.GetExtension(file.ToLower());
                if (
                    fileExtension != ".jpg" &&
                    fileExtension != ".jpeg" &&
                    fileExtension != ".png" &&
                    fileExtension != ".bmp" &&
                    fileExtension != ".gif" &&
                    true
                    )
                { continue; }
                imageFiles.Add(file);
            }
            imageFiles = imageFiles.OrderBy(x => x).ToList();
            return imageFiles;
        }
    }
}
