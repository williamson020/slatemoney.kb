using System.Xml.Serialization;

namespace Slate.KnowledgeBase.Preprocessor;
public class Topic
{
    [XmlAttribute("href")]  
    public string Href { get; set; }  
     
    [XmlAttribute("legend")]  
    public string Legend { get; set; }


    [XmlAttribute("subtopic")]  
    public string Subtopic { get; set; }


    /// <summary>
    /// External kb id (handle) used to reference this topic
    /// </summary>

    [XmlAttribute("exref")]  
    public string ExternalReference { get; set; }


    [XmlIgnore]
    public string ClassNames => Subtopic == "true" ? "sub-topic topic" : "topic";

    public void Dump(string page) => Console.WriteLine($"{page}{Href}");

    public void DumpExRef(string page)
    {
	    if (ExternalReference != null)
	    {
		    Console.WriteLine(ExRef.Make(page, Href, ExternalReference));
	    }
    }

}



