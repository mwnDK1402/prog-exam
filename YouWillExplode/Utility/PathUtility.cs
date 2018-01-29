namespace Utility
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class PathUtility
    {
        public static string[] GetFilesRelative(string directory, string filter)
        {
            StringBuilder folder = new StringBuilder(GetProcessDirectory());
            folder.Append(directory);
            folder.Append("\\");
            try
            {
                return Directory.GetFiles(folder.ToString(), filter);
            }
            catch (DirectoryNotFoundException)
            {
                return new string[0];
            }
        }

        public static string GetProcessDirectory()
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\";
        }
    }
}