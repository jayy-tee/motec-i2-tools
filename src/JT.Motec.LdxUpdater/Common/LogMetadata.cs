using System.Xml;

namespace JT.Motec.LdxUpdater.Common;

public class LogMetadata
{
    private readonly BeaconCollection _beacons = new();
    private readonly List<Lap> _laps = new();

    public IReadOnlyCollection<Lap> Laps => _laps.AsReadOnly();

    public string? Event { get; set; }
    public string? Venue { get; set; }
    public int? VenueLengthMeters { get; set; }
    public string? Driver { get; set; }
    public string? Team { get; set; }
    public string? VehicleId { get; set; }
    public string? VehicleNumber { get; set; }
    public string? VehicleDescription { get; set; }
    public string? EngineId { get; set; }
    public string? Session { get; set; }
    public string? StartLap { get; set; }
    public string? ShortComment { get; set; }
    public string? LongComment { get; set; }
    public DateTime? LogDate { get; set;}
    public string? Sky { get; set; }
    public string? WindDirection { get; set; }
    public string? WeatherComment { get; set; }
    public string? VehicleType { get; set; }
    public string? VehicleDriveType { get; set; }
    public string? VehicleComment { get; set; }
    public int TotalLaps => _beacons.Count - 1;
    public TimeSpan? FastestLapTime { get; private set; }
    public int? FastestLap { get; private set; }

    public void AddBeacon(string name, decimal time)
    {
        _beacons.Add(new Beacon(name, time));
        GenerateLaps();
    }

    private void GenerateLaps()
    {
        _laps.Clear();
        
        var lapNumber = 0;
        foreach (var beacon in _beacons)
        {
            var timeStart = lapNumber > 1 ? _beacons[lapNumber - 1].Time : 0m;

            var lap = new Lap(lapNumber, timeStart, beacon.Time);
            _laps.Add(lap);

            if (FastestLapTime is null || FastestLapTime > lap.LapTime)
            {
                FastestLapTime = lap.LapTime;
                FastestLap = lapNumber;
            }

            lapNumber++;
        }
    }
}