using System;

namespace AngryBirds
{
    public static class AngryBirdsTask
    {
        public static double FindSightAngle(double v, double distance)
        {
            return Math.Asin((9.8 * distance) / (v * v)) / 2;
        }
    }
}