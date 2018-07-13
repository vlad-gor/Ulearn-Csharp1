using GeometryTasks;
using System.Drawing;
using System.Collections.Generic;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color colorValue)
        {
            if (colors.Count > 0 && colorValue != null)
            {
                if (colors.ContainsKey(segment))
                {
                    colors.Remove(segment);
                }
                colors.Add(segment, colorValue);
            }

            else if (colorValue != null) colors.Add(segment, colorValue);
        }

        public static Color GetColor(this Segment segment)
        {
            if (segment != null)
            {
                Color val;
                if (colors.TryGetValue(segment, out val)) return colors[segment];
            }
            return Color.Black;
        }

        public static bool Contains(Segment segment, Color colorValue)
        {
            foreach (var key in colors.Keys)
            {
                if (Equal(key, segment))
                {
                    if (colors[key] != colorValue) return true;
                }
                return false;
            }
            return false;
        }

        public static bool Equal(Segment segment1, Segment segment2)
        {
            if (segment1.Begin != null && 
				segment2.Begin != null && 
				segment1.End != null && 
				segment2.End != null)
            {
                if (segment1.Begin.X == segment2.Begin.X &&
				  segment1.Begin.Y == segment2.Begin.Y &&
				  segment1.End.X == segment2.End.X &&
				  segment1.End.Y == segment2.End.Y) return true;
            }
            return false;
        }
    }
}