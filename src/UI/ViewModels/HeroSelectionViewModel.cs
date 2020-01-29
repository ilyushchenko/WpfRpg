using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLayer.Client.Providers;
using Core.Data;
using Core.Models.Heroes;
using UI.Internal;

namespace UI.ViewModels
{
    public class HeroSelectionViewModel : ViewModelBase
    {
        private readonly CHeroesProvider _heroesProvider;
        private CHeroData _selectedHero;

        private HeroSelectionViewModel(CHeroesProvider heroesProvider)
        {
            _heroesProvider = heroesProvider;
            Heroes = new ObservableCollection<CHeroData>();
            PlayCommand = new CRelayCommand(SelectCommandExecute, CheckHeroSelected);
        }

        public CHeroData SelectedHero
        {
            get => _selectedHero;
            set
            {
                _selectedHero = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayCommand { get; }

        public ObservableCollection<CHeroData> Heroes { get; }

        public static HeroSelectionViewModel Create()
        {
            CHeroesProvider provider = CHeroesProvider.Create();
            var viewModel = new HeroSelectionViewModel(provider);
            return viewModel;
        }

        public void Load()
        {
            IEnumerable<CHeroData> heroes = _heroesProvider.GetHeroes();
            foreach (CHeroData heroData in heroes)
            {
                Heroes.Add(heroData);
            }
        }

        public event EventHandler<CHeroBase> OnHeroSelected;

        private Boolean CheckHeroSelected(Object parameter)
        {
            return SelectedHero != null;
        }

        private void SelectCommandExecute(Object parameter)
        {
            var hero = _heroesProvider.GetHero(SelectedHero.Type);
            OnHeroSelected?.Invoke(this, hero);
        }
    }
}