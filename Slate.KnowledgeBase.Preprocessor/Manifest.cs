using System.Diagnostics;
using System.Xml.Serialization;

namespace Slate.KnowledgeBase.Preprocessor;

[XmlRoot(ElementName = "Manifest")]
public class Manifest
{
    public Manifest()
    {
        Pages = new List<Page>();
    }

    [XmlElement(ElementName = "Page")]
    public List<Page> Pages
    {
        get; set;
    }


    [XmlElement(ElementName = "Sidebar")]
    public Sidebar SidebarDefinition
    {
        get;
        set;
    }

    public Page this[string name] => Pages.FirstOrDefault(s => string.Equals(s.SourceFileName, name, StringComparison.OrdinalIgnoreCase));

    [XmlIgnore]
    public string AssetPath
    {
        get;
        private set;
    }

    public string SourcePagesDir => Path.Combine(AssetPath, "Pages");
    public string TemplatesDir => Path.Combine(AssetPath, "Templates");

    private string PageTemplateFile => Path.Combine(TemplatesDir, "page-template.html");

    public Page LaunchPage => Pages.FirstOrDefault(p => p.IsLaunchPage);
    
        

    public string GenerateSideBarHtml()
    {
        Debug.Assert(SidebarDefinition != null);

        return SidebarDefinition.Html();
    }


    public void Process(string assetPath, string outputDirectory)
    {

        if (!Directory.Exists(assetPath))
        {
            throw new DirectoryNotFoundException($"Asset path {assetPath} not found");
        }

        AssetPath = assetPath;


        //Auto generated sidebar (html) is inserted in every page
        var sideBar = GenerateSideBarHtml();

        foreach (var helpPage in Pages)
        {
            Console.WriteLine($"Processing {helpPage.SourceFileName}");
            //Extracts the page main content into Html prop
            helpPage.ExtractContent(SourcePagesDir);


            if (helpPage.HtmlMainContent == null)
            {
                throw new Exception($"Failed to extract main content from {helpPage.SourceFileName}");
            }

            //Generate a final target page with name

            var res = helpPage.GenerateTarget(PageTemplateFile, sideBar, outputDirectory);

            Console.WriteLine($"-- generated {helpPage.TargetFileName} {res}");

        }


        /*
         parse all <Page elements> : generate targets including SideBar topic section.

           Note the id of anchors in <div class="gx-card gx-page-block" id="gx_intro">
           create target file from TARGET-TEMPLATE.html

           *
           subst ELEMENT  <div class="gx-main-content"></DIV> WITH extract element <div class="gx-main-content"> [do not edit - verbatim]

           * generate and insert common sidebar (dynamic html)

           SUBST ELEMET <div class="gx-sidebar" data-mode="dark"></DIV>
         */
    }

    public void DumpAnchors() =>  Pages.ForEach(p=> p.DumpTopicAnchors());
    
}




