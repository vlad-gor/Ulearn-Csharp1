using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            var shift = sx.GetLength(0) / 2;

            for (int x = shift; x < width - shift; x++)
                for (int y = shift; y < height - shift; y++)
                {
                   var gx = MultiplyPixelsByMatrix(g, sx, x, y, shift);
                   var gy = MultiplyPixelsByMatrix(g, sy, x, y, shift);
                   result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] matrix)
        {
            var sideLength = matrix.GetLength(0);
            var transposedMatrix = new double[sideLength, sideLength];
            for (int x = 0; x < sideLength; x++)
                for (int y = 0; y < sideLength; y++)
                    transposedMatrix[x, y] = matrix[y, x];
            return transposedMatrix;
        }

        public static double MultiplyPixelsByMatrix(double[,] pixels, double[,] matrix, int x, int y, int shift)
        {
            double result = 0;
            int sideLength = matrix.GetLength(0);
            for (int i = 0; i < sideLength; i++)
                for (int j = 0; j < sideLength; j++)
                    result += matrix[i, j] * pixels[x - shift + i, y - shift + j];
            return result;
        }
    }
}