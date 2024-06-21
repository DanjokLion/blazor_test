using Microsoft.AspNetCore.Components;
using MudBlazor;
using Template.UI.Data;

namespace Template.UI.Components
{

    public partial class WeatherForecastEdit : ComponentBase
    {
        [Parameter]
        public EventCallback<WeatherForecast> OnSave { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        private bool _isLoading { get; set; }

        private WeatherForecast _this;

        private WeatherForecast _state;

        public async Task Open(WeatherForecast selected)
        {
            IsVisible = true;
            _this = selected;   
            _state = selected;
        }


        private async void Close()
        {
            if (!_this.Equals(_state))
            {
                var dialog = await DialogService.ShowAsync<SubmitDialog>("Есть несохраненные данные, продолжить?");

                var res = await dialog.Result;

                if (res.Cancelled)
                {
                    return;
                }
            }
            IsVisible = false;
            StateHasChanged();
        }

        private async Task Delete()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog =  await DialogService.ShowAsync<SubmitDialog>($"Удаление {_this.Id}", options);

            var res = await dialog.Result;

            if (res.Cancelled)
            {
                return;

            }
            _this.isDelete = true;
            await SaveAsync();
        }


        private async Task SaveAsync()
        {
            _isLoading = true;
            await OnSave.InvokeAsync(_this);
            _isLoading = false;
            _state = _this;
            Close();

        }
        
    }
}
