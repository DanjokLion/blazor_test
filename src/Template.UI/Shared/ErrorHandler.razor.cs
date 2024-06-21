using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Template.UI.Shared
{
    /// <summary>
    /// Компонент обработчика ошибок
    /// </summary>
    public partial class ErrorHandler : ComponentBase
    {
        [Inject] private ISnackbar _snackbar { get; set; }

        /// <summary>
        /// Пользовательский интерфейс в котором произошло исключение. 
        /// Для отлова всех ошибок возникших на стороне пользователя
        /// требуется обернуть компонент Router находящийся в компоненте App
        /// в компонент обработчика ошибок (см файл App.razor).
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Метод логирующий ошибку с выводом сообщения пользователю через snackbar.
        /// </summary>
        /// <param name="ex">Ошибка.</param>
        /// <param name="snackbarMessage">Сообщение пользователю</param>
        public void ProcessError(Exception ex, string snackbarMessage)
        {
            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                ex.GetType(), ex.Message);

            _snackbar.Add(snackbarMessage, Severity.Error);
        }
    }
}
