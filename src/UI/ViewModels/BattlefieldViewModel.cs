using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using Core.Models;
using Core.Models.Heroes;
using Core.Models.Items;
using Core.Models.Units;
using Interfaces;
using UI.Factories;
using UI.Interfaces;
using UI.Internal;
using UI.Models;
using UI.Navigation.Game;

namespace UI.ViewModels
{
    internal class BattlefieldViewModel : ViewModelBase
    {
        private readonly IGameServiceProvider _provider;
        private ICell _selectedCell;
        private CHeroBase _myHero;

        public BattlefieldViewModel(CMap map, CHeroBase hero, IGameServiceProvider provider)
        {
            _provider = provider;
            MyHero = hero;
            Map = map;
            MoveCommand = new CRelayCommand(MoveCommandExecuted);
            TradeCommand = new CRelayCommand(TradeCommandExecuted, TradeCanExecute);
            BattleCommand = new CRelayCommand(BattleExecute, BattleCanExecute);
            UseItemCommand = new CRelayCommand(UseItemExecute, UseItemCanExecute);

            provider.ServiceCallback.HeroMoved += OnHeroMoved;
            provider.ServiceCallback.UnitDied += OnUnitDied;
            MyHeroViewModel = new HeroViewModel(hero);
        }

        private void OnRoundStarted(Object sender, TimeSpan e)
        {
            _myHero.RestoreAfterRound();
        }

        private void OnUnitDied(Object sender, SPoint position)
        {
            Map.RemoveUnit(position);
        }

        public HeroViewModel MyHeroViewModel { get; set; }

        private Boolean UseItemCanExecute(Object arg)
        {
            return arg is IUsableItem usableItem;
        }

        private void UseItemExecute(Object obj)
        {
            if (obj is IUsableItem usableItem)
            {
                MyHero.UseItem(usableItem);
                if (usableItem is CSingleUseItem)
                {
                    MyHero.Inventory.Remove(usableItem);
                }

                OnPropertyChanged(nameof(MyHero));
                MyHeroViewModel.Update();
            }
        }

        private Boolean CheckBattleCanExecute()
        {
            IPositionable unit = SelectedCell?.Unit;
            if (unit == null)
            {
                return false;
            }

            if (!(unit is IFightingUnit))
            {
                return false;
            }

            if (unit == MyHero)
            {
                return false;
            }

            return true;
        }

        private Boolean BattleCanExecute(Object arg)
        {
            return CheckBattleCanExecute();
        }

        private void BattleExecute(Object obj)
        {
            if (!CheckBattleCanExecute())
            {
                return;
            }

            var unit = SelectedCell.Unit as IFightingUnit;
            if (unit == null)
            {
                return;
            }

            var navData = new CBattleNavigationData(MyHero, unit, _provider, SelectedCell.Location);
            CGameNavigator.Instance.NavigateTo(EAreaType.Battle, navData);
        }

        private void OnHeroMoved(Object sender, (CHeroBase Hero, SPoint OldPosition, SPoint NewPosition) moveData)
        {
            Map.RemoveUnit(moveData.OldPosition);
            Map.SetUnit(moveData.Hero, moveData.NewPosition);
        }

        public ICell SelectedCell
        {
            get => _selectedCell;
            set
            {
                _selectedCell = value;
                OnPropertyChanged();
            }
        }

        private void TradeCommandExecuted(Object parameter)
        {
            if (SelectedCell?.Unit is CTrader trader)
            {
                var navigationData = new CTradingNavigationData(MyHero, trader);
                CGameNavigator.Instance.NavigateTo(EAreaType.Trading, navigationData);
            }
        }

        private Boolean TradeCanExecute(Object arg)
        {
            return SelectedCell?.Unit is CTrader;
        }

        public ICommand MoveCommand { get; }

        public ICommand TradeCommand { get; }

        public ICommand BattleCommand { get; }

        public ICommand UseItemCommand { get; }


        private void MoveCommandExecuted(Object parameter)
        {
            if (!(parameter is EMoveDirections direction)) return;

            SPoint newPosition = GetNewPosition(direction);

            SPoint oldPosition = MyHero.Position;

            Boolean result = _provider.GameClient.TryMoveUnit(MyHero.Position, newPosition);

            if (result)
            {
                Map.Move(MyHero, oldPosition, newPosition);
                OnPropertyChanged(nameof(Map));
            }
        }

        public CMap Map { get; }

        public CHeroBase MyHero
        {
            get => _myHero;
            private set
            {
                _myHero = value;
                OnPropertyChanged();
            }
        }

        private SPoint GetNewPosition(EMoveDirections direction)
        {
            SPoint position = MyHero.Position;
            switch (direction)
            {
                case EMoveDirections.Up:
                    return new SPoint(position.X, position.Y - 1);
                case EMoveDirections.Down:
                    return new SPoint(position.X, position.Y + 1);
                case EMoveDirections.Left:
                    return new SPoint(position.X - 1, position.Y);
                case EMoveDirections.Right:
                    return new SPoint(position.X + 1, position.Y);
                default:
                    return new SPoint(position.X, position.Y);
            }
        }
    }

    public class HeroViewModel : ViewModelBase
    {
        private readonly CHeroBase _hero;

        public HeroViewModel(CHeroBase hero)
        {
            _hero = hero;
            _hero.Updated += HeroOnUpdated;
            Image = CUnitTextureFactory.GetTexture(hero);
            Inventory = new ObservableCollection<IInventoryItem>(_hero.Inventory);
        }

        private void HeroOnUpdated(Object sender, EventArgs e)
        {
            Update();
        }

        public void Update()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(HealthPoints));
            OnPropertyChanged(nameof(MaxHealthPoints));
            OnPropertyChanged(nameof(Level));
            OnPropertyChanged(nameof(Xp));
            OnPropertyChanged(nameof(NextLevelXp));
            OnPropertyChanged(nameof(Inventory));
            OnPropertyChanged(nameof(Gold));
            OnPropertyChanged(nameof(MaxWeight));
            OnPropertyChanged(nameof(MovingEnergy));
            OnPropertyChanged(nameof(MaxMovingEnergy));
            Inventory.Clear();
            foreach (IInventoryItem item in _hero.Inventory)
            {
                Inventory.Add(item);
            }
        }

        public ImageSource Image { get; }
        public String Name => _hero.Name;
        public Int32 HealthPoints => _hero.HealthPoints;
        public Int32 MaxHealthPoints => _hero.MaxHealthPoints;
        public Int32 Level => _hero.Level;
        public Int32 Xp => _hero.Xp;
        public Int32 NextLevelXp => _hero.NextLevelXp;
        public ObservableCollection<IInventoryItem> Inventory { get; }
        public Int32 Gold => _hero.Gold;
        public Double MaxWeight => _hero.MaxWeight;
        public Int32 MovingEnergy => _hero.MovingEnergy;
        public Int32 MaxMovingEnergy => _hero.MaxMovingEnergy;
    }
}