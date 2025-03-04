using Microsoft.Extensions.Time.Testing;

namespace TestsRelyingOnTime;

public class BasicStopwatchImprovedTests
{
    [Fact]
    public void RestartStopwatch()
    {
        var clock = new FakeTimeProvider(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));
        var delay = TimeSpan.FromHours(2);

        var stopwatch = new BasicStopwatchImproved(clock);
        stopwatch.Start();
        clock.Advance(delay);
        stopwatch.Stop();

        // Start should reset
        stopwatch.Start();
        clock.Advance(delay);
        stopwatch.Stop();

        Assert.NotNull(stopwatch.ElapsedTime);
        Assert.Equal(delay, stopwatch.ElapsedTime);
    }
}
