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
using System.Windows.Shapes;
using UI.Models.Heroes;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для HeroSelectionView.xaml
    /// </summary>
    public partial class HeroSelectionView : UserControl
    {
        private readonly HeroSelectionViewModel _viewModel;
        public HeroSelectionViewModel ViewModel => _viewModel;
        public HeroSelectionView()
        {
            _viewModel = HeroSelectionViewModel.Create();
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.Load();
        }
    }
}
