using FluentValidation;
using FluentValidation.Validators;
using Template.UI.Data.ValidationModels;

namespace Template.UI.Components.Validations.ValidationsExtensions
{
    /// <summary>
    /// A standard AbstractValidator which contains multiple rules and can be shared with the back end API
    /// </summary>
    /// <typeparam name="OrderModel"></typeparam>
    public class OrderModelFluentValidator : AbstractValidator<OrderModel>
    {
        public OrderModelFluentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Поле Имя не может быть пустым")
                .Length(1, 100)
                .WithMessage("Количество символов ввода должно быть в диапазоне от 1 до 100");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Поле Адрес почты не может быть пустым")
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value));

            RuleFor(x => x.CCNumber)
                .NotEmpty()
                .WithMessage("Поле Серийный номер не может быть пустым")
                .Length(1, 100)
                .WithMessage("Количество символов ввода должно быть в диапазоне от 1 до 100")
                .CreditCard()
                .WithMessage("Номер кредитной карты должен быть 4012 8888 8888 1881");

            RuleFor(x => x.Address.Address)
                .NotEmpty()
                .WithMessage("Поле Адрес не может быть пустым")
                .Length(1, 100)
                .WithMessage("Количество символов ввода должно быть в диапазоне от 1 до 100");

            RuleFor(x => x.Address.City)
                .NotEmpty()
                .WithMessage("Поле Город не может быть пустым")
                .Length(1, 100)
                .WithMessage("Количество символов ввода должно быть в диапазоне от 1 до 100");

            RuleFor(x => x.Address.Country)
                .NotEmpty()
                .WithMessage("Поле страна не может быть пустым")
                .Length(1, 100)
                .WithMessage("Количество символов ввода должно быть в диапазоне от 1 до 100");

            RuleForEach(x => x.OrderDetails)
                .SetValidator(new OrderDetailsModelFluentValidator());
        }

        private async Task<bool> IsUniqueAsync(string email)
        {
            // Simulates a long running http call
            await Task.Delay(2000);
            return email.ToLower() != "test@test.com";
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<OrderModel>.CreateWithOptions((OrderModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
