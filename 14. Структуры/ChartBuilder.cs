using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Profiling
{
    public class ChartBuilder : IChartBuilder
    {
        public Control Build(List<ExperimentResult> results)
        {
            var chart = GetChartBase(results.Count + 1);
            var position = 1;
            var elementsAmount = 1;
            foreach (var result in results)
            {
                chart.Series["ClassResult"].Points.AddXY(position, result.ClassResult);
                chart.Series["StructResult"].Points.AddXY(position, result.StructResult);
                chart.ChartAreas["main"].AxisX.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = position - 0.5,
                    ToPosition = position + 0.5,
                    Text = elementsAmount.ToString()
                });
                elementsAmount *= 2;
                position++;
            }
            chart.Legends.Add("Legend");
            chart.Legends["Legend"].BorderColor = Color.Black;
            return chart;
        }

        private Chart GetChartBase(int axisXMax)
        {
            var chart = new Chart { Palette = ChartColorPalette.Pastel };
            chart.Titles.Add("Время работы, мс");
            chart.ChartAreas.Add("main");
            chart.ChartAreas["main"].AxisX.Maximum = axisXMax;
            chart.Series.Add("ClassResult");
            chart.Series["ClassResult"].ChartType = SeriesChartType.Column;
            chart.Series["ClassResult"].Color = Color.Green;
            chart.Series["ClassResult"].LegendText = "Класс";
            chart.Series.Add("StructResult");
            chart.Series["StructResult"].ChartType = SeriesChartType.Column;
            chart.Series["StructResult"].Color = Color.Blue;
            chart.Series["StructResult"].LegendText = "Структура";
            return chart;
        }
    }
}