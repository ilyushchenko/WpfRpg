using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Models;
using Interfaces;
using UI.Events;
using UI.Internal;
using UI.Models;
using UI.ViewModels;

namespace UI.Controls
{
    /// <summary>
    ///     Логика взаимодействия для MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty SourceProperty;
        private static readonly DependencyProperty SelectedCellProperty;
        private static readonly DependencyProperty VisibleWidthProperty;
        private static readonly DependencyProperty VisibleHeightProperty;
        private static readonly RoutedEvent CellClickEvent;
        private CellViewModel[] _cells;
        private ICommand _moveMapCommand;
        private Int32 _xArea;
        private Int32 _yArea;

        static MapControl()
        {
            SourceProperty = DependencyProperty.Register(
                nameof(Source),
                typeof(CMap),
                typeof(MapControl),
                new PropertyMetadata(null, OnMapChanged));

            VisibleWidthProperty = DependencyProperty.Register(
                nameof(VisibleWidth),
                typeof(Int32),
                typeof(MapControl),
                new PropertyMetadata(10, OnAreaXChanged));

            VisibleHeightProperty = DependencyProperty.Register(
                nameof(VisibleHeight),
                typeof(Int32),
                typeof(MapControl),
                new PropertyMetadata(10, OnAreaYChanged));

            SelectedCellProperty = DependencyProperty.Register(
                nameof(SelectedCell),
                typeof(ICell),
                typeof(MapControl),
                new PropertyMetadata(null, OnSelectedCellChanged));

            CellClickEvent = EventManager.RegisterRoutedEvent(
                nameof(CellClick),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MapControl));
        }

        public MapControl()
        {
            InitializeComponent();
            DataContext = this;
            ResizeMap(VisibleWidth, VisibleHeight);
            _moveMapCommand = new CRelayCommand(MoveMapAction);
        }

        public CellViewModel[] Cells
        {
            get => _cells;
            set
            {
                _cells = value;
                OnPropertyChanged();
            }
        }

        public CMap Source
        {
            get => (CMap) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Int32 VisibleWidth
        {
            get => (Int32) GetValue(VisibleWidthProperty);
            set => SetValue(SourceProperty, value);
        }

        public Int32 VisibleHeight
        {
            get => (Int32) GetValue(VisibleHeightProperty);
            set => SetValue(SourceProperty, value);
        }

        public Int32 XArea
        {
            get => _xArea;
            set
            {
                if (_xArea == value) return;
                if (value <= 0) value = 0;

                Int32 maxRightPosition = Source.Width - VisibleWidth;
                if (value > maxRightPosition) value = maxRightPosition;
                _xArea = value;
                UpdateMap();
            }
        }

        public Int32 YArea
        {
            get => _yArea;
            set
            {
                if (_yArea == value) return;
                if (value <= 0) value = 0;

                Int32 maxDownPosition = Source.Height - VisibleHeight;
                if (value > maxDownPosition) value = maxDownPosition;
                _yArea = value;
                UpdateMap();
            }
        }

        public ICommand MoveMapCommand
        {
            get => _moveMapCommand;
            set
            {
                _moveMapCommand = value;
                OnPropertyChanged();
            }
        }

        public ICell SelectedCell
        {
            get => (ICell) GetValue(SelectedCellProperty);
            set => SetValue(SelectedCellProperty, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void OnSelectedCellChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MapControl mapControl)) return;
            if (!(e.NewValue is ICell cell)) return;

            CellViewModel cellViewModel = mapControl.Cells.FirstOrDefault(cvm => cvm.Cell == cell);

            //TODO: Process selected cell
        }

        public event RoutedEventHandler CellClick
        {
            add => AddHandler(CellClickEvent, value);
            remove => RemoveHandler(CellClickEvent, value);
        }

        public void OnPropertyChanged([CallerMemberName] String prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected virtual void RaiseCellClickEvent()
        {
            var args = new RoutedEventArgs(CellClickEvent);
            RaiseEvent(args);
        }

        private void MoveMapAction(Object parameter)
        {
            if (parameter is EMoveDirections direction)
                switch (direction)
                {
                    case EMoveDirections.Up:
                        YArea--;
                        break;
                    case EMoveDirections.Down:
                        YArea++;
                        break;
                    case EMoveDirections.Left:
                        XArea--;
                        break;
                    case EMoveDirections.Right:
                        XArea++;
                        break;
                }
        }

        private static void OnAreaYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MapControl mapControl)) return;
            if (!(e.NewValue is Int32 yValue)) return;

            mapControl.VisibleHeight = yValue;
        }

        private static void OnAreaXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MapControl mapControl)) return;
            if (!(e.NewValue is Int32 xValue)) return;

            mapControl.VisibleWidth = xValue;
        }

        private static void OnMapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MapControl mapControl)) return;
            if (!(e.NewValue is CMap map)) return;
            mapControl.SetMap(map);
        }

        private void SetMap(CMap map)
        {
            Source = map;
            ResizeMap(VisibleWidth, VisibleHeight);
            UpdateMap();
        }

        private void UpdateMap()
        {
            for (var y = 0; y < VisibleHeight; y++)
            for (var x = 0; x < VisibleWidth; x++)
            {
                var xIndex = XArea + x;
                var yIndex = YArea + y;

                if (xIndex >= Source.Width || yIndex >= Source.Height ||
                    xIndex < 0 || yIndex < 0)
                {
                    continue;
                }

                ICell cell = Source.GetCell(new SPoint(xIndex, yIndex));
                Int32 index = GetIndex(x, y);
                CellViewModel cellViewModel = Cells[index];

                if (SelectedCell != null && cellViewModel.Cell == SelectedCell)
                {
                    cellViewModel.Selected = true;
                }
                else
                {
                    cellViewModel.Selected = false;
                }

                cellViewModel.Cell = cell;
            }
        }

        private void ResizeMap(Int32 width, Int32 height)
        {
            var cells = new CellViewModel[width * height];

            Double cellSize = ItemsControl.Width / VisibleWidth;

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                Int32 drawPositionX = x * (Int32) cellSize;
                Int32 drawPositionY = y * (Int32) cellSize;

                var cellVm = new CellViewModel
                {
                    Size = cellSize,
                    DrawPosition = new SPoint(drawPositionX, drawPositionY)
                };
                cellVm.Clicked += CellVmOnClicked;

                Int32 index = GetIndex(x, y);

                cells[index] = cellVm;
            }

            Cells = cells;
        }

        private Int32 GetIndex(Int32 x, Int32 y)
        {
            return VisibleWidth * y + x;
        }

        private void CellVmOnClicked(Object sender, ICell cell)
        {
            SelectedCell = cell;
            RaiseCellClickEvent();
            UpdateMap();
        }
        private void MapOnUnitPositionChanged(Object sender, CUnitPositionChangedEventArgs e)
        {
            UpdateMap();
        }

        public void Update()
        {
            UpdateMap();
        }
    }
}