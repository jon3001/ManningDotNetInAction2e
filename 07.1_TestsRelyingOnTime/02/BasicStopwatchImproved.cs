namespace TestsRelyingOnTime;

public class BasicStopwatchImproved
{
    private static readonly TimeProvider DefaultClock = TimeProvider.System;
    private DateTimeOffset? _startTime;
    private DateTimeOffset? _stopTime;
    private readonly TimeProvider _clock;

    public BasicStopwatchImproved(TimeProvider? systemClock = null)
    {
        _clock = systemClock ?? DefaultClock;
    }

    public void Start()
    {
        _startTime = _clock.GetUtcNow();
        _stopTime = null;
    }

    public void Stop()
    {
        if (_startTime != null)
        {
            _stopTime = _clock.GetUtcNow();
        }
    }

    public TimeSpan? ElapsedTime
    {
        get
        {
            if (_startTime != null && _stopTime != null)
            {
                return _stopTime - _startTime;
            }

            return null;
        }
    }
}

