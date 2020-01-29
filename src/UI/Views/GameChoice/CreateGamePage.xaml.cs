using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;
using Core.Data;
using UI.Interfaces;
using UI.Services;
using UI.ViewModels.GameChoice;

namespace UI.Views.GameChoice
{
    /// <summary>
    /// Логика взаимодействия для CreateGamePage.xaml
    /// </summary>
    public partial class CreateGamePage : Page
    {
        private readonly IGameChoiceProvider _gameChoiceProvider;
        private readonly CreateGamePageViewModel viewModel;

        public CreateGamePage(IGameChoiceProvider gameChoiceProvider)
        {
            _gameChoiceProvider = gameChoiceProvider;

            viewModel = CreateGamePageViewModel.Create(gameChoiceProvider.Service);
            viewModel.Connected += OnConnected;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnConnected(Object sender, ConnectionInfo info)
        {
            NavigationService?.Navigate(new GamePlayersListPage(info, _gameChoiceProvider));
        }

        private void BackButton_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }

    public class ConnectionInfo
    {
        public CGameInfo Game { get; set; }
        public String Session { get; set; }
    }
}