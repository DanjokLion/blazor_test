using Microsoft.AspNetCore.Components;
using Template.UI.Data;
using Template.UI.Shared;

namespace Template.UI.Components
{
    public partial class Auto : ComponentBase
    {
        /// <summary>
        /// Внедрение обработчика ошибок через CascadingParameter
        /// </summary>
        [CascadingParameter] public ErrorHandler? ErrorHandler { get; set; }

        private string _searchString = string.Empty;

        /// <summary>
        /// Список автомобилей для рендеринга таблицы с шаблоном обрабоки ошибок
        /// </summary>
        [Parameter] public List<AutoDto> Elements { get; set; } = new List<AutoDto>()
        {
            new AutoDto
            {
                Id = 1,
                Colour = "White",
                Year = 2005,
                Company = "BMW",
                Desctiption = "Белый BMW 2005 года"
            },
             new AutoDto
            {
                Id = 2,
                Colour = "Red",
                Year = 2015,
                Company = "Audi",
                Desctiption = "Красный audi 2015 года"
            },
              new AutoDto
            {
                Id = 2,
                Colour = "Green",
                Year = 2021,
                Company = "Volkswagen",
                Desctiption = "Зеленый volkswagen 2021 года"
            }
        };

        /// <summary>
        /// Генерация не обработанного исключения. 
        /// Исключение ловиться и обрабатывается в компоненте CustomErrorBoundary
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void GenerateEditException()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Генерация обрабатываемого компонентом ErrorHandler исключения
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void GenerateDeliteException()
        {
            try
            {
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                var snackbarMessage = "Произошла ошибка во время удаления объекта";
                ErrorHandler?.ProcessError(ex, snackbarMessage);
            }
        }
    }
}