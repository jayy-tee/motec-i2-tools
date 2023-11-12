namespace JT.Motec.LdxUpdater.Common;

public class Lap
{
    public decimal TimeStart { get; }
    public decimal TimeFinish { get; }
    public int Number { get; }
    public TimeSpan LapTime { get; }
    public decimal LapTimeMicroseconds { get; }

    public Lap(int number, decimal timeStart, decimal timeFinish)
    {
        TimeStart = timeStart;
        TimeFinish = timeFinish;
        Number = number;
        LapTimeMicroseconds = timeFinish - timeStart;
        LapTime = TimeSpan.FromMicroseconds(decimal.ToDouble(LapTimeMicroseconds));
    }
    
    public new string ToString()
    {
        return Number == 0 ? 
            $@"Out Lap: {LapTime:m\:ss\.fff}"
            : $@"Lap {Number}: {LapTime:m\:ss\.fff}";
    }
}