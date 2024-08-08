using System.Xml.Serialization;

namespace Slate.KnowledgeBase.Preprocessor;
public class NavLink
{
    
    [XmlAttribute("legend")]  
    public string Legend { get; set; }  
     
    [XmlAttribute("href")]  
    public string Href { get; set; }
}
