using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Data;
using Core.Models;
using Core.Models.Heroes;
using GameServer.Contracts;
using Interfaces;
using Interfaces.Enums;
using UI.Interfaces;
using UI.Internal;
using UI.Navigation.Game;
using UI.Services;
using UI.Views;
using UI.Views.PlayArea;

namespace UI.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private readonly IGameServiceProvider _gameServiceProvider;
        private readonly CGameNavigator _navigator;
        private String _session;
        private readonly Timer _countdownTimer;
        private readonly IGameService _gameServiceClient;
        private TimeSpan _countdown;
        private Int32 _round;
        private CHeroBase _hero;
        private CMap _map;

        private GameViewModel(IGameServiceProvider gameServiceProvider, CGameNavigator navigator)
        {
            _gameServiceProvider = gameServiceProvider;
            _navigator = navigator;
            СGameServiceCallback serviceCallback = gameServiceProvider.ServiceCallback;
            serviceCallback.GameStarted += OnGameStarted;
            serviceCallback.RoundStarted += OnRoundStarted;
            serviceCallback.RoundEnded += OnRoundEnded;

            _gameServiceClient = gameServiceProvider.GameClient;

            FinishRoundCommand = new CRelayCommand(FinishRoundExecuted);
            _countdownTimer = new Timer();
            _countdownTimer.Elapsed += OnCountdown;
            _countdownTimer.Interval = 1000;
            _countdownTimer.AutoReset = true;


            var heroSelectionView = new HeroSelectionView();
            CGameNavigator.Instance.Register(heroSelectionView, EAreaType.HeroSelection);
            heroSelectionView.ViewModel.OnHeroSelected += OnHeroSelected;


            CGameNavigator.Instance.Register(new EndRoundControl(), EAreaType.RoundEnded);

            CGameNavigator.Instance.Register(new TradingPage(), EAreaType.Trading);

            CGameNavigator.Instance.Register(new BattleView(), EAreaType.Battle);
        }

        public TimeSpan Countdown
        {
            get => _countdown;
            set
            {
                _countdown = value;
                OnPropertyChanged();
            }
        }

        public Int32 Round
        {
            get => _round;
            set
            {
                _round = value;
                OnPropertyChanged();
            }
        }

        public ICommand FinishRoundCommand { get; }

        public static GameViewModel Create(IGameServiceProvider gameProvider)
        {
            return new GameViewModel(gameProvider, CGameNavigator.Instance);
        }

        private void OnGameStarted(Object sender, (CMap Map, CHeroBase Hero) game)
        {
            _map = game.Map;
            _hero = game.Hero;
            ICell cell = _map.GetCell(_hero.Position);
            var hero = cell.Unit as CHeroBase;
            var gameAreaPage = new GameArea(_map, hero, _gameServiceProvider);

            CGameNavigator.Instance.Register(gameAreaPage, EAreaType.Main);
            _navigator.NavigateTo(EAreaType.Main);
        }

        private void OnRoundStarted(Object sender, (Int32 Round, TimeSpan RoundTime) e)
        {
            Round = e.Round;
            StartRound(e.RoundTime);
            _navigator.NavigateTo(EAreaType.Main);
        }

        private void OnRoundEnded(Object sender, TimeSpan e)
        {
            _countdownTimer.Stop();
            _navigator.NavigateTo(EAreaType.RoundEnded);
        }

        private void OnCountdown(Object sender, ElapsedEventArgs e)
        {
            TimeSpan newCountdown = Countdown.Add(TimeSpan.FromSeconds(-1));
            Countdown = newCountdown;
            if (Countdown <= TimeSpan.Zero) _countdownTimer.Stop();
        }

        private void FinishRoundExecuted(Object obj)
        {
            _gameServiceClient.FinishRound(_session);
        }

        private void ResetTimer(TimeSpan roundTime)
        {
            Countdown = roundTime;
            _countdownTimer.Start();
        }

        #region Game state actions

        public void StartRound(TimeSpan roundTime)
        {
            OnPropertyChanged(nameof(Round));
            ResetTimer(roundTime);
        }

        #endregion

        public void SelectHero(CHeroBase hero)
        {
            _gameServiceClient.SelectHero(hero);
        }

        public void Connect()
        {
            _gameServiceClient.Connect(CAuthController.Instance.PlayerId);
            CGameNavigator.Instance.NavigateTo(EAreaType.HeroSelection);
        }

        private void OnHeroSelected(Object sender, CHeroBase hero)
        {
            SelectHero(hero);
        }
    }
}