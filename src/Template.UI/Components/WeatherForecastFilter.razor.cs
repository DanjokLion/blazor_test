using Microsoft.AspNetCore.Components;
using MudBlazor;
using Template.UI.Data;

namespace Template.UI.Components
{
    public partial class WeatherForecastFilter :ComponentBase
    {
        [Parameter] public EventCallback<WeatherForecastFilterModel> OnClick { get; set; }

        private WeatherForecastFilterModel _filters = new();

        public async Task ClearFiltersAsync()
        {
            _filters = new WeatherForecastFilterModel();
            await SubmitFiltersAsync();
        }
        public async Task SubmitFiltersAsync()
        {
            await OnClick.InvokeAsync(_filters);
        }
    }
}
