using FluentValidation;
using Template.UI.Data.ValidationModels;

namespace Template.UI.Components.Validations.ValidationsExtensions
{
    /// <summary>
    /// A standard AbstractValidator which contains multiple rules and can be shared with the back end API
    /// </summary>
    /// <typeparam name="FileModel"></typeparam>
    public class FileModelFluentValidator : AbstractValidator<FileModel>
    {
        public FileModelFluentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);
            RuleFor(x => x.File)
            .NotEmpty();
            When(x => x.File != null, () =>
            {
                RuleFor(x => x.File.Size).LessThanOrEqualTo(10485760).WithMessage("The maximum file size is 10 MB");
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<FileModel>.CreateWithOptions((FileModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
