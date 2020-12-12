using Microcharts;
using Restaurant.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Reports
{
    public class TestChartViewModel : ViewModelBase
    {
        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }
        public TestChartViewModel()
        {
            InitData();
        }
        private void InitData()
        {
            var blueColor = SKColor.Parse("#09C");
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(200)
                {
                    Label = "Khoa",
                    ValueLabel = "200",
                    Color = blueColor
                },
                new ChartEntry(450)
                {
                    Label = "Mot",
                    ValueLabel = "450",
                    Color = blueColor
                },
                new ChartEntry(800)
                {
                    Label = "Lan",
                    ValueLabel = "800",
                    Color = blueColor
                },
            };

            BarChart = new BarChart { Entries = chartEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
        }
    }
}
