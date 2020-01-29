using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Navigation
{
    public class CNavigator
    {
        public static CNavigator Instance => _instance.Value;
        private static readonly Lazy<CNavigator> _instance = new Lazy<CNavigator>(() => new CNavigator());

        private Stack<CNavigationHistoryItem> _navigationItems;

        public event EventHandler<Page> PageNavigate;
        public event EventHandler<NavigateEventArgs> SigningPageNavigate;

        private CNavigator()
        {
            _navigationItems = new Stack<CNavigationHistoryItem>();
        }

        public void NavigateTo(EPageType pageType, Object data = null)
        {
            Page page = CPagesStore.Instance.GetPageByType(pageType);
            ViewModelInitialize(page, data);
            _navigationItems.Push(new CNavigationHistoryItem(pageType, data));
            PageNavigate?.Invoke(this, page);
        }

        private void ViewModelInitialize(Page page, Object data)
        {
            if (page?.DataContext is ViewModelBase viewModel)
            {
                viewModel.OnNavigated(data);
            }
        }

        public void GoBack()
        {
            if (_navigationItems.Count > 1)
            {
                _navigationItems.Pop();
                CNavigationHistoryItem navigationItem = _navigationItems.Pop();
                NavigateTo(navigationItem.PageType, navigationItem.Data);
            }
        }
    }
}