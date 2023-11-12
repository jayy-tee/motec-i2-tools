using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater;

public static class CsvDataSource
{
    public static Dictionary<string, LogMetadata> CreateLogsFromCsvData(string filename)
    {
        using var file = File.OpenText(filename);
        var logs = new Dictionary<string, LogMetadata>();

        var row = 0;
        while (!file.EndOfStream)
        {
            row++;

            var line = file.ReadLine();
            var currentDataRow = line!.Split(',', StringSplitOptions.TrimEntries);

            if (row == 1 || currentDataRow[7] == string.Empty)
            {
                continue;
            }

            var log = new LogMetadata
            {
                Event = currentDataRow[11],
                Venue = currentDataRow[10],
                VenueLengthMeters = int.Parse(currentDataRow[12]),
                Driver = currentDataRow[8],
                Session = currentDataRow[4],
                ShortComment = currentDataRow[6],
                LongComment = currentDataRow[13],
                LogDate = DateTime.Parse($"{currentDataRow[0]} {currentDataRow[5]}")
            };

            logs.Add(currentDataRow[7], log);
        }

        return logs;
    }
    
}