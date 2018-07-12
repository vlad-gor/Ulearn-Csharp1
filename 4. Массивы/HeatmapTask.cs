using System;


namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var minDay = 2;
            var maxDay = 31;
            var days = new string[maxDay - minDay + 1];
            for (var i = 0; i < days.Length; i++)
                days[i] = (i + minDay).ToString();
            var minMonth = 1;
            var maxMonth = 12;
            var months = new string[maxMonth];

            for (var i = 0; i < months.Length; i++)
                months[i] = (i + minMonth).ToString();
            var birthCounts = new double[days.Length, months.Length];

            foreach (var name in names)
            {
                if (name.BirthDate.Day > 1)
                    birthCounts[(name.BirthDate.Day - minDay), (name.BirthDate.Month - minMonth)]++;
            }

            return new HeatmapData("Карта интенсивностей рождаемости", birthCounts, days, months);
        }
    }
}
