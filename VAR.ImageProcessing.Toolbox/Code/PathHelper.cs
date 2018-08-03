using System.IO;

namespace VAR.ImageProcessing.Toolbox.Code
{
    public class PathHelper
    {
        public static string AddSuffixToFilePath(string filePath, string suffix)
        {
            string strNewFileName = string.Format("{0}/{1}{3}{2}",
                Path.GetDirectoryName(filePath),
                Path.GetFileNameWithoutExtension(filePath),
                Path.GetExtension(filePath),
                suffix);
            return strNewFileName;
        }
    }
}
