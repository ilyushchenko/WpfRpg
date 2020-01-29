using System;
using System.Windows.Input;
using Interfaces;
using UI.Internal;

namespace UI.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private ICell _cell;
        private SPoint _drawPosition;
        private Boolean _selected;


        public CellViewModel() : this(null)
        {
        }

        public CellViewModel(ICell cell)
        {
            Size = 50;
            MakeSelected = new CRelayCommand(SelectCell);
            Cell = cell;
        }

        public Double Size { get; set; }

        public ICell Cell
        {
            get => _cell;
            set
            {
                _cell = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Unit));
            }
        }

        public IPositionable Unit => Cell?.Unit;

        public SPoint DrawPosition
        {
            get => _drawPosition;
            set
            {
                _drawPosition = value;
                OnPropertyChanged();
            }
        }

        public ICommand MakeSelected { get; }

        public Boolean Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler<ICell> Clicked;

        private void SelectCell(Object parameter)
        {
            Clicked?.Invoke(this, Cell);
        }
    }
}