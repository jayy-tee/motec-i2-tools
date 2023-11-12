using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater.RacePak;

public class CsvBeaconDetector
{
    public ICollection<Beacon> DetectBeacons(string fileName)
    {
        var beacons = new List<Beacon>();
        
        using var file = File.OpenText(fileName);

        string[]? previousDataRow = null;
        string[] header;
        var headerRead = false;

        var config = new RacePakCsvColumnConfiguration();
        double? fileStartTime = null;

        var lineNumber = 0;
        
        var checkForIncrementingLapTimeOrDistance = false;

        int? lapTimeColumnIndex = null;
        int? lapDistanceColumnIndex = null;

        while (!file.EndOfStream)
        {
            var line = file.ReadLine();
            var currentDataRow = line!.Split(',', StringSplitOptions.TrimEntries);

            if (!headerRead)
            {
                header = currentDataRow;

                lapTimeColumnIndex = Array.FindIndex(header, headerValue => headerValue == config.LapTimeColumnName);
                lapDistanceColumnIndex = Array.FindIndex(header, headerValue => headerValue == config.LapDistanceColumnName);
                
                headerRead = true;
                continue;
            }


            lineNumber++;

            double.TryParse(currentDataRow[0], out var currentSampleTime);

            fileStartTime ??= currentSampleTime;

            if (double.TryParse(currentDataRow[lapTimeColumnIndex!.Value], out var currentSampleLapTime) && double.TryParse(previousDataRow?[lapTimeColumnIndex!.Value], out var previousSampleLapTime))
            {
                double.TryParse(currentDataRow[lapDistanceColumnIndex!.Value], out var currentSampleLapDistance);
                double.TryParse(previousDataRow[lapDistanceColumnIndex!.Value], out var previousSampleLapDistance);

                if (checkForIncrementingLapTimeOrDistance && (currentSampleLapDistance > previousSampleLapDistance || currentSampleLapTime > previousSampleLapTime))
                {
                    GenerateBeacon(currentSampleTime);
                    checkForIncrementingLapTimeOrDistance = false;
                }

                if (currentSampleLapTime < previousSampleLapTime)
                {
                    // Indicate that we should detect beacon if next sample INCREMENTS lap time
                    checkForIncrementingLapTimeOrDistance = true;
                } 
                
                if (beacons.Count == 0 && currentSampleLapTime > previousSampleLapTime && previousSampleLapTime <= 0 && currentSampleTime > 30)
                {
                    GenerateBeacon(currentSampleTime);
                }
            }

            previousDataRow = currentDataRow;
        }

        void GenerateBeacon(double result)
        {
            var beaconTime = ((decimal)result - (decimal)fileStartTime) * 1000000;

            var lastRecordedLap = beacons.LastOrDefault();
            if (lastRecordedLap is null || (beaconTime - lastRecordedLap.Time) > 1_000_000)
            {
                beacons.Add(new Beacon($"Autodetected Lap: {beacons.Count+1}", beaconTime));
            }
        }

        return beacons;
    }
}