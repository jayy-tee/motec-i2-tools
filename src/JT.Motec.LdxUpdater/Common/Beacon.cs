using System.Xml.Linq;

namespace JT.Motec.LdxUpdater.Common;

public class Beacon
{
    public Beacon(string name, decimal time)
    {
        Name = name;
        Time = time;
    }

    public decimal Time { get; init; }
    public string Name { get; init; }

    protected const string ClassName = "BCN";
    private const string Version = "100";
    private const string Flags = "77";

    public virtual XElement ToXElement()
    {
        var element = new XElement("Marker");
        element.SetAttributeValue(XName.Get(nameof(Version)), Version);
        element.SetAttributeValue(XName.Get(nameof(ClassName)), ClassName);
        element.SetAttributeValue(XName.Get(nameof(Name)), Name);
        element.SetAttributeValue(XName.Get(nameof(Flags)), Flags);
        element.SetAttributeValue(XName.Get(nameof(Time)), TimeConverter.DecimalToMotecString(Time));
        
        return element;
    }
}