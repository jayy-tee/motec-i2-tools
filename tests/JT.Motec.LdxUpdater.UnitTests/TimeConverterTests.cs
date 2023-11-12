using FluentAssertions;
using JT.Motec.LdxUpdater.Common;

namespace JT.Motec.LdxUpdater.UnitTests;

public class TimeConverterTests
{

    [Test]
    public void Convert_GivenValidTimeString_ReturnsExpectedDecimal()
    {
        var timeAsString = "1.61520000096569419e+08";
        var expected = 161520000.096569419m;
        
        var result = TimeConverter.Convert(timeAsString);

        result.Should().Be(expected);
    }

    [Test]
    public void DecimalToMotecString_Should_Return_WithCorrectAccuracy()
    {
        var expectedString = "1.61520000096569419e+08";
        var time = 161520000.096569419m;
        
        var result = TimeConverter.DecimalToMotecString(time);

        result.Should().Be(expectedString);
    }

    [Test]
    public void TimeSpanToMotecString_Should_Return_WithExpectedLevelOfAccuracy()
    {
        var input = new TimeSpan(0, 0,14, 26, 421);
        var expectedResult = "8.66421000000000000e+08";

        TimeConverter.TimeSpanToMotecString(input).Should().Be(expectedResult);
    }
}