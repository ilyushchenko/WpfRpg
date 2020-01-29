using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Models;
using Core.Models.Heroes;
using Interfaces;
using UI.Controls;
using UI.Interfaces;
using UI.Models;
using UI.Navigation.Game;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для GameAreaPage.xaml
    /// </summary>
    public partial class GameArea : UserControl
    {
        private readonly IGameServiceProvider _provider;
        private readonly BattlefieldViewModel _viewModel;
        public GameArea()
        {
            InitializeComponent();
        }

        public GameArea(CMap map, CHeroBase hero, IGameServiceProvider provider) : this()
        {
            _provider = provider;
            _viewModel = new BattlefieldViewModel(map, hero, provider);
            DataContext = _viewModel;
            Map.Source = map;
            map.MapUpdated += OnMapUpdated;
        }

        private void OnMapUpdated(Object sender, EventArgs e)
        {
            if (sender is CMap map)
            {
                Map.Update();
            }
        }

        private void Map_OnCellClick(Object sender, RoutedEventArgs e)
        {
            if (sender is MapControl mapControl) _viewModel.SelectedCell = mapControl.SelectedCell;
        }

        private void TradeButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!(_viewModel.SelectedCell?.Unit is ITrader trader)) return;
            //var tradingPage = new TradingPage(_viewModel.MyHero, trader);
            //CGameNavigator.Instance?.NavigateTo(tradingPage);
        }
    }
}
