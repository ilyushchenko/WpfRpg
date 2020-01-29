using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModels
{
    /// <summary>
    /// Base class for ViewModels
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public virtual void OnNavigated(Object data)
        {
        }
    }
}
