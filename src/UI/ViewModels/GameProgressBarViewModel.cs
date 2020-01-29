using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UI.ViewModels
{
    public class GameProgressBarViewModel : ViewModelBase
    {
        private String _header;
        private Int32 _maxValue;
        private Int32 _currentValue;
        private SolidColorBrush _color;

        public String Header
        {
            get => _header;
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public Int32 MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                OnPropertyChanged();
            }
        }

        public Int32 CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
    }
}