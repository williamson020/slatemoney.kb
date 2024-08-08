using System.Text;
using System.Xml.Serialization;

namespace Slate.KnowledgeBase.Preprocessor;
public class Sidebar
{

    #region XML Attribs

    
    [XmlElement(ElementName = "Entry")]  
    public List<SidebarEntry> Entries { get; set; }
    #endregion

   
    public string Html() => $"{XmlUtil.DivClass("gx-sidebar-overlay",null)}{DivGxSidebar()}";

    private string DivGxSidebar()
    {
        var a1 = XmlUtil.Anchor("sb-full", href: "index.html", content: XmlUtil.Image("assets/img/logo/slate-full-logo.png","logo"));
        var a2 = XmlUtil.Anchor("sb-collapse", href: "index.html", content:XmlUtil.Image("assets/img/logo/slate-collapse-logo.png","logo"));


        var divLogo = XmlUtil.DivClass("gx-sb-logo", $"{a1}{a2}");

        var divWrapper = XmlUtil.DivClass("gx-sb-wrapper", DivSbContent());

        return XmlUtil.DivClass("gx-sidebar", $"{divLogo}{divWrapper}", datamode:"dark");
    }

    private string DivSbContent() =>  XmlUtil.DivClass("gx-sb-content", DivSbList());

    private string DivSbList() => XmlUtil.DivClass("gx-sb-list", ListItems());

    private string ListItems()
    {
        var sb = new StringBuilder();

        foreach (var sidebarEntry in Entries)
        {
            sb.AppendLine(sidebarEntry.GenerateHtml());
        }

        return sb.ToString();
    }
}









