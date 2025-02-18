using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Linq;

namespace AvaloniaMusicConsole.Controls;

public partial class SpectrumControl 
    : UserControl
{
    public static readonly StyledProperty<Thickness> DiscreteOfMarkerProperty
       = AvaloniaProperty.Register<SpectrumControl, Thickness>(nameof(DiscreteOfMarker), new Thickness(1, 0.8));
    public Thickness DiscreteOfMarker
    {
        get => GetValue(DiscreteOfMarkerProperty);
        set => SetValue(DiscreteOfMarkerProperty, value);
    }

    public static readonly StyledProperty<int> HeightOfMarkerProperty
        = AvaloniaProperty.Register<SpectrumControl, int>(nameof(HeightOfMarker), 2);
    public int HeightOfMarker
    {
        get => GetValue(HeightOfMarkerProperty);
        set => SetValue(HeightOfMarkerProperty, value);
    }

    public static readonly DirectProperty<SpectrumControl, bool> LightThemeProperty =
            AvaloniaProperty.RegisterDirect<SpectrumControl, bool>(nameof(LightTheme),
                o => o._LightTheme,
                (o, v) => o.LightTheme = v,
                unsetValue: false);

    private bool _LightTheme;
    public bool LightTheme
    {
        get => _LightTheme;
        set => SetAndRaise(LightThemeProperty, ref _LightTheme, value);
    }
    public static readonly DirectProperty<SpectrumControl, int[]> LevelProperty =
             AvaloniaProperty.RegisterDirect<SpectrumControl, int[]>(nameof(Level),
                 o => o._Level,
                 (o, v) => o.Level = v,
                 unsetValue: []);
    private int[] _Level = [];
    public int[] Level
    {
        get => _Level;
        set
        {
            SetAndRaise(LevelProperty, ref _Level, value);
            UpdateContainers(_Level);
        }
    }

    public static readonly DirectProperty<SpectrumControl, int> HeightLevelProperty =
            AvaloniaProperty.RegisterDirect<SpectrumControl, int>(nameof(HeightLevel),
                o => o._HeightLevel,
                (o, v) => o.HeightLevel = v,
                unsetValue: 100);
    private int _HeightLevel;
    public int HeightLevel
    {
        get => _HeightLevel;
        set => SetAndRaise(HeightLevelProperty, ref _HeightLevel, value);
    }

    public static readonly DirectProperty<SpectrumControl, int> NumberBinsProperty =
           AvaloniaProperty.RegisterDirect<SpectrumControl, int>(nameof(NumberBins),
               o => o._NumberBins,
               (o, v) => o.NumberBins = v,
               unsetValue: 10);
    private int _NumberBins;
    public int NumberBins
    {
        get => _NumberBins;
        set => SetAndRaise(NumberBinsProperty, ref _NumberBins, value);
    }

    private int[]? _MarkersRange;
    private MarkerFillBrush? _MarkerBrush;
   

    public SpectrumControl()
    {
        InitializeComponent();
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        _MarkersRange = [.. Enumerable.Range(0, (Design.IsDesignMode ? HeightLevelProperty.GetUnsetValue(this) : HeightLevel) / HeightOfMarker)];
        _MarkerBrush = new MarkerFillBrush(_MarkersRange);

        GenerateRectangles(container);
    }
    private void GenerateRectangles(StackPanel control)
    {
        control.Children.AddRange(Enumerable.Range(0, Design.IsDesignMode ? NumberBinsProperty.GetUnsetValue(this) : NumberBins)
            .Select(i =>
            {
                var sp = new StackPanel(){ Name = $"container{i}" };
                sp.Classes.Add("container");

                sp.Children.AddRange(_MarkersRange!
                    .Select( index =>
                    { 
                        var r = new Rectangle() 
                        { 
                            IsVisible = Design.IsDesignMode,
                            Fill = _MarkerBrush!.GetBrush(index, LightTheme)
                        };
                        r.Classes.Add("bin");
                        
                        return r;
                    }));
                return sp;
            }));
    }
    private void UpdateContainers(int[] values)
    {
        for (int item = 0; item < NumberBins; item++) 
            UpdateRectangles((StackPanel)container.Children[item], values[item]);
    }
    private void UpdateRectangles(StackPanel control, int value)
    {
        if (control == null || control.Children.Any() == false)
            return;

        value = Math.Min(value, HeightLevel); // overflow
        
        var length = control.Children.Count();
        var count = value / HeightOfMarker;
        var remains = length - count;

        for (int i = 0; i < length; i++)
            control.Children[i].IsVisible = i < count;
    }
    
    private record MarkerFillBrush()
    {
        private readonly int[][]? ranges;

        public MarkerFillBrush(int[] markers)
            : this()
        {
            var length = markers.Length / 4;
            var deviation = length / 8;
            var bins = new[]{ 0, length, length * 2, length * 3, markers.Length };

            ranges =
            [
                markers[bins[0]..(bins[1] - deviation)],

                markers[(bins[1]-deviation)..bins[1]],
                markers[bins[1]..(bins[1] + deviation)],
                markers[(bins[1] + deviation)..(bins[2] - deviation)],

                markers[(bins[2] - deviation)..bins[2]],
                markers[bins[2]..(bins[2] + deviation)],
                markers[(bins[2] + deviation)..(bins[3] - deviation)],

                markers[(bins[3] - deviation)..bins[3]],
                markers[bins[3]..(bins[3] + deviation)],
                markers[(bins[3] + deviation)..(bins[4] - deviation)],

                markers[(bins[4] - deviation)..bins[4]],
            ];
        }

        public SolidColorBrush GetBrush(int index, bool lightTheme = false)
        {
            return ranges!
                .Select((r, i) => r.Contains(index) ? i : -1)
                .Where(i => i >= 0)
                .Select(i =>
                {
                    return i switch
                    {
                        0 => SolidColorBrush.Parse(lightTheme ? "#00ff0a" : $"#13126b"),
                        1 => SolidColorBrush.Parse(lightTheme ? "#37ff00" : $"#24126b"),
                        2 => SolidColorBrush.Parse(lightTheme ? "#59ff00" : $"#30126b"),
                        3 => SolidColorBrush.Parse(lightTheme ? "#70ff00" : $"#48126b"),
                        4 => SolidColorBrush.Parse(lightTheme ? "#8fff00" : $"#54126b"),
                        5 => SolidColorBrush.Parse(lightTheme ? "#c1ff00" : $"#64126b"),
                        6 => SolidColorBrush.Parse(lightTheme ? "#e3ff00" : $"#6b1261"),
                        7 => SolidColorBrush.Parse(lightTheme ? "#fff500" : $"#6b1251"),
                        8 => SolidColorBrush.Parse(lightTheme ? "#ff9d00" : $"#6b123c"),
                        9 => SolidColorBrush.Parse(lightTheme ? "#ff9d00" : $"#6b1221"),
                        _ => SolidColorBrush.Parse(lightTheme ? "#ff4d00" : $"#6b1212")
                    };
                })
                .First();
        }
    }
}