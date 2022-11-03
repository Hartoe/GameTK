namespace GameTK;

/// <summary>
/// Helper class that keeps track of the time that the game has been running
/// </summary>
public class GameTime
{
    public TimeSpan TotalGameTime { get; set; }
    public TimeSpan ElapsedGameTime { get; set; }

    public GameTime()
    {
        TotalGameTime = TimeSpan.Zero;
        ElapsedGameTime = TimeSpan.Zero;
    }

    public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
    {
        TotalGameTime = totalGameTime;
        ElapsedGameTime = elapsedGameTime;
    }
}