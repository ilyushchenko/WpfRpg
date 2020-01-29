using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.ViewModels;
using UI.ViewModels.Authorization;
using UI.Views;
using UI.Views.Authorization;

namespace UI.Navigation
{
    public class CPagesFactory
    {
        public static Page GetPage(EPageType pageType)
        {
            Page page = CreatePage(pageType);
            page.DataContext = CreatePageViewModel(pageType);
            return page;
        }

        private static Page CreatePage(EPageType pageType)
        {
            switch (pageType)
            {
                case EPageType.SignIn:
                    return new LoginPage();
                case EPageType.Main:
                    return new MainPage();
                default:
                    return new Page();
            }
        }

        private static ViewModelBase CreatePageViewModel(EPageType pageType)
        {
            switch (pageType)
            {
                case EPageType.SignIn:
                    return new LoginPageViewModel();
                default:
                    return new ViewModelBase();
            }
        }
    }
}
