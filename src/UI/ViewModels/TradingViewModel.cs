using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.Models.Heroes;
using Core.Models.Units;
using Interfaces;
using UI.Annotations;
using UI.Internal;
using UI.Models.Heroes;
using UI.Navigation.Game;

namespace UI.ViewModels
{
    internal class TradingViewModel : ViewModelBase
    {
        private ITradable _hero;
        private ITrader _trader;
        private Int32 _traderGold;
        private Int32 _heroGold;

        public TradingViewModel()
        {
            MoveToBuyCommand = new CRelayCommand(BuyItem);
            MoveToSellCommand = new CRelayCommand(SellItem);
            BackCommand = new CRelayCommand(BackExecute);
        }

        private void BackExecute(Object obj)
        {
            CGameNavigator.Instance.NavigateTo(EAreaType.Main);
        }

        public Int32 HeroGolds
        {
            get => _heroGold;
            set
            {
                _heroGold = value;
                OnPropertyChanged();
            }
        }

        public Int32 TraderGolds
        {
            get => _traderGold;
            set
            {
                _traderGold = value;
                OnPropertyChanged();
            }
        }

        public ICommand MoveToSellCommand { get; }
        public ICommand MoveToBuyCommand { get; }

        public ObservableCollection<IInventoryItem> HeroItems { get; private set; }
        public ObservableCollection<IInventoryItem> TraderItems { get; private set; }

        public ICommand BackCommand { get; }

        public override void OnNavigated(Object navigationData)
        {
            if (navigationData is CTradingNavigationData data)
            {
                _hero = data.Hero;
                _heroGold = _hero.Gold;
                _trader = data.Trader;
                _traderGold = _trader.Gold;
                HeroItems = new ObservableCollection<IInventoryItem>(_hero.Inventory);
                TraderItems = new ObservableCollection<IInventoryItem>(_trader.Inventory);
                Update();
            }
        }

        private void SellItem(Object obj)
        {
            if (!(obj is IInventoryItem item)) return;

            Boolean tradeResult = _trader.BuyItem(_hero, item);
            if (tradeResult) Update();
        }

        private void BuyItem(Object obj)
        {
            if (!(obj is IInventoryItem item)) return;
            Boolean tradeResult = _trader.SellItem(_hero, item);
            if (tradeResult) Update();
        }

        private void Update()
        {
            _heroGold = _hero.Gold;
            _traderGold = _trader.Gold;
            TraderItems.Clear();
            HeroItems.Clear();
            foreach (IInventoryItem item in _trader.Inventory)
            {
                TraderItems.Add(item);
            }
            foreach (IInventoryItem item in _hero.Inventory)
            {
                HeroItems.Add(item);
            }
            OnPropertyChanged(nameof(TraderItems));
            OnPropertyChanged(nameof(HeroItems));
            OnPropertyChanged(nameof(TraderGolds));
            OnPropertyChanged(nameof(HeroGolds));
        }
    }

    public class CTradingNavigationData
    {
        public CTradingNavigationData([NotNull] CHeroBase hero, [NotNull] CTrader trader)
        {
            Hero = hero ?? throw new ArgumentNullException(nameof(hero));
            Trader = trader ?? throw new ArgumentNullException(nameof(trader));
        }

        public CHeroBase Hero { get; set; }
        public CTrader Trader { get; set; }
    }
}