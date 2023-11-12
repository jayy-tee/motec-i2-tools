using System.Xml.Linq;
using FluentAssertions;
using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater.UnitTests;

public class BeaconTests
{
    [Test]
    public void ToXElement_Returns_Expected_Shape()
    {
        var becaon = new Beacon("Manual.1", 161520000.096569419m);
        var expectedString = """
                             <Marker Version="100" ClassName="BCN" Name="Manual.1" Flags="77" Time="1.61520000096569419e+08"/>
                             """;

        var expected = XElement.Parse(expectedString);

        var result = becaon.ToXElement();

        result.Should().BeEquivalentTo(expected);
    } 
}