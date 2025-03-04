namespace TestsRelyingOnTime;

public class BasicStopwatch
{
    private DateTime? _startTime;
    private DateTime? _stopTime;

    public void Start()
    {
        _startTime = DateTime.UtcNow;
        _stopTime = null;
    }

    public void Stop()
    {
        if (_startTime != null)
        {
            _stopTime = DateTime.UtcNow;
        }
    }

    public TimeSpan? ElapsedTimeSpan
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
