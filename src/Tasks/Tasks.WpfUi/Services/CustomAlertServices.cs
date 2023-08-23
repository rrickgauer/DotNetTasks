using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.Services;

public sealed class CustomAlertServices
{
    private readonly ISnackbarService _snackbarService;

    public CustomAlertServices(ISnackbarService snackbarService)
    {
        _snackbarService = snackbarService;
    }

    /// <summary>
    /// Show successful alert
    /// </summary>
    /// <param name="message"></param>
    public void Successful(string message)
    {
        _snackbarService.Show("Success", message, SymbolRegular.Checkmark32, ControlAppearance.Success);
    }

    /// <summary>
    /// Show an error message
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public void Error(string message, string title = "Error")
    {
        _snackbarService.Show(title, message, SymbolRegular.ErrorCircle24, ControlAppearance.Danger);
    }
}
