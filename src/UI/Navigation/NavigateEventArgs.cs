using System;

namespace UI.Navigation
{
    public class NavigateEventArgs
    {
        public NavigateEventArgs(EPageType pageType, Object data = null)
        {
            PageType = pageType;
            Data = data;
        }

        public EPageType PageType { get; }

        public Object Data { get; }
    }
}