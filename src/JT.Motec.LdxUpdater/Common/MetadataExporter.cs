using System.Xml.Linq;

namespace JT.Motec.LdxUpdater.Common;

public class MetadataExporter
{
    public void Export(string exportFileName, LogMetadata log)
    {

        var ldxFile = new XElement("LDXFile");
        ldxFile.SetAttributeValue(XName.Get("Locale"), "English_Australia.1252");
        ldxFile.SetAttributeValue(XName.Get("DefaultLocale"), "C");
        ldxFile.SetAttributeValue(XName.Get("Version"), "1.6");
        
        var layers = new XElement("Layers",
            new XElement("Layer",
                new XElement("MarkerBlock", log.GetBeaconXml())));


        var details = new XElement("Details");

        if (!string.IsNullOrWhiteSpace(log.Event))
        {
            details.Add(CreateString("Event", log.Event));
        }
        
        if (!string.IsNullOrWhiteSpace(log.Driver))
        {
            details.Add(CreateString("Driver", log.Driver));
        }
        
        if (!string.IsNullOrWhiteSpace(log.Venue))
        {
            details.Add(CreateString("Venue", log.Venue));
        }
        
        if (!string.IsNullOrWhiteSpace(log.Venue))
        {
            details.Add(CreateString("Venue", log.Venue));
        }

        if (log.VenueLengthMeters.HasValue)
        {
            details.Add(CreateNumeric("Venue Length", log.VenueLengthMeters.ToString()!, "m", "0"));
        }

        if (log.LogDate.HasValue)
        {
            details.Add(CreateDateTime("Log Date", log.LogDate.Value.ToString("d/MM/yyyy")));
            details.Add(CreateDateTime("Log Time", log.LogDate.Value.ToString("H:mm:s")));
        }

        if (log.TotalLaps > 0)
        {
            details.Add(CreateString("Fastest Time", log.FastestLapTime!.Value.ToString(@"m\:ss\.fff")));
            details.Add(CreateString("Fastest Lap", log.FastestLap!.Value.ToString()));
            details.Add(CreateString("Total Laps", log.TotalLaps.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(log.LongComment))
        {
            details.Add(CreateString("Long Comment", log.LongComment));
        }
        
        if (!string.IsNullOrWhiteSpace(log.ShortComment))
        {
            details.Add(CreateString("Short Comment", log.ShortComment));
        }
        
        if (!string.IsNullOrWhiteSpace(log.Session))
        {
            details.Add(CreateString("Session", log.Session));
        }
        
        layers.Add(details);
        ldxFile.Add(layers);

        ldxFile.Save(exportFileName);
    }

    private static XElement CreateString(string id, string value)
    {
        var xml = new XElement("String");
        
        xml.SetAttributeValue(XName.Get("Id"), id);
        xml.SetAttributeValue(XName.Get("Value"), value);

        return xml;
    }
    
    private static XElement CreateNumeric(string id, string value, string unit, string dps)
    {
        var xml = new XElement("Numeric");
        
        xml.SetAttributeValue(XName.Get("Id"), id);
        xml.SetAttributeValue(XName.Get("Value"), value);
        xml.SetAttributeValue(XName.Get("Unit"), unit);
        xml.SetAttributeValue(XName.Get("DPS"), dps);

        return xml;
    }
    
    private static XElement CreateDateTime(string id, string value)
    {
        var xml = new XElement("DateTime");
        
        xml.SetAttributeValue(XName.Get("Id"), id);
        xml.SetAttributeValue(XName.Get("Value"), value);

        return xml;
    }

}