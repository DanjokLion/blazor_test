using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Template.UI.Data;

namespace Template.UI.Components;

public partial class WeatherForecastList : ComponentBase
{
    [Inject] private ISnackbar _snackbar { get; set; }

    [Inject] private WeatherForecastService _service { get; set; }

    [Inject] private IClipboardService _clipboardService { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    private IEnumerable<WeatherForecast> _data = new List<WeatherForecast>();

    private WeatherForecastFilterModel _filter = new ();

    private WeatherForecastEdit _editDialog = new();

    private bool _isLoading =true;


    private string _searchString = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await RefreshAsync();
    }

    private bool FilterFunc(WeatherForecast element)
    {
        Regex regex = new(_searchString, RegexOptions.IgnoreCase);
        var column = new List<string?>
        {
            element.Id.ToString(),
            element.Summary,
            element.TemperatureC.ToString(),
            element.Date.ToString(),
        };
        return column.Any(o => regex.IsMatch(o ?? string.Empty));
    }

    private async Task RefreshAsync()
    {
        _data = await _service.LazyGet(_filter);

        _isLoading=false;
        
    }

    private async Task EditClickAsync(WeatherForecast model)
    {
        await _editDialog.Open(model);

    }


    private async Task UserDialogCallbackAsync(WeatherForecast dialogData)
    {
        var id =  await _service.Update(dialogData);
        var message = dialogData.isDelete ?
            $"Запись {id} удалена":
            $"Изменения {id} сохранены";

        _logger.LogInformation("Сообщение пользователю: {message}", message);
        _snackbar.Add(message, Severity.Success);

        await RefreshAsync();
    }

    private async Task FilterAsync(WeatherForecastFilterModel dialogData)
    {
        _isLoading = true;
        _filter = dialogData;
        await RefreshAsync();
    }

    public async void Copy(Guid id)
    {
        string result = $"Значение {id} скопировано в буфер обмена.";
        Severity level = Severity.Info;
        try
        {
            await _clipboardService.CopyToClipboard(id.ToString());
            _logger.LogInformation("Значение Id: {id} скопировано в буфер обмена", id);

        }
        catch (Exception ex)
        {
            result = "Ошибка копирования в буфер";
            level = Severity.Error;
            _logger.LogError(ex, "Ошибка копирования в буфер");
        }
        _snackbar.Add(result, level);
    }

    private async Task AddClickAsync()
    {
        await _editDialog.Open(new WeatherForecast());

    }

    private async Task DeleteClickAsync(WeatherForecast dialogData)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = await DialogService.ShowAsync<SubmitDialog>($"Удаление {dialogData.Id}", options);

        var res = await dialog.Result;

        if (res.Cancelled)
        {
            return;
        }

        _isLoading = true;

        await _service.Remove(dialogData.Id);

        _snackbar.Add($"Запись удалена {dialogData.Id}", Severity.Success);
        _logger.LogInformation("Запись удалена {id}", dialogData.Id);
        await RefreshAsync();

    }

}