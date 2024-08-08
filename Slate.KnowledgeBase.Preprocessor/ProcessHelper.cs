namespace Slate.KnowledgeBase.Preprocessor;
internal static class ProcessHelper
{
    public static void OpenUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch
        {
            //TODO - PROPAGATE ERROR MESSAGE
            //MessageBox.Show($"Failed to open {url} {ex.Message}");
        }
    }


    public static void OpenTextFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return;
        }

        var psi = new System.Diagnostics.ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = fileName
        };
        System.Diagnostics.Process.Start(psi);

    }
}
