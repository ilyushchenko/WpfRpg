using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Models.Terrains;
using Interfaces;
using UI.Factories;

namespace UI.Controls
{
    /// <summary>
    ///     Логика взаимодействия для CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        public static readonly DependencyProperty TerrainProperty;
        public static readonly DependencyProperty UnitProperty;
        public static readonly DependencyProperty SelectedProperty;
        public static readonly RoutedEvent ClickEvent;

        static CellControl()
        {
            TerrainProperty = DependencyProperty.Register(
                nameof(Terrain),
                typeof(ITerrain),
                typeof(CellControl),
                new PropertyMetadata(new CDirt(), OnTerrainChanged));
            UnitProperty = DependencyProperty.Register(
                nameof(Unit),
                typeof(IPositionable),
                typeof(CellControl),
                new PropertyMetadata(null, OnUnitChanged));
            SelectedProperty = DependencyProperty.Register(
                nameof(Selected),
                typeof(Boolean),
                typeof(CellControl),
                new PropertyMetadata(false, OnSelectedChanged));
            ClickEvent = EventManager.RegisterRoutedEvent(
                nameof(Click), RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CellControl));
        }

        public CellControl()
        {
            InitializeComponent();
            TerrainTexture.Source = CTerrainTextureFactory.GetTexture(Terrain);
        }

        public ITerrain Terrain
        {
            get => (ITerrain) GetValue(TerrainProperty);
            set => SetValue(TerrainProperty, value);
        }

        public IPositionable Unit
        {
            get => (IPositionable) GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        public Boolean Selected
        {
            get => (Boolean) GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }

        private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is CellControl cellControl)) return;
            if (!(e.NewValue is Boolean selected)) return;
            cellControl.CellSelectedBorder.Visibility = selected ? Visibility.Visible : Visibility.Hidden;
        }

        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        private static void OnUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is CellControl cellControl)) return;

            if (e.NewValue == null)
                cellControl.ClearTexture();
            else
                cellControl.UpdateUnitImage(e.NewValue as IPositionable);
        }

        private static void OnTerrainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void UpdateUnitImage(IPositionable unit)
        {
            UnitTexture.Source = CUnitTextureFactory.GetTexture(unit);
        }

        private void ClearTexture()
        {
            UnitTexture.Source = null;
        }

        private void RaiseClickEvent()
        {
            var newEventArgs = new RoutedEventArgs(ClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void CellControl_OnPreviewMouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}