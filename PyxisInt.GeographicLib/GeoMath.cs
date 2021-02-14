using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public static class GeoMath
    {
        public static readonly int Digits = 53;

        public static readonly double Epsilon = Math.Pow(0.5, Digits - 1);
        public static readonly double Min = Math.Pow(0.5, 1022);

        public static double Square(double x)
        {
            return x * x;
        }

        public static double Hypot(double x, double y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            double a = Math.Max(x, y);
            double b = Math.Min(x, y) / (a != 0 ? a : 1);
            return a * Math.Sqrt(1 + b * b);
        }

        public static double Log1p(double x)
        {
            double y = 1 + x;
            double z = y - 1;
            return z == 0 ? x : x * Math.Log(y) / z;
        }

        public static double Atanh(double x)
        {
            double y = Math.Abs(x);
            y = Log1p(2 * y / (1 - y)) / 2;
            return x < 0 ? -y : y;
        }

        public static double CopySign(double x, double y)
        {
            return Math.Abs(x) * (y < 0 || (y == 0 && 1 / y < 0) ? -1 : 1);
        }

        public static double CubeRoot(double x)
        {
            double y = Math.Pow(Math.Abs(x), 1 / 3.0); // Return the real cube root
            return x < 0 ? -y : y;
        }

        public static Pair Norm(double sinx, double cosx)
        {
            double r = Hypot(sinx, cosx);
            return new Pair(sinx / r, cosx / r);
        }

        public static Pair Sum(double u, double v)
        {
            double s = u + v;
            double up = s - v;
            double vpp = s - up;
            up -= u;
            vpp -= v;
            double t = -(up + vpp);
            // u + v =       s      + t
            //       = round(u + v) + t
            return new Pair(s, t);
        }

        public static double PolyVal(int N, double[] p, int s, double x)
        {
            double y = N < 0 ? 0 : p[s++];
            while (--N >= 0) y = y * x + p[s++];
            return y;
        }

        public static double AngRound(double x)
        {
            // The makes the smallest gap in x = 1/16 - nextafter(1/16, 0) = 1/2^57
            // for reals = 0.7 pm on the earth if x is an angle in degrees.  (This
            // is about 1000 times more resolution than we get with angles around 90
            // degrees.)  We use this to avoid having to deal with near singular
            // cases when x is non-zero but tiny (e.g., 1.0e-200).  This converts -0 to
            // +0; however tiny negative numbers get converted to -0.
            double z = 1 / 16.0;
            if (x == 0) return 0;
            double y = Math.Abs(x);
            // The compiler mustn't "simplify" z - (z - y) to y
            y = y < z ? z - (z - y) : y;
            return x < 0 ? -y : y;
        }

        /// <summary>
        /// Normalize the angle and return in degrees
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double AngNormalize(double x)
        {
            x = x % 360.0;
            return x <= -180 ? x + 360 : (x <= 180 ? x : x - 360);
        }

        public static double LatFix(double x)
        {
            return Math.Abs(x) > 90 ? Double.NaN : x;
        }

        public static Pair AngDiff(double x, double y)
        {
            double d, t;
            {
                Pair r = Sum(AngNormalize(-x), AngNormalize(y));
                d = AngNormalize(r.First); t = r.Second;
            }
            return Sum(d == 180 && t > 0 ? -180 : d, t);
        }

        public static Pair SinCosD(double x)
        {
            // In order to minimize round-off errors, this function exactly reduces
            // the argument to the range [-45, 45] before converting it to radians.
            double r; int q;
            r = x % 360.0;
            q = (int)Math.Floor(r / 90 + 0.5);
            r -= 90 * q;
            // now abs(r) <= 45
            r = Math2.ToRadians(r);
            // Possibly could call the gnu extension sincos
            double s = Math.Sin(r), c = Math.Cos(r);
            double sinx, cosx;
            switch (q & 3)
            {
                case 0: sinx = s; cosx = c; break;
                case 1: sinx = c; cosx = -s; break;
                case 2: sinx = -s; cosx = -c; break;
                default: sinx = -c; cosx = s; break; // case 3
            }
            if (x != 0) { sinx += 0.0; cosx += 0.0; }
            return new Pair(sinx, cosx);
        }

        public static double Atan2d(double y, double x)
        {
            // In order to minimize round-off errors, this function rearranges the
            // arguments so that result of atan2 is in the range [-pi/4, pi/4] before
            // converting it to degrees and mapping the result to the correct
            // quadrant.
            int q = 0;
            if (Math.Abs(y) > Math.Abs(x)) { double t; t = x; x = y; y = t; q = 2; }
            if (x < 0) { x = -x; ++q; }
            // here x >= 0 and x >= abs(y), so angle is in [-pi/4, pi/4]
            double ang = Math2.ToDegrees(Math.Atan2(y, x));
            switch (q)
            {
                // Note that atan2d(-0.0, 1.0) will return -0.  However, we expect that
                // atan2d will not be called with y = -0.  If need be, include
                //
                //   case 0: ang = 0 + ang; break;
                //
                // and handle mpfr as in AngRound.
                case 1: ang = (y >= 0 ? 180 : -180) - ang; break;
                case 2: ang = 90 - ang; break;
                case 3: ang = -90 + ang; break;
            }
            return ang;
        }

        public static bool IsFinite(double x)
        {
            return Math.Abs(x) <= Double.MaxValue;
        }
    }
}