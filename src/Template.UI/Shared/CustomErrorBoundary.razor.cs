using Microsoft.AspNetCore.Components.Web;

namespace Template.UI.Shared
{
    public partial class CustomErrorBoundary : ErrorBoundary
    {
        /// <summary>
        /// Список возникших исключений
        /// </summary>
        private readonly List<Exception> receivedExceptions = new();

        /// <summary>
        /// Переопределение метода отлавливающего исключения
        /// требуется, что бы можно было собирать больше одного
        /// исключения. Требуется, что бы пользовательский интерфейс
        /// не ломался при возникновении нескольких необработанных исключения
        /// </summary>
        /// <param name="exception">Исключение.</param>
        protected override Task OnErrorAsync(Exception exception)
        {
            receivedExceptions.Add(exception);
            return base.OnErrorAsync(exception);
        }

        /// <summary>
        /// Метод для очистки списка пойманных исключений
        /// </summary>
        public new void Recover()
        {
            receivedExceptions.Clear();
            base.Recover();
        }
    }
}
