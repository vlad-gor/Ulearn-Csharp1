using System;
using System.Drawing;


namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static double GetXCoord(double x, double y, double angle)
        {
            return (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2);
        }

        public static double GetYCoord(double x, double y, double angle)
        {
            return (x * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2);
        }

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var x = 1.0;
            var y = 0.0;
            var initRandom = new Random(seed);

            for (int i = 0; i < iterationsCount; ++i)
            {
                var oldX = x;
                // Transformation A
                if (initRandom.Next(2) == 0)
                {
                    x = GetXCoord(x, y, Math.PI / 4);
                    y = GetYCoord(oldX, y, Math.PI / 4);
                }
                // Transformation B
                else
                {
                    x = GetXCoord(x, y, Math.PI * 3 / 4) + 1;
                    y = GetYCoord(oldX, y, Math.PI * 3 / 4);
                }
                pixels.SetPixel(x, y);
            }
        }
    }
}
