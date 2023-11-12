using FluentAssertions;
using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater.UnitTests;

public class LogMetadataTests
{
    [Test]
    public void AddBeacon_Generates_LapData()
    {
        var expectedFastedLapTime = new TimeSpan(0, 0, 1, 11, 960);
        var log = new LogMetadata();
        var beaconTimes = GetBeaconTimes();

        for (var i = 0; i < beaconTimes.Length; i++)
        {
            log.AddBeacon($"Manual.{(i+1).ToString()}", beaconTimes[i]);
        }

        log.TotalLaps.Should().Be(beaconTimes.Length-1, because: "in laps are not included in the total lap count");
        log.FastestLapTime.Should().BeCloseTo(expectedFastedLapTime, TimeSpan.FromMilliseconds(1));
        log.FastestLap.Should().Be(4);
    }

    private static decimal[] GetBeaconTimes()
    {
        // These are beacon markers taken from a real log file. In this data, the fastest time was 1:11.960
        return new[]
        {
            TimeConverter.Convert("1.61520000096569419e+08"),
            TimeConverter.Convert("2.43820003400347769e+08"),
            TimeConverter.Convert("3.20680000311890006e+08"),
            TimeConverter.Convert("3.94920000276114047e+08"),
            TimeConverter.Convert("4.66880000276114047e+08"),
            TimeConverter.Convert("5.42119997123216271e+08"),
            TimeConverter.Convert("6.17919997132000327e+08")
        };
    }
}