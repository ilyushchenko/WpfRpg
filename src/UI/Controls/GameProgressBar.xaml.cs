using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    /// <summary>
    ///     Логика взаимодействия для GameProgressBar.xaml
    /// </summary>
    public partial class GameProgressBar : UserControl
    {
        public static readonly DependencyProperty HeaderDependencyProperty;
        public static readonly DependencyProperty MaxValueDependencyProperty;
        public static readonly DependencyProperty CurrentValueDependencyProperty;
        public static readonly DependencyProperty ColorDependencyProperty;

        static GameProgressBar()
        {
            HeaderDependencyProperty = DependencyProperty.Register(
                nameof(Header),
                typeof(String),
                typeof(GameProgressBar),
                new PropertyMetadata(String.Empty, OnHeaderChanged));

            MaxValueDependencyProperty = DependencyProperty.Register(
                nameof(MaxValue),
                typeof(Int32),
                typeof(GameProgressBar),
                new PropertyMetadata(0, OnMaxValueChanged));

            CurrentValueDependencyProperty = DependencyProperty.Register(
                nameof(CurrentValue),
                typeof(Int32),
                typeof(GameProgressBar),
                new PropertyMetadata(0, OnCurrentValueChanged));

            ColorDependencyProperty = DependencyProperty.Register(
                nameof(Color),
                typeof(SolidColorBrush),
                typeof(GameProgressBar),
                new PropertyMetadata(new SolidColorBrush(Colors.Green), OnColorChanged));
        }

        public GameProgressBar()
        {
            InitializeComponent();
        }

        public String Header
        {
            get => (String) GetValue(HeaderDependencyProperty);
            set => SetValue(HeaderDependencyProperty, value);
        }

        public Int32 MaxValue
        {
            get => (Int32) GetValue(MaxValueDependencyProperty);
            set => SetValue(MaxValueDependencyProperty, value);
        }

        public Int32 CurrentValue
        {
            get => (Int32) GetValue(CurrentValueDependencyProperty);
            set => SetValue(CurrentValueDependencyProperty, value);
        }

        public SolidColorBrush Color
        {
            get => (SolidColorBrush) GetValue(ColorDependencyProperty);
            set => SetValue(ColorDependencyProperty, value);
        }

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is GameProgressBar control)) return;
            if (!(e.NewValue is Int32 value)) return;

            control.SetBarMaxValue(value);
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is GameProgressBar control)) return;
            if (!(e.NewValue is Int32 value)) return;

            control.SetBarValue(value);
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is GameProgressBar control)) return;
            if (!(e.NewValue is SolidColorBrush value)) return;

            control.SetBarColor(value);
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is GameProgressBar control)) return;
            if (!(e.NewValue is String value)) return;

            control.SetHeader(value);
        }

        private void SetBarMaxValue(Double maxValue)
        {
            Bar.Maximum = maxValue;
        }

        private void SetBarValue(Double value)
        {
            Bar.Value = value;
        }

        private void SetBarColor(Brush color)
        {
            Bar.Foreground = color;
        }

        private void SetHeader(String header)
        {
            HeaderLabel.Content = header;
        }
    }
}