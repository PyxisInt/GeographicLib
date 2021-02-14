using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public static class Math2
    {
        public static double ToRadians(double x)
        {
            return Math.PI * x / 180.0;
        }

        public static double ToDegrees(double x)
        {
            return x * (180.0 / Math.PI);
        }
    }
}