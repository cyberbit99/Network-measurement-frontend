using Microcharts;
using Network_measurement_frontend.Shared.Model;
using SkiaSharp;
using System.Collections.ObjectModel;
using static Network_measurement_frontend.Pages.ColorCalculator;

namespace Network_measurement_frontend.Pages;

public partial class DiagramPage : ContentPage
{
    public List<ChartEntry> Items { get; set; } = new List<ChartEntry>();
    ColorCalculator CC = new ColorCalculator();
    public DiagramPage(List<NetworkData> initialItems)
	{
		InitializeComponent();
        foreach (var item in initialItems)
        {
            RgbValues rgb = CC.GetRgbValues(-90, -30, (float)item.SignalStrengthDecibel);

            string hex = rgb.Green.ToString("X2") + rgb.Red.ToString("X2") + rgb.Blue.ToString("X2");
            ChartEntry chartEntry = new ChartEntry((float)item.SignalStrengthDecibel*-1)
            {
                
                Label = item.Ssid,
                ValueLabel = item.SignalStrengthDecibel.ToString(),
                Color = SKColor.Parse(hex),
                
                
            };
            Items.Add(chartEntry);
        }
        chart.Chart = new BarChart
        {
            Entries = Items.ToArray(),
            MinBarHeight = 70,
            
        };

	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}
public class ColorCalculator
{
    public  RgbValues GetRgbValues(float minimum, float maximum, float value)
    {
        var normalizedValue = Normalize(minimum, maximum, value);

        return new RgbValues
        {
            Blue = Distance(normalizedValue, 0),
            Green = Distance(normalizedValue, 1),
            Red = Distance(normalizedValue, 2)
        };
    }

    private float Normalize(float minimum, float maximum, float value)
    {
        return (value - minimum) / (maximum - minimum) * 2;
    }

    private int Distance(float value, float color)
    {
        var distance = Math.Abs(value - color);

        var colorStrength = 1 - distance;

        if (colorStrength < 0)
            colorStrength = 0;

        return (int)Math.Round(colorStrength * 255);
    }
    public class RgbValues
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}