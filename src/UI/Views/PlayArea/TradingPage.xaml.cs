using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Views.PlayArea
{
    /// <summary>
    /// Логика взаимодействия для TradingPage.xaml
    /// </summary>
    public partial class TradingPage : UserControl
    {
        public TradingPage()
        {
            InitializeComponent();
            DataContext = new TradingViewModel();
        }
    }
}
