using System.Collections;
using System.Xml.Linq;

namespace JT.Motec.LdxUpdater.Common;

public class BeaconCollection : ICollection<Beacon>
{
    private readonly List<Beacon> _beacons = new();
    
    public IEnumerator<Beacon> GetEnumerator() => _beacons.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(Beacon item) => _beacons.Add(item);

    public void Clear() => _beacons.Clear();

    public bool Contains(Beacon item) => _beacons.Contains(item);

    public void CopyTo(Beacon[] array, int arrayIndex) => _beacons.CopyTo(array, arrayIndex);

    public bool Remove(Beacon item) => _beacons.Remove(item);

    public int Count => _beacons.Count;
    public bool IsReadOnly => false;

    public XElement ToXElement()
    {
        var element = new XElement("MarkerGroup");
        element.SetAttributeValue(XName.Get("Name"), "Beacons");
        element.SetAttributeValue(XName.Get("Index"), (_beacons.Count+1).ToString());

        foreach (var beacon in _beacons)
        {
            element.Add(beacon.ToXElement());
        }
        
        return element;
    }

    public IReadOnlyCollection<Beacon> AsReadOnly() => _beacons.AsReadOnly();

    public Beacon this[int lapNumber] => _beacons[lapNumber];
}