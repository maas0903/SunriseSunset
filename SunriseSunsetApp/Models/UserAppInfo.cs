namespace SunriseSunsetApp.Models
{
    public class UserAppInfo
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool ValuesSet { get; set; } = false;

        public const string DefaultLatitude = "-33.35035690634893";
        public const string DefaultLongitude = "18.47711460013822";

        public UserAppInfo()
        {
            Latitude = double.Parse(DefaultLatitude);
            Longitude = double.Parse(DefaultLongitude);
            ValuesSet = true;
        }

        public async Task SetCoordinates(string LatitudeString = DefaultLatitude, string LongitudeString = DefaultLongitude)
        {
            await Task.Delay(100);
            try
            {
                Latitude = double.Parse(LatitudeString);
                Longitude = double.Parse(LongitudeString);
                ValuesSet = true;

            }
            catch (ArgumentException)
            {
                Latitude = double.Parse(DefaultLatitude);
                Longitude = double.Parse(DefaultLongitude);
                ValuesSet = false;
            }
        }

    }
}
