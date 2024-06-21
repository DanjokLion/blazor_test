using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Template.UI.Components.Validations.DataAnnotations
{
    public partial class EditFormValidation : ComponentBase
    {
        RegisterAccountForm model = new RegisterAccountForm();
        bool success;

        public class RegisterAccountForm
        {
            [Required(ErrorMessage = "Введите имя")]
            [StringLength(8, ErrorMessage = "Имя должно содержать не более 8 символов")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Введите адрес эл.почты")]
            [EmailAddress(ErrorMessage = "Адрес эл.почты некорректный")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Введите пароль")]
            [StringLength(30, ErrorMessage = "Пароль должен быть не менее 8 символов", MinimumLength = 8)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Введите пароль для проверки")]
            [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
            public string RepeatPassword { get; set; }
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
        }
    }
}