using System.Collections.Generic;
using System.IO;

namespace VAR.ImageProcessing.Toolbox.Code
{
    public class PathHelper
    {
        public static string AddSuffixToFilePath(string filePath, string suffix)
        {
            string strNewFileName = string.Format("{0}{1}{2}{3}{4}",
                Path.GetDirectoryName(filePath),
                Path.DirectorySeparatorChar,
                Path.GetFileNameWithoutExtension(filePath),
                suffix,
                Path.GetExtension(filePath));
            return strNewFileName;
        }

        public static string AddDirectoryToFilePath(string filePath, string directory, bool checkAndCreate = false)
        {
            string path = string.Format("{0}{1}{2}{3}",
                Path.GetDirectoryName(filePath),
                Path.DirectorySeparatorChar,
                directory,
                Path.DirectorySeparatorChar);
            if (checkAndCreate)
            {
                CreatePath(path);
            }
            string strNewFileName = string.Format("{0}{1}",
                path,
                Path.GetFileName(filePath));
            return strNewFileName;
        }
        
        public static void CreatePath(string path)
        {
            var dirInfo = new DirectoryInfo(Path.GetDirectoryName(path));
            DirectoryInfo rootDirInfo = dirInfo;
            var listPathDirs = new List<DirectoryInfo>();
            while (rootDirInfo.Parent != null)
            {
                listPathDirs.Insert(0, rootDirInfo);
                rootDirInfo = rootDirInfo.Parent;
            }
            foreach (DirectoryInfo auxDirInfo in listPathDirs)
            {
                if (!auxDirInfo.Exists)
                {
                    auxDirInfo.Create();
                }
            }
        }

    }
}
