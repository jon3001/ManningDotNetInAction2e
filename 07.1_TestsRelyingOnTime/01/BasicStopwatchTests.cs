namespace TestsRelyingOnTime;

public class BasicStopwatchTests
{
    [Theory]
    [InlineData(25)]     /* typically fails due to system overheads at 5ms */
    [InlineData(100)]
    public void RestartStopwatch(int delayMs)
    {
        TimeSpan delay = TimeSpan.FromMilliseconds(delayMs);

        var stopwatch = new BasicStopwatch();
        stopwatch.Start();
        Thread.Sleep(delay);
        stopwatch.Stop();

        stopwatch.Start();
        Thread.Sleep(delay);
        stopwatch.Stop();

        Assert.NotNull(stopwatch.ElapsedTimeSpan);
        Assert.True(stopwatch.ElapsedTimeSpan >= delay);
        Assert.True(stopwatch.ElapsedTimeSpan < (delay * 2));
    }

}
