using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Template.UI.Components.Validations.ValidationsExtensions;
using Template.UI.Data.ValidationModels;

namespace Template.UI.Components.Validations.FluentValidation
{
    public partial class UploadFileValidation : ComponentBase
    {
        private MudForm form;
        private FileModel model = new();
        private FileModelFluentValidator ValidationRules = new();
        private bool SuppressOnChangeWhenInvalid;

        private void UploadFiles(InputFileChangeEventArgs e)
        {
            //If SuppressOnChangeWhenInvalid is false, perform your validations here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add($"This file has the extension {model.File.Name.Split(".").Last()}", Severity.Info);

            //TODO upload the files to the server
        }

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                Snackbar.Add("Submited!");
            }
        }
    }
}