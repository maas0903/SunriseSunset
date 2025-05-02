namespace SunriseSunsetApp.Services
{
    public class SessionTracker
    {
        public Guid SessionId { get; set; } = Guid.NewGuid();
    }
}
