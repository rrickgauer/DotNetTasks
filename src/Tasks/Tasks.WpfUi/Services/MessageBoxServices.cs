using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tasks.WpfUi.Services;

public static class MessageBoxServices
{
    public static void ShowException(Exception ex)
    {
        ShowMessage(ex.Message);
    }

    public static void ShowMessage(object message)
    {
        var displayMessage = message.ToString();

        MessageBox.Show(displayMessage);
    }

    public static bool Confirm(string message, string caption)
    {
        var result = MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            return true;
        }

        return false;
    }
}
