using System.Xml;
using MariGold.HtmlParser;

namespace Slate.KnowledgeBase.Preprocessor;

internal class Program
{

    private static void Usage()
    {
        Console.WriteLine("Slate.Knowledgebase.Preprocessor {output folder}");
    }


    public static void Main(string[] args)
    {

        try
        {

            if (args.Length == 0)
            {
                Usage();
                return;
            }

            var outputDir = args[0];

            if (!Directory.Exists(outputDir))
            {
                throw new DirectoryNotFoundException($"Output folder not found {outputDir}");
            }

            var assetFolder = Path.Combine( FileUtil.InstallPath(), "Assets");

            //var assetFolder =
            //    @"C:\Users\make-\source\repos\williamson020\slatemoney.io\Slate.KnowledgeBase.Preprocessor\Assets";


            var manifest = XmlUtil.DeserializeToObject<Manifest>(Path.Combine(assetFolder, "manifest.xml"));

            if (manifest == null)
            {
                throw new Exception("Failed to load manifest");
            }

            manifest.Process(assetFolder, outputDir);


            if (manifest.LaunchPage != null)
            {
                ProcessHelper.OpenUrl(Path.Combine(outputDir,manifest.LaunchPage.TargetFileName ));
            }


            //Output  all anchors to console
            manifest.DumpAnchors();

            //Output insert statements for database (server side) 
            manifest.DumpExRefs();

        }
        catch (Exception e)
        {
            Console.WriteLine("Application terminated with exception");
            Console.WriteLine($"--{e.Message}");
        }
    }


   

   
}