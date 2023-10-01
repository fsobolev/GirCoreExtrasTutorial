using System.IO;
using System.Reflection;
using System.Xml;

namespace Tutorial;

public class Builder
{
    public static Gtk.Builder FromFile(string name)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        using var reader = new StreamReader(stream!);
        var uiContents = reader.ReadToEnd();
        var xml = new XmlDocument();
        xml.LoadXml(uiContents);
        return Gtk.Builder.NewFromString(xml.OuterXml, -1);
    }
}