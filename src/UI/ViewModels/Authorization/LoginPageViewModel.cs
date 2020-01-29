using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using BusinessLayer.Client.Providers;
using Core.Data;
using LoginService.Contracts;
using UI.Internal;
using UI.Navigation;
using UI.Services;

namespace UI.ViewModels.Authorization
{
    public class LoginPageViewModel : ViewModelBase
    {
        private String _login;
        private String _password;
        private String _errorMessage;

        public LoginPageViewModel()
        {
            LoginCommand = new CRelayCommand(LoginExecute);
        }

        private async void LoginExecute(Object obj)
        {
            ErrorMessage = String.Empty;
            CAuthToken authToken = null;
            try
            {
                var authorizationProvider = new CAuthorizationProvider();
                var result = await Task.Run(() => authorizationProvider.TryAuthorize(Login, Password, out authToken));
                if (!result)
                {
                    ErrorMessage = "Login or Password incorrect";
                    return;
                }

            }
            catch (Exception e)
            {
                ErrorMessage = "Authorization on Main service failed";
                return;
            }

            try
            {
                Boolean loginResult =
                    GameChoiceProvider.Instance.Service.LogIn(authToken.Login, authToken.Id, out CPlayer player);
                CAuthController.Instance.SetUser(player);
            }
            catch (Exception exception)
            {
                ErrorMessage = "Authorization on Game server failed";
                return;
            }
            CNavigator.Instance.NavigateTo(EPageType.Main);
        }

        public String Login
        {
            get => _login;
            set

            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public String Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public String ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasError));
            }
        }

        public Object HasError => !String.IsNullOrWhiteSpace(ErrorMessage);
    }
}