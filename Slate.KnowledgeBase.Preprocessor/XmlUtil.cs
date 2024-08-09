using System.Formats.Asn1;
using System.Text;

namespace Slate.KnowledgeBase.Preprocessor;
internal static class XmlUtil
{
    const char doubleQuote = '"';

    public static T DeserializeToObject<T>(string filepath) where T : class
    {

        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException(filepath);
        }

        var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

        using var sr = new StreamReader(filepath);
        return (T)ser.Deserialize(sr);
    }

    public static string Quoted(string str) => $"{doubleQuote}{str}{doubleQuote}";
    

    public static string UList(string theClass, string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<ul class={Quoted(theClass)}>");

        sb.Append(content);

        sb.Append(@"</ul>");


        return sb.ToString();
    }

    public static string Nav(string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<nav>");

        sb.Append(content);

        sb.Append(@"</nav>");


        return sb.ToString();
    }

    public static string ListIndented(string content, string theclass=null)
    {
        var sb = new StringBuilder();

        if (theclass != null)
        {
            sb.Append($"<li class={Quoted(theclass)}>");
        }
        else
        {
            sb.Append("<li>");
        }
        

        sb.Append(content);

        sb.Append(@"</li>");


        return sb.ToString();
    }


    public static string Image(string src, string alt)
    {
        var sb = new StringBuilder();
        sb.Append($"<img src={Quoted(src)} alt={Quoted(alt)}>");

    

        sb.Append("</img>");


        return sb.ToString();
    }

    public static string Anchor(string theClass, string href, string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<a class={Quoted(theClass)} ");

        sb.Append($"href={Quoted(href)}>");
        sb.Append(content);

        sb.Append("</a>");


        return sb.ToString();
    
    }

    public static  string DivClass(string theClass, string content, string datamode=null)
    {
        var sb = new StringBuilder();

        if (datamode == null)
        {
            sb.Append($"<div class={Quoted(theClass)}>");
        }
        else
        {
            sb.Append($"<div class={Quoted(theClass)} data-mode={Quoted(datamode)}>");
        }
      

        sb.Append(content);

        sb.Append(@"</div>");


        return sb.ToString();


    }

    public static string Span(string theClass, string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<span class={Quoted(theClass)}>");

        sb.Append(content);

        sb.Append("</span>");


        return sb.ToString();
    }


    public static string Italic(string theClass, string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<i class={Quoted(theClass)}>");

        sb.Append(content);

        sb.Append("</i>");


        return sb.ToString();


    }
    public static string H4(string theClass, string content)
    {
        var sb = new StringBuilder();
        sb.Append($"<h4 class={Quoted(theClass)}>");

        sb.Append(content);

        sb.Append("</h4>");


        return sb.ToString();


    }

    public static string Title(string legend) => $"<title>{legend}</title>";


    public static string AsComment(string token) => $"<!-- {token} -->";

    public static string Comment(string comment) => AsComment(comment);



}
