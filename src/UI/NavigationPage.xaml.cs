﻿using System;
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
using UI.Navigation;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для NavigationPage.xaml
    /// </summary>
    public partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            InitializeComponent();
            CNavigator.Instance.PageNavigate += Page_OnNavigate;
            CNavigator.Instance.NavigateTo(EPageType.SignIn);
        }
        private void Page_OnNavigate(Object sender, Page page)
        {
            PagesFrame.Content = page;
        }
    }
}
