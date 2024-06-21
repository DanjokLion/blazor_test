using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.RegularExpressions;

namespace Template.UI.Components.Validations.DataAnnotations
{
    public partial class SimpleFormValidation : ComponentBase
    {
        private bool _success;
        private string[] _errors = { };
        private MudTextField<string> _passwordField;
        private MudForm _form;

        private IEnumerable<string> PasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                yield return "Заполните пароль";
                yield break;
            }
            if (password.Length < 8)
                yield return "Пароль должен быть не короче 8 символов";
            if (!Regex.IsMatch(password, @"[A-Z]"))
                yield return "Пароль должен содержать хотя бы одну заглавную букву";
            if (!Regex.IsMatch(password, @"[a-z]"))
                yield return "Пароль должен сожержать хотя бы одну строчную букву";
            if (!Regex.IsMatch(password, @"[0-9]"))
                yield return "Пароль должен содержать хотя бы одну цифру";
        }

        private string PasswordMatch(string repeatPassword)
        {
            if (_passwordField.Value != repeatPassword)
                return "Пароли не совпадают";
            return null;
        }
    }
}