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
        var expectedLapTimes = GetLapTimes();

        for (var i = 0; i < beaconTimes.Length; i++)
        {
            log.AddBeacon($"Manual.{(i+1).ToString()}", beaconTimes[i]);
        }

        log.TotalLaps.Should().Be(beaconTimes.Length-1, because: "in laps are not included in the total lap count");
        log.FastestLapTime.Should().BeCloseTo(expectedFastedLapTime, TimeSpan.FromMilliseconds(1));
        log.FastestLap.Should().Be(4);

        var laps = log.Laps.ToArray();
        laps[0].LapTime.Should().BeCloseTo(expectedLapTimes[0], TimeSpan.FromMilliseconds(1));
        laps[1].LapTime.Should().BeCloseTo(expectedLapTimes[1], TimeSpan.FromMilliseconds(1));
        laps[2].LapTime.Should().BeCloseTo(expectedLapTimes[2], TimeSpan.FromMilliseconds(1));
        laps[3].LapTime.Should().BeCloseTo(expectedLapTimes[3], TimeSpan.FromMilliseconds(1));
        laps[4].LapTime.Should().BeCloseTo(expectedLapTimes[4], TimeSpan.FromMilliseconds(1));
        laps[5].LapTime.Should().BeCloseTo(expectedLapTimes[5], TimeSpan.FromMilliseconds(1));
        laps[6].LapTime.Should().BeCloseTo(expectedLapTimes[6], TimeSpan.FromMilliseconds(1));
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

    private static TimeSpan[] GetLapTimes()
    {
        return new[]
        {
            new TimeSpan(0, 0, 2, 41, 520),
            new TimeSpan(0, 0, 1, 22, 300),
            new TimeSpan(0, 0, 1, 16, 859),
            new TimeSpan(0, 0, 1, 14, 239),
            new TimeSpan(0, 0, 1, 11, 960),
            new TimeSpan(0, 0, 1, 15, 239),
            new TimeSpan(0, 0, 1, 15, 800),
        };
    }
}