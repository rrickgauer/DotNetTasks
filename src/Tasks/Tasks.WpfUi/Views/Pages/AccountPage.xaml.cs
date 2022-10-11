﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tasks.WpfUi.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for AccountPage.xaml
/// </summary>
public partial class AccountPage : INavigableView<AccountPageViewModel>
{
    public AccountPageViewModel ViewModel { get; set; }

    public AccountPage(AccountPageViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
