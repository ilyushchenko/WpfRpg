using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Data;
using UI.GameServer;
using UI.Internal;
using UI.Views.GameChoice;

namespace UI.ViewModels.GameChoice
{
    public class SelectGamePageViewModel : ViewModelBase
    {
        private readonly GameChoiceServiceClient _gameService;
        private Boolean _isLoading;
        private CGameInfo _selectedGame;

        private SelectGamePageViewModel(GameChoiceServiceClient gameService)
        {
            _gameService = gameService;
            Games = new ObservableCollection<CGameInfo>();
            ConnectCommand = new CRelayCommand(ConnectExecute, ConnectCanExecute);
            RefreshCommand = new CRelayCommand(RefreshExecute);
        }

        public ObservableCollection<CGameInfo> Games { get; }

        public Boolean IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public CGameInfo SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConnectCommand { get; }
        public ICommand RefreshCommand { get; }

        public static SelectGamePageViewModel Create(GameChoiceServiceClient gameService)
        {
            var viewModel = new SelectGamePageViewModel(gameService);
            Task.Run(() => viewModel.LoadAsync());
            return viewModel;
        }

        public event EventHandler<ConnectionInfo> Connected;

        public async Task LoadAsync()
        {
            IsLoading = true;
            try
            {
                Task<CGameInfo[]> games = _gameService.GetGamesAsync();
                Games.Clear();
                foreach (CGameInfo game in await games) Games.Add(game);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void RefreshExecute(Object obj)
        {
            await LoadAsync();
        }

        private Boolean ConnectCanExecute(Object arg)
        {
            return SelectedGame != null;
        }

        private void ConnectExecute(Object obj)
        {
            if (SelectedGame == null) return;

            Boolean gameConnectionResult =
                _gameService.TryConnect(SelectedGame.Id, out CGameInfo game);
            if (gameConnectionResult)
                Connected?.Invoke(this, new ConnectionInfo
                {
                    Game = game
                });
        }
    }
}