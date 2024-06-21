using FluentValidation;
using Template.UI.Data.ValidationModels;

namespace Template.UI.Components.Validations.ValidationsExtensions
{
    /// <summary>
    /// A standard AbstractValidator for the Collection Object
    /// </summary>
    /// <typeparam name="OrderDetailsModel"></typeparam>
    public class OrderDetailsModelFluentValidator : AbstractValidator<OrderDetailsModel>
    {
        public OrderDetailsModelFluentValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Поле Описание не должно быть пустым")
                .Length(1, 100)
                .WithMessage("Поле Описание не должно быть от 1 до 100 символов");

            RuleFor(x => x.Offer)
                .GreaterThan(0)
                .WithMessage("Поле Номер не может быть меньше 1")
                .LessThan(999)
                .WithMessage("Поле Номер не должно в дипазоне от 1 до 999 символов");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<OrderDetailsModel>.CreateWithOptions((OrderDetailsModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
