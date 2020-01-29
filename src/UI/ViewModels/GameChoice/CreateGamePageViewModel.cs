using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core.Data;
using UI.GameServer;
using UI.Internal;
using UI.Services;
using UI.Views.GameChoice;

namespace UI.ViewModels.GameChoice
{
    public class CreateGamePageViewModel : ViewModelBase
    {
        private readonly GameChoiceServiceClient _gameService;
        private readonly MapServiceClient _mapService;
        private Int32 _maxPlayers;
        private String _name;

        private CreateGamePageViewModel(GameChoiceServiceClient gameService, MapServiceClient mapService)
        {
            _gameService = gameService;
            _mapService = mapService;
            Maps = new ObservableCollection<CMapInfo>();
            CreateGameCommand = new CRelayCommand(CreateGameExecute, CreateGameCanExecute);
        }

        private Boolean CreateGameCanExecute(Object arg)
        {
            if (String.IsNullOrWhiteSpace(Name)) return false;
            if (MaxPlayers < 1) return false;
            if (SelectedMap == null) return false;

            return true;
        }

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public Int32 MaxPlayers
        {
            get => _maxPlayers;
            set
            {
                _maxPlayers = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateGameCommand { get; }

        public ObservableCollection<CMapInfo> Maps { get; }

        public CMapInfo SelectedMap { get; set; }

        public event EventHandler<ConnectionInfo> Connected;

        public static CreateGamePageViewModel Create(GameChoiceServiceClient gameService)
        {
            CMapServiceProvider mapServiceProvider = CMapServiceProvider.Create();
            var viewModel = new CreateGamePageViewModel(gameService, mapServiceProvider.Service);
            Task.Run(viewModel.LoadDataAsync);
            return viewModel;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                CMapInfo[] maps = await _mapService.GetMapsAsync();
                foreach (CMapInfo mapInfo in maps)
                    Application.Current?.Dispatcher?.Invoke(() => { Maps.Add(mapInfo); });
            }
            catch (Exception e)
            {
                //TODO: Log this
                Debug.WriteLine(e);
            }
        }

        private void CreateGameExecute(Object obj)
        {
            CGameInfo game = _gameService.CreateGame(Name, MaxPlayers);
            if (game != null) Connected?.Invoke(this, new ConnectionInfo {Game = game});
        }
    }
}