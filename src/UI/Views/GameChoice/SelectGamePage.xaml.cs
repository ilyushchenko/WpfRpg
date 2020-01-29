using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Interfaces;
using UI.Services;
using UI.ViewModels.GameChoice;

namespace UI.Views.GameChoice
{
    /// <summary>
    /// Логика взаимодействия для SelectGamePage.xaml
    /// </summary>
    public partial class SelectGamePage : Page
    {
        private readonly SelectGamePageViewModel viewModel;
        private IGameChoiceProvider _gameChoiceProvider;

        public SelectGamePage(IGameChoiceProvider gameChoiceProvider)
        {
            _gameChoiceProvider = gameChoiceProvider;
            _gameChoiceProvider.Callback.GameStarted += CallbackOnGameStarted;
            viewModel = SelectGamePageViewModel.Create(_gameChoiceProvider.Service);
            viewModel.Connected += OnConnected;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CallbackOnGameStarted(Object sender, String gameHostUrl)
        {
            NavigationService?.Navigate(new GamePage(gameHostUrl));
        }

        private void OnConnected(Object sender, ConnectionInfo connectionInfo)
        {
            NavigationService?.Navigate(new GamePlayersListPage(connectionInfo, _gameChoiceProvider));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}