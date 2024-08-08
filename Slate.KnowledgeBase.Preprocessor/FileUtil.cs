using System.Reflection;

namespace Slate.KnowledgeBase.Preprocessor;
public static class FileUtil
{

    public static void Delete(string filename)
    {

        try
        {
            File.Delete(filename);
        }
        catch 
        {
            
        }
    }

    public static string InstallPath()
    {
        var fileName = Assembly.GetEntryAssembly()?.Location;
        if (fileName != null)
        {
            var fi = new FileInfo(fileName);
            return fi.DirectoryName;
        }
        return null;
    }
}
