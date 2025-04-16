using Microsoft.AspNetCore.Components;
using SunriseSunsetApp.Data;
using SunriseSunsetApp.Helpers;
using SunriseSunsetApp.Models;
using Results = SunriseSunsetApp.Models.Results;

namespace SunriseSunsetApp.Components.Pages
{
    public class SunriseSunsetBase : ComponentBase
    {
        protected SunriseSunsetModel sunriseSunsetModelYzerFontein = new SunriseSunsetModel();
        protected SunriseSunsetModel sunriseSunsetModelCapeTown = new SunriseSunsetModel();
        protected SunriseSunsetModel sunriseSunsetModelHeverlee = new SunriseSunsetModel();
        protected int timezone = 2;
        protected string dayLengthDifference = string.Empty;

        //Erna
        // private double latitude = -33.35035690634893;
        // private double longitude = 18.14970686674384;
        //Marius
        //private double latitude = 50.853520152481615;
        //private double longitude = 4.693098052258068;
        //Cape Town
        // private double latitude = -33.93408625718356;
        // private double longitude = 18.47711460013822;

        private bool getSunriseSunsetError;

        private string FormatDuration(TimeSpan duration)
        {
            return $"{duration.Hours:D2}h {duration.Minutes:D2}m {duration.Seconds:D2}s";
        }

        protected override async Task OnInitializedAsync()

        {
            await Task.Delay(500);
            // Erna
            sunriseSunsetModelYzerFontein = GetSunriseSunsetData(-33.35035690634893, 18.14970686674384, timezone);

            // Cape Town
            sunriseSunsetModelCapeTown = GetSunriseSunsetData(-33.35035690634893, 18.47711460013822, timezone);

            // Marius
            timezone = TimeZones.GetBelgiumTimeZone(DateTime.Now);
            sunriseSunsetModelHeverlee = GetSunriseSunsetData(50.853520152481615, 4.693098052258068, timezone);

            if (sunriseSunsetModelHeverlee.results != null && sunriseSunsetModelYzerFontein.results != null)
            {
                /*  var totalSeconds = Math.Abs(sunriseSunsetModelHeverlee.results.dayLength - sunriseSunsetModelYzerFontein.results.dayLength);
                var duration = TimeSpan.FromSeconds(totalSeconds);
                dayLengthDifference = FormatDuration(duration);*/

                dayLengthDifference = TimeSpan.FromHours(
                    Math.Abs(sunriseSunsetModelHeverlee.results.dayLength - sunriseSunsetModelYzerFontein.results.dayLength)).ToString(@"hh\:mm\:ss");
            }
            else
            {
                dayLengthDifference = "N/A";
            }
        }

        private SunriseSunsetModel GetSunriseSunsetData(double Latitude, double Longitude, int TimeZone)
        {
            DateTime today = DateTime.Now;

            double sunriseHours,
                   sunsetHours,
                   CivilDawnHours,
                   CivilTwilightHours,
                   NauticalDawnHours,
                   NauticalTwilightHours,
                   AstronomicalDawnHours,
                   AstronomicalTwilightHours
                   ;
            SunriseSunsetHelper.SunriseSunset(today, Latitude, Longitude, TimeZone, out sunriseHours, out sunsetHours);
            SunriseSunsetHelper.AstronomicalTwilight(today, Latitude, Longitude, TimeZone, out AstronomicalDawnHours, out AstronomicalTwilightHours);
            SunriseSunsetHelper.NauticalTwilight(today, Latitude, Longitude, TimeZone, out NauticalDawnHours, out NauticalTwilightHours);
            SunriseSunsetHelper.CivilTwilight(today, Latitude, Longitude, TimeZone, out CivilDawnHours, out CivilTwilightHours);

            var sunriseSunsetModel = new SunriseSunsetModel()
            {
                results = new Results()
                {
                    sunrise = TimeSpan.FromHours(sunriseHours).ToString(@"hh\:mm\:ss"),
                    sunset = TimeSpan.FromHours(sunsetHours).ToString(@"hh\:mm\:ss"),
                    civil_twilight_begin = TimeSpan.FromHours(CivilDawnHours).ToString(@"hh\:mm\:ss"),
                    civil_twilight_end = TimeSpan.FromHours(CivilTwilightHours).ToString(@"hh\:mm\:ss"),
                    nautical_twilight_begin = TimeSpan.FromHours(NauticalDawnHours).ToString(@"hh\:mm\:ss"),
                    nautical_twilight_end = TimeSpan.FromHours(NauticalTwilightHours).ToString(@"hh\:mm\:ss"),
                    astronomical_twilight_begin = TimeSpan.FromHours(AstronomicalDawnHours).ToString(@"hh\:mm\:ss"),
                    astronomical_twilight_end = TimeSpan.FromHours(AstronomicalTwilightHours).ToString(@"hh\:mm\:ss"),
                    dayLength = sunsetHours - sunriseHours,
                    solar_noon = TimeSpan.FromHours((sunriseHours + sunsetHours) / 2).ToString(@"hh\:mm\:ss")
                },
                status = "OK",
                tzid = "Europe/Brussels"
            };

            sunriseSunsetModel.results.day_length = TimeSpan.FromHours(sunriseSunsetModel.results.dayLength).ToString(@"hh\:mm\:ss");


            //int getGoogleTimeZoneWithClientAsync = TimeZones.GetGoogleTimeZoneWithClientAsync(latitude, longitude).Result;

            //string json = JsonConvert.SerializeObject(sunriseSunSetModel, Formatting.Indented);

            return sunriseSunsetModel;
        }
    }
}
