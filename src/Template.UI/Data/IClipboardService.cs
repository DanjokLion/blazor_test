namespace Template.UI.Data;

public interface IClipboardService
{
    Task CopyToClipboard(string text);
}