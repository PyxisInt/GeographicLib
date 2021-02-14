using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public static class Constants
    {
        // The equatorial radius of the WGS84 ellipsoid in meters
        public static readonly double WGS84_a = 6378137;

        // The flattening at the poles of the WGS84 ellipsoid
        public static readonly double WGS84_f = 1 / 298.257223563;
    }
}