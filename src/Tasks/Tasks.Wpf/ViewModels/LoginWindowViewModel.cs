using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.CustomAttributes;

namespace Tasks.Wpf.ViewModels;

public class LoginWindowViewModel : ViewModelBase
{

    [RaisePropertyChange]
    public string? EmailTextBox
    {
        get => emailTextBox;
        set
        {
            if (emailTextBox == value) return;
            emailTextBox = value;
            RaisePropertyChanges();
        }
    }
    private string? emailTextBox = "rrickgauer1@gmail.com";


    [RaisePropertyChange]
    public string? PasswordTextBox
    {
        get => passwordTextBox;
        set
        {
            if (passwordTextBox == value) return;
            passwordTextBox = value;
            RaisePropertyChanges();
        }
    }

    private string? passwordTextBox;
    

    [RaisePropertyChange]
    public bool CanSubmitForm
    {
        get
        {
            //if (string.IsNullOrEmpty(EmailTextBox))
            //{
            //    return false;
            //}
            //if (string.IsNullOrEmpty(PasswordTextBox))
            //{
            //    return false;
            //}

            return true;
        }
    }

}
