// Вставьте сюда финальное содержимое файла MedianFilterTask.cs
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var medianPixels = new double[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    medianPixels[x, y] = GetMedianPixelValue(x, y, original);
			return medianPixels;
		}

        public static double GetMedianPixelValue(int x, int y, double[,] pixels)
        {
            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);
            var pixelValues = new List<double>();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (IsPixelInsideBoundaries(x - 1 + i, y - 1 + j, width, height))
                        pixelValues.Add(pixels[x - 1 + i, y - 1 + j]);

            pixelValues.Sort();
            if (pixelValues.Count % 2 == 1)
                return pixelValues[pixelValues.Count / 2];
            else
                return (pixelValues[(pixelValues.Count / 2) - 1] + pixelValues[pixelValues.Count / 2]) / 2;
        }

        public static bool IsPixelInsideBoundaries(int x, int y, int width, int height)
        {
            return (x > -1 && y > -1) && (x < width && y < height);
        }
	}
}