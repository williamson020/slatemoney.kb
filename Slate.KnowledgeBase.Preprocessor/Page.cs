﻿using System.Text;
using System.Xml.Serialization;
using MariGold.HtmlParser;

namespace Slate.KnowledgeBase.Preprocessor;
public class Page
{

    #region XML deserialization Attribs

    [XmlAttribute("src")]
    public string SourceFileName
    {
        get; set;
    }

    [XmlAttribute("target")]
    public string TargetFileName
    {
        get; set;
    }

    [XmlAttribute("title")]
    public string PageTitle
    {
        get; set;
    }
    
    [XmlAttribute("openinbrowser")]
    public string OpenInBrowser
    {
        get; set;
    }

    [XmlElement(ElementName = "Topic")]
    public List<Topic> Topics
    {
        get; set;
    }

    #endregion

    public override string ToString() => $"{SourceFileName} has target {TargetFileName}";



    /// <summary>
    /// The html extracted from SourceFileName to be inserted into TargetFileName via target template
    /// </summary>
    [XmlIgnore]
    public string HtmlMainContent
    {
        get;
        private set;
    }

    private string GenerateTopicsBar()
    {

        if (!Topics.Any())
        {
            return string.Empty;
        }


        var sb = new StringBuilder();
        foreach (var topic in Topics)
        {

            var anchor = XmlUtil.Anchor(topic.ClassNames, topic.Href, topic.Legend);

            sb.AppendLine(XmlUtil.ListIndented(anchor));

        }

        var uList = XmlUtil.UList("topic-list", sb.ToString());
        var nav = XmlUtil.Nav(uList);
        var cardContent = XmlUtil.DivClass("gx-card-content gx-topic-list", nav);
        var cardHeader = XmlUtil.DivClass("gx-card-header", XmlUtil.H4("gx-card-title", "Topics"));

        var sidebardetail = $"{cardHeader}{cardContent}";

        var sidebar = XmlUtil.DivClass("gx-card topic-sidebar", sidebardetail);


        var div1 = XmlUtil.DivClass("gx-side-overlay", null);
        var div2 = XmlUtil.DivClass("col-xl-3 col-md-12 gx-topic", sidebar);


        sb.Clear();

        sb.AppendLine(div1);
        sb.AppendLine(div2);

        return sb.ToString();

    }


    /// <summary>
    /// Extract the  content of div class==gx-main-content and assign to Html prop
    /// </summary>
    /// <param name="helpPagesDir"></param>
    /// <exception cref="FileNotFoundException"></exception>

    internal void ExtractContent(string helpPagesDir)
    {
        var srcFile = Path.Combine(helpPagesDir, SourceFileName);

        if (!File.Exists(srcFile))
        {
            throw new FileNotFoundException($"Source page not found: {srcFile}");
        }

        HtmlParser parser = new HtmlTextParser(File.ReadAllText(srcFile));

        if (parser.Parse())
        {
            var body = parser.Current?.Children.FirstOrDefault(n => n.Tag == "body");
            var main = body?.Children.FirstOrDefault(n => n.Tag == "main");
            var div = main?.Children.FirstOrDefault(n => n.Tag == "div");

            if (div != null)
            {
                //Expecting the first with  named class <div class="gx-main-content">

                var classAttribute = div.Attributes["class"];

                if (classAttribute == null)
                {
                    return;
                }

                if (classAttribute.ToString() == "gx-main-content")
                {
                    HtmlMainContent = div.Html;
                }
            }
        }
    }


    private  string TitleToken => AsComment("$$TITLE$$") ;
    private  string SideBarToken =>AsComment("$$SIDEBAR$$") ;
    private string MainContentToken => AsComment("$$MAIN-CONTENT$$");
    private string SideBarTopicsToken => AsComment("$$SIDEBAR-TOPICS$$");


    [XmlIgnore] public bool IsLaunchPage => OpenInBrowser == "true";


    private static string AsComment(string token) => $"<!-- {token} -->";

    public bool GenerateTarget(string pageTemplateFile, string commonSideBar, string outputDir)
    {
        if (!File.Exists(pageTemplateFile))
        {
            throw new FileNotFoundException($"Page template file not found {pageTemplateFile}");
        }


        if (!Directory.Exists(outputDir))
        {
            throw new DirectoryNotFoundException($"Output folder not found {outputDir}");
        }


        //1. Load page template 

        var data = File.ReadAllText(pageTemplateFile);

        //2. Insert title
        data = Subst(data,  TitleToken, XmlUtil.Title(PageTitle));


        //2. Insert navigation side bar
        data = Subst(data, SideBarToken, commonSideBar);

        //3. Insert main content & topics bar

        data = Subst(data, MainContentToken, Subst(HtmlMainContent, SideBarTopicsToken, GenerateTopicsBar()) );


        //5. Save as target name

        var target = Path.Combine(outputDir, TargetFileName);

        FileUtil.Delete(target);

        File.WriteAllText(target, data);

        return true;
    }

    private string Subst(string data, string token, string content)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentNullException(nameof(data));
        }


        return data.Replace(token, content);


    }
}

