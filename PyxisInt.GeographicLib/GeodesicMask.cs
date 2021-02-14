using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public static class GeodesicMask
    {
        internal static readonly int CAP_NONE = 0;
        internal static readonly int CAP_C1 = 1 << 0;
        internal static readonly int CAP_C1p = 1 << 1;
        internal static readonly int CAP_C2 = 1 << 2;
        internal static readonly int CAP_C3 = 1 << 3;
        internal static readonly int CAP_C4 = 1 << 4;
        internal static readonly int CAP_ALL = 0x1F;
        internal static readonly int CAP_MASK = CAP_ALL;
        internal static readonly int OUT_ALL = 0x7F80;
        internal static readonly int OUT_MASK = 0xFF80;    //Include LONG_UNROLL

        public static readonly int NONE = 0;
        public static readonly int LATITUDE = 1 << 7 | CAP_NONE;
        public static readonly int LONGITUDE = 1 << 8 | CAP_C3;
        public static readonly int AZIMUTH = 1 << 9 | CAP_NONE;
        public static readonly int DISTANCE = 1 << 10 | CAP_C1;
        public static readonly int STANDARD = LATITUDE | LONGITUDE | AZIMUTH | DISTANCE;
        public static readonly int DISTANCE_IN = 1 << 11 | CAP_C1 | CAP_C1p;
        public static readonly int REDUCEDLENGTH = 1 << 12 | CAP_C1 | CAP_C2;
        public static readonly int GEODESICSCALE = 1 << 13 | CAP_C1 | CAP_C2;
        public static readonly int AREA = 1 << 14 | CAP_C4;
        public static readonly int ALL = OUT_ALL | CAP_ALL;
        public static readonly int LONG_UNROLL = 1 << 15;
    }
}