using JT.Motec.LdxUpdater;
using JT.Motec.LdxUpdater.Common;
using JT.Motec.LdxUpdater.RacePak;

var masterFile = "c:\\code\\jt.git\\motec-ldx-updater\\master.csv";
var csvDir = "c:\\racepakdata\\iq3";
var exportDir = "c:\\i2-data-test";

var logsToProcess = CsvDataSource.CreateLogsFromCsvData(masterFile);


foreach (var logToProcess in logsToProcess)
{
    var logFile = logToProcess.Value;
    var ldxFile = $"{logToProcess.Key}.ldx";
    var racepakExportFile = $"{logToProcess.Key}.csv";

    var file = Directory.GetFiles(csvDir, racepakExportFile, SearchOption.AllDirectories).First();
    
    var beacons = new CsvBeaconDetector().DetectBeacons(file);


    foreach (var beacon in beacons)
    {
        logFile.AddBeacon(beacon.Name, beacon.Time);
    }
    
    new MetadataExporter().Export($"{exportDir}\\{ldxFile}", logToProcess.Value);


    Console.WriteLine($"Found {logToProcess.Value.TotalLaps} laps");
}