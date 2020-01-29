using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.ViewModels;
using UI.Views;

namespace UI.Navigation.Game
{
    public class CGameNavigator
    {
        public static CGameNavigator Instance => _instance.Value;
        private static readonly Lazy<CGameNavigator> _instance = new Lazy<CGameNavigator>(() => new CGameNavigator());

        private Dictionary<EAreaType, ContentControl> _pages;

        //private Stack<CNavigationHistoryItem> _navigationItems;

        public event EventHandler<ContentControl> Navigate;

        private CGameNavigator()
        {
            _pages = new Dictionary<EAreaType, ContentControl>();
            //_navigationItems = new Stack<CNavigationHistoryItem>();
        }

        public void Register(ContentControl control, EAreaType type)
        {
            if (_pages.ContainsKey(type))
            {
                throw new Exception($"Control type {type} already exist");
            }
            _pages.Add(type, control);
        }

        public void NavigateTo(EAreaType areaType, Object data = null)
        {
            var eventData = new CAreaNavigateEventArgs(areaType, data);
            ContentControl control = GetContentControl(areaType);
            if (control.DataContext is ViewModelBase vm)
            {
                vm.OnNavigated(data);
            }
            Navigate?.Invoke(this, control);
            //_navigationItems.Push(new CNavigationHistoryItem(pageType, targetId));
        }

        private ContentControl GetContentControl(EAreaType areaType)
        {
            if (_pages.ContainsKey(areaType))
            {
                return _pages[areaType];
            }
            throw new Exception($"Control type {areaType} not found");
        }

        //public void PageNavigateToBack()
        //{
        //    if (_navigationItems.Count > 1)
        //    {
        //        _navigationItems.Pop();
        //        CNavigationHistoryItem navigationItem = _navigationItems.Pop();
        //        PageNavigateTo(navigationItem.PageType, navigationItem.TargetId);
        //    }
        //}
    }

    public class CAreaNavigateEventArgs
    {
        public CAreaNavigateEventArgs(EAreaType area, Object data)
        {
            Area = area;
            Data = data;
        }

        public EAreaType Area { get; set; }
        public Object Data { get; set; }
    }

    public enum EAreaType
    {
        Main,
        RoundEnded,
        HeroSelection,
        Trading,
        Battle
    }
}