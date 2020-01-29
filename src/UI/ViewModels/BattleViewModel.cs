using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Core.Models.Heroes;
using Core.Models.Units;
using Interfaces;
using UI.Annotations;
using UI.Factories;
using UI.Interfaces;
using UI.Internal;
using UI.Navigation.Game;

namespace UI.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        public BattleViewModel()
        {
            AttackCommand = new CRelayCommand(AttackExecute);
            BackCommand = new CRelayCommand(BackExecute);
        }

        private void BackExecute(Object obj)
        {
            CGameNavigator.Instance.NavigateTo(EAreaType.Main);
        }

        private void AttackExecute(Object obj)
        {
            if (SelectedWeapon == null)
            {
                return;
            }

            Opponent.Attack(SelectedWeapon);

            if (Opponent.HealthPoints <= 0)
            {
                Provider.GameClient.KillUnit(OpponentPosition);
                CGameNavigator.Instance.NavigateTo(EAreaType.Main);
            }

            Update();
        }

        public override void OnNavigated(Object data)
        {
            if (!(data is CBattleNavigationData navigationData))
            {
                return;
            }

            MyHero = navigationData.MyHero;
            Opponent = navigationData.Opponent;
            Provider = navigationData.Provider;
            OpponentPosition = navigationData.Position;
            HeroImage = CUnitTextureFactory.GetTexture(MyHero);
            OpponentImage = CUnitTextureFactory.GetTexture(Opponent as IPositionable);
            Weapons = new ObservableCollection<IDamaging>(MyHero.Inventory.OfType<IDamaging>());
            HeroViewModel = new HeroViewModel(MyHero);
            Update();
        }

        public SPoint OpponentPosition { get; set; }

        private void Update()
        {
            OnPropertyChanged(nameof(Opponent));
            OnPropertyChanged(nameof(MyHero));
            OnPropertyChanged(nameof(HeroImage));
            OnPropertyChanged(nameof(OpponentImage));
            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(HeroViewModel));
        }

        public IGameServiceProvider Provider { get; set; }

        public IWeapon SelectedWeapon { get; set; }
        public CHeroBase MyHero { get; private set; }
        public IFightingUnit Opponent { get; private set; }

        public ICommand AttackCommand { get; }

        public ImageSource HeroImage { get; private set; }
        public ImageSource OpponentImage { get; private set; }

        public ObservableCollection<IDamaging> Weapons { get; private set; }

        public ICommand BackCommand { get; }

        public HeroViewModel HeroViewModel { get; private set; }
    }

    public class CBattleNavigationData
    {
        public CBattleNavigationData([NotNull] CHeroBase hero, [NotNull] IFightingUnit opponent,
            [NotNull] IGameServiceProvider provider, SPoint position)
        {
            MyHero = hero ?? throw new ArgumentNullException(nameof(hero));
            Opponent = opponent ?? throw new ArgumentNullException(nameof(opponent));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Position = position;
        }

        public CHeroBase MyHero { get; set; }
        public IFightingUnit Opponent { get; set; }

        public IGameServiceProvider Provider { get; set; }
        public SPoint Position { get; }
    }
}
