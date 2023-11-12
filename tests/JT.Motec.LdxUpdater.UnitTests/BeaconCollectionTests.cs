using System.Xml.Linq;
using FluentAssertions;
using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater.UnitTests;

public class BeaconCollectionTests
{
    [Test]
    public void ToXElement_Returns_Expected_Data()
    {
        var becaon = new Beacon("Manual.1", 161520000.096569419m);

        var collection = new BeaconCollection();
        collection.Add(becaon);
        
        
        var expectedString = """
                             <MarkerGroup Name="Beacons" Index="2"><Marker Version="100" ClassName="BCN" Name="Manual.1" Flags="77" Time="1.61520000096569419e+08"/></MarkerGroup>
                             """;

        var expected = XElement.Parse(expectedString);

        var result = collection.ToXElement();

        result.Should().BeEquivalentTo(expected);
    }
}