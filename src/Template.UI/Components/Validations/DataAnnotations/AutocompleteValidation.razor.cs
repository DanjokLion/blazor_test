using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Template.UI.Components.Validations.DataAnnotations
{
    public partial class AutocompleteValidation : ComponentBase
    {
        private bool success;
        private TestedValues _choice = new();
        private EditContext _editContext;

        private void HandleInvalidSubmit()
        {

        }

        private void HandelValidSubmit()
        {

        }

        protected override void OnInitialized()
        {
            _editContext = new EditContext(_choice);
        }

        private static string[] _carTypes =
        {
        "Седан", "Универсал", "Хетчбэк", "Лифтбэк",
        "Лимузин", "Пикап", "Минивэн", "Купе",
        "Четырехдверное купе", "Масл кар", "Кабриолет",
        "Фаэтон", "Ландо", "Кроссовер", "Родстер", "Внедорожник"
    };

        private async Task<IEnumerable<string>> SearchAsync(string value)
        {
            // в реальном примере тут может быт обращение к бд с получением значений для валидации
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
            {
                return _carTypes;
            }

            return _carTypes.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<string> ValidateCarTypes(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                yield return "Поле обязательно к заполнению";
                yield break;
            }

            if (!_carTypes.Contains(value))
            {
                yield return "Введенное значение некорректно";
            }
        }

        public class TestedValues
        {
            [CarTypes]
            [Required(ErrorMessage = "Поле обязательно к заполнению")]
            [MaxLength(6, ErrorMessage = "Количество используемых символов не должно превышать 6")]
            public string CarType { get; set; }
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
        public sealed class CarTypesAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (!_carTypes.Contains(value))
                {
                    return new ValidationResult("Данного типа авто нет в списке допустимых типов", new[] { validationContext.MemberName });
                }

                return null;
            }
        }
    }
}