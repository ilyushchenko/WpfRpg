using System;
using System.Windows;
using System.Windows.Controls;
using UI.Interfaces;
using UI.Services;
using UI.Views.GameChoice;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly IGameChoiceProvider _gameChoiceProvider;
        public MainPage()
        {
            InitializeComponent();
            _gameChoiceProvider = GameChoiceProvider.Instance;
        }

        private void CreateGameButton_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CreateGamePage(_gameChoiceProvider));
        }

        private void FindGameButton_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SelectGamePage(_gameChoiceProvider));
        }
    }
}