using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SunriseSunsetApp.Components.Pages
{
    public class CoordinatesBase : ComponentBase
    {
        public string Latitude { get; set; } 
        private double latitude = 0;
        private double longitude = 0;
        private DateTime date = DateTime.Now;
        public string Longitude { get; set; }
        //public IMask mask1 = new RegexMask(@"^[0-5]?(?:\d(?::(?:[0-5]?\d)?)?)?$");
        public IMask mask1 = new RegexMask(@"^(?<lat>-?(90|(\d|[1-8]\d)(\.\d{1,6})?))\s*,\s*(?<long>-?(180|(\d|\d\d|1[0-7]\d)(\.\d{1,6})?))$");

        public async Task Apply()
        {
            await Task.Delay(100);
            if (double.TryParse(Latitude, out latitude) && double.TryParse(Longitude, out longitude))
            {
                //NavigationManager.NavigateTo($"/coordinates/{latitude}/{longitude}/{date:yyyy-MM-dd}");
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add("Title", "Error");
                parameters.Add("Message", "Invalid coordinates");
                //var dialog = DialogService.Show<Dialogs.ErrorDialog>("Error", parameters);
                //var result = await dialog.Result;
            }
            //NavigationManager.NavigateTo($"/coordinates/{latitude}/{longitude}/{date:yyyy-MM-dd}");
        }

        public async Task Revert()
        {
            await Task.Delay(100);
            Latitude = "-33.35035690634893";
            Longitude = "18.47711460013822";
            StateHasChanged();
        }
    }
}
