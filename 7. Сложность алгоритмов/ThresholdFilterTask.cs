using System;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double threshold)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            double actualThreshold = FindActualThreshold(original, threshold, width, height);
            var blackAndWhitePixels = new double[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    blackAndWhitePixels[x, y] = (original[x, y] >= actualThreshold) ? 1.0 : 0.0;
            return blackAndWhitePixels;
        }

        private static double FindActualThreshold(double[,] original, double threshold, int width, int height)
        {
            var pixelsCount = width * height;
            var allPixels = new double[pixelsCount];
            for (int i = 0; i < pixelsCount; i++)
                allPixels[i] = original[i % width, i / width];
            Array.Sort(allPixels);

            var actualThreshold = 0.0;
            var whitePixelsCount = (int)(threshold * pixelsCount);
            if (whitePixelsCount == pixelsCount) actualThreshold = double.NegativeInfinity;
            else if (whitePixelsCount == 0) actualThreshold = double.PositiveInfinity;
            else actualThreshold = allPixels[Math.Max(pixelsCount - whitePixelsCount, 0)];
            return actualThreshold;
        }
    }
}