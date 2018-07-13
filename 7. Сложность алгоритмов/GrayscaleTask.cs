namespace Recognizer
{
	public static class GrayscaleTask
	{     
        const double RedMultiplier = 0.299;
        const double GreenMultiplier = 0.587;
        const double BlueMultiplier = 0.114;
        const double NumberOfShades = 255;

		public static double[,] ToGrayscale(Pixel[,] original)
		{
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var grayscale = new double[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    grayscale[x, y] = (original[x, y].R * 
									   RedMultiplier + original[x, y].G * 
									   GreenMultiplier + original[x, y].B * 
									   BlueMultiplier) / NumberOfShades;
            return grayscale;
		}
	}
}