using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Navigation
{
    public class CPagesStore
    {
        public static CPagesStore Instance
        {
            get { return _instance.Value; }
        }

        private static readonly Lazy<CPagesStore> _instance = new Lazy<CPagesStore>(() => new CPagesStore());

        private readonly Dictionary<EPageType, Page> _pages;

        private CPagesStore()
        {
            _pages = new Dictionary<EPageType, Page>
            {
                {EPageType.None, GetPage(EPageType.None)},
                {EPageType.SignUp, GetPage(EPageType.SignUp)},
                {EPageType.SignIn, GetPage(EPageType.SignIn)},
                {EPageType.Main, GetPage(EPageType.Main)},
            };
        }

        public Page GetPageByType(EPageType pageType)
        {
            return _pages[pageType];
        }

        private Page GetPage(EPageType pageType)
        {
            return CPagesFactory.GetPage(pageType);
        }
    }
}
