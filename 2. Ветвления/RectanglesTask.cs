using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return Math.Min(r1.Right, r2.Right) >= Math.Max(r1.Left, r2.Left) &&
                   Math.Min(r1.Bottom, r2.Bottom) >= Math.Max(r1.Top, r2.Top);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            return AreIntersected(r1, r2) ? (Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left)) *
                                            (Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top))
                                            : 0;
        }


        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (r1.Left >= r2.Left && r1.Right <= r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom)
                return 0;
            else if (r1.Left <= r2.Left && r1.Right >= r2.Right && r1.Top <= r2.Top && r1.Bottom >= r2.Bottom)
                return 1;
            else
                return -1;
        }
    }
}
