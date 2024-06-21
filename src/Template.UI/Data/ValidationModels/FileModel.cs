using Microsoft.AspNetCore.Components.Forms;

namespace Template.UI.Data.ValidationModels
{
    public class FileModel
    {
        public string Name { get; set; }
        public IBrowserFile File { get; set; }
    }
}
