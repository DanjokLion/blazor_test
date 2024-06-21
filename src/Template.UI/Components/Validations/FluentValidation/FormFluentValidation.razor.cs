using Microsoft.AspNetCore.Components;
using MudBlazor;
using Template.UI.Components.Validations.ValidationsExtensions;
using Template.UI.Data.ValidationModels;

namespace Template.UI.Components.Validations.FluentValidation
{
    public partial class FormFluentValidation : ComponentBase
    {
        [Inject] ISnackbar Snackbar { get; set; }

        MudForm form;

        OrderModelFluentValidator orderValidator = new OrderModelFluentValidator();

        OrderDetailsModelFluentValidator orderDetailsValidator = new OrderDetailsModelFluentValidator();

        OrderModel model = new OrderModel();

        public IMask cityMask = new RegexMask(@"^[a-zA-Zа-яёА-ЯЁ\s]+$");

        public IMask countryMask = new RegexMask(@"^[a-zA-Zа-яёА-ЯЁ\s]+$");

        public IMask mask = new BlockMask(delimiters: " ", new Block('0', 1, 4), new Block('0', 1, 4),
            new Block('0', 1, 4), new Block('0', 1, 4));
        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                Snackbar.Add("Регистрация прошла успешно");
            }
        }
    }
}