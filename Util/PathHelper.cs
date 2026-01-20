using System.Reflection;

namespace SqlCodeEditor.Util
{
    public class PathHelper
    {
        public const string SyntaxHighlightingFolderPath = "Config/SyntaxHighlighting";

        public static string GetAssemblyFolder()
        {
            string dllFolder = Assembly.GetExecutingAssembly().CodeBase;

            return Path.GetDirectoryName(dllFolder.Substring(8, dllFolder.Length - 8));
        }

        public static string GetSyntaxHighlightingConfigFolder()
        {
            return Path.Combine(GetAssemblyFolder(), SyntaxHighlightingFolderPath);
        }
    }
}
