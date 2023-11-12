using System.Globalization;

namespace JT.Motec.LdxUpdater.Common;

public static class TimeConverter
{
    private const string MotecTimeStringFormat = "#.00000000000000000e+00";

    public static decimal Convert(string time)
    {
        return  decimal.Parse(time, NumberStyles.Float);
    }

    public static string DecimalToMotecString(decimal time) => time.ToString(MotecTimeStringFormat);
    public static string TimeSpanToMotecString(TimeSpan timeSpan) =>  ((decimal)timeSpan.TotalMicroseconds).ToString(MotecTimeStringFormat);
}