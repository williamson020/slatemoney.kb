using System.Text;
using System.Xml.Serialization;

namespace Slate.KnowledgeBase.Preprocessor;
public class SidebarEntry
{

    #region Xml Attribs

   
    /// <summary>
    /// 'dropitem' or 'separator' expected as class attribute
    /// </summary>
   
    [XmlAttribute("class")]  
    public string TheClass { get; set; }  
     
    [XmlAttribute("legend")]  
    public string Legend { get; set; }

    [XmlElement(ElementName = "Navlink")]  
    public List<NavLink> NavLinks { get; set; }


    /// <summary>
    /// https://remixicon.com/
    /// /see options in remixicon.css
    /// </summary>
    [XmlAttribute("icon")]
    public string Icon
    {
        get; set;
    }

    #endregion

    public string GenerateHtml()
    {
        return TheClass switch
        {
            "dropitem" => DropItem(),
            "separator" => Separator(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string Separator()
    {
        /*
         <li class="gx-sb-item-separator"></li>
           <li class="gx-sb-title condense">Configuration</li>
         */


        var sb = new StringBuilder();
        sb.AppendLine(XmlUtil.ListIndented(null, "gx-sb-item-separator"));
        sb.AppendLine(XmlUtil.ListIndented(Legend, "gx-sb-title condense"));

        return sb.ToString();
    }

    private string DropItem()
    {
        var span = XmlUtil.Span("condense", $"{Legend}{XmlUtil.Italic("drop-arrow ri-arrow-down-s-line",null)}");

        var headlineContent = $"{XmlUtil.Italic( Icon ??"ri-dashboard-3-line", null)}{span}";
        var headlineAnchorDropToggle = XmlUtil.Anchor(theClass: "gx-drop-toggle", href: "javascript:void(0)", content: headlineContent);


        var dropItemContent = $"{headlineAnchorDropToggle}{NavLinksHtml()}";

        return XmlUtil.ListIndented(content: dropItemContent, theclass: "gx-sb-item sb-drop-item");
    }

    private string NavLinksHtml()
    {
        var sb = new StringBuilder();

        foreach (var navLink in NavLinks)
        {

            var anchorContent = $"{XmlUtil.Italic("ri-record-circle-line",null)}{navLink.Legend}";

            var anchor = XmlUtil.Anchor(theClass: "gx-page-link drop", href: navLink.Href, content: anchorContent);

            var li = XmlUtil.ListIndented(content: anchor, theclass: "list");

            sb.AppendLine(li);
        }

        return XmlUtil.UList("gx-sb-drop condense", sb.ToString());

    }
}