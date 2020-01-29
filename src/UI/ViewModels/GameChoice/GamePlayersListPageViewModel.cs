using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.Data;
using UI.Interfaces;
using UI.Internal;

namespace UI.ViewModels.GameChoice
{
    public class GamePlayersListPageViewModel : ViewModelBase
    {
        private readonly CGameInfo _game;
        private readonly IGameChoiceProvider _gameProvider;

        private GamePlayersListPageViewModel(IGameChoiceProvider gameProvider, CGameInfo game)
        {
            _gameProvider = gameProvider;
            _game = game;
            _gameProvider.Callback.PlayerConnected += OnPlayerConnected;
            _gameProvider.Callback.PlayerDisconnected += OnPlayerDisconnected;
            Players = new ObservableCollection<CPlayer>(_game.Players);
            LeaveCommand = new CRelayCommand(LeaveExecute);
            gameProvider.Service.Ready(_game.Id);
        }

        public String Name => _game.Name;
        public ObservableCollection<CPlayer> Players { get; }
        public ICommand LeaveCommand { get; }

        public Int32 ConnectedPlayersCount => Players.Count;
        public Int32 MaxPlayersCount => _game.MaxPlayers;

        public static GamePlayersListPageViewModel Create(IGameChoiceProvider gameProvider, CGameInfo game)
        {
            return new GamePlayersListPageViewModel(gameProvider, game);
        }

        private void OnPlayerDisconnected(Object sender, CPlayer player)
        {
            _game.Players.Remove(player);
            Players.Remove(player);
            OnPropertyChanged(nameof(ConnectedPlayersCount));
        }

        private void OnPlayerConnected(Object sender, CPlayer player)
        {
            _game.Players.Add(player);
            Players.Add(player);
            OnPropertyChanged(nameof(ConnectedPlayersCount));
        }

        private void LeaveExecute(Object obj)
        {
            _gameProvider.Service.Disconnect(_game.Id);
            LeaveGame?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler LeaveGame;
    }
}