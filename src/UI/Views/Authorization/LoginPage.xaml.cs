using System;
using System.Windows;
using System.Windows.Controls;
using Core.Data;
using UI.Services;
using UI.ViewModels.Authorization;

namespace UI.Views.Authorization
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel();
        }

        private void PasswordBox_OnPasswordChanged(Object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}