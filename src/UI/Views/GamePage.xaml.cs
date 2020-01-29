using System;
using System.Windows.Controls;
using UI.Navigation.Game;
using UI.Services;
using UI.ViewModels;
using UI.Views.PlayArea;

namespace UI.Views
{
    public partial class GamePage : Page
    {
        public GamePage(String url)
        {
            CGameServiceProvider gameProvider = CGameServiceProvider.Create(url);
            InitializeComponent();

            CGameNavigator.Instance.Navigate += OnNavigate;

            GameViewModel gameViewModel = GameViewModel.Create(gameProvider);
            DataContext = gameViewModel;
            gameViewModel.Connect();
        }

        private void OnNavigate(Object sender, ContentControl content)
        {
            GameFrame.Content = content;
        }
    }
}