using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Navigation
{
    public class CNavigationHistoryItem
    {
        public EPageType PageType { get; set; }

        public object Data { get; set; }

        public CNavigationHistoryItem(EPageType pageType, object data)
        {
            PageType = pageType;
            Data = data;
        }
    }
}
