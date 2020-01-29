using System;
using System.Windows.Controls;
using UI.Interfaces;
using UI.ViewModels.GameChoice;

namespace UI.Views.GameChoice
{
    /// <summary>
    ///     Логика взаимодействия для GamePlayersListPage.xaml
    /// </summary>
    public partial class GamePlayersListPage : Page
    {
        private readonly GamePlayersListPageViewModel viewModel;

        public GamePlayersListPage(ConnectionInfo connectionInfo, IGameChoiceProvider gameChoiceProvider)
        {
            viewModel = GamePlayersListPageViewModel.Create(gameChoiceProvider, connectionInfo.Game);
            gameChoiceProvider.Callback.GameStarted += GameStarted;
            viewModel.LeaveGame += OnLeaveGame;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void GameStarted(Object sender, String gameServerUrl)
        {
            NavigationService?.Navigate(new GamePage(gameServerUrl));
        }

        private void OnLeaveGame(Object sender, EventArgs e)
        {
            NavigationService?.Navigate(new MainPage());
        }
    }
}