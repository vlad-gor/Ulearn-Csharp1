using System;
using System.Linq;


namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var minDay = 1;
            var maxDay = int.MinValue;
            foreach (var day in names)
                maxDay = Math.Max(maxDay, day.BirthDate.Day);
            var days = new string[maxDay - minDay + 1];

            for (var i = 0; i < days.Length; i++)
            {
                days[i] = (i + minDay).ToString();
            }

            var birthCounts = new double[maxDay - minDay + 1];

            foreach (var day in names)
            {
                if (day.Name == name && day.BirthDate.Day > 1)
                    birthCounts[day.BirthDate.Day - minDay]++;
            }

            return new HistogramData(String.Format("Рождаемость людей с именем '{0}'", name), days, birthCounts);
        }
    }
}
