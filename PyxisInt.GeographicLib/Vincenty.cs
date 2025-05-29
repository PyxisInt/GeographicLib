using System;

namespace PyxisInt.GeographicLib
{
    public static class Vincenty
    {
        // WGS-84 ellipsoid parameters
        private const double a = 6378137.0; // Semi-major axis
        private const double f = 1 / 298.257223563; // Flattening
        private const double b = (1 - f) * a; // Semi-minor axis

        public static VincentyResult Inverse(double lat1, double lon1, double lat2, double lon2)
        {
            const double tol = 1e-12;
            const int maxIter = 200;

            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);

            double U1 = Math.Atan((1 - f) * Math.Tan(lat1));
            double U2 = Math.Atan((1 - f) * Math.Tan(lat2));
            double L = lon2 - lon1;
            double λ = L;

            double sinU1 = Math.Sin(U1), cosU1 = Math.Cos(U1);
            double sinU2 = Math.Sin(U2), cosU2 = Math.Cos(U2);

            double sinλ, cosλ, sinσ, cosσ, σ, sinα, cosSqα, cos2σm, C;
            double λprev;
            int iter = 0;

            do
            {
                sinλ = Math.Sin(λ);
                cosλ = Math.Cos(λ);
                sinσ = Math.Sqrt(Math.Pow(cosU2 * sinλ, 2) +
                                 Math.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosλ, 2));
                if (sinσ == 0) return new VincentyResult(); // coincident points

                cosσ = sinU1 * sinU2 + cosU1 * cosU2 * cosλ;
                σ = Math.Atan2(sinσ, cosσ);
                sinα = cosU1 * cosU2 * sinλ / sinσ;
                cosSqα = 1 - sinα * sinα;
                cos2σm = cosσ - 2 * sinU1 * sinU2 / cosSqα;

                if (double.IsNaN(cos2σm)) cos2σm = 0; // equatorial line

                C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));
                λprev = λ;
                λ = L + (1 - C) * f * sinα *
                    (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * cos2σm * cos2σm)));

            } while (Math.Abs(λ - λprev) > tol && ++iter < maxIter);

            if (iter >= maxIter)
                throw new Exception("Vincenty formula failed to converge");

            double uSq = cosSqα * (a * a - b * b) / (b * b);
            double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
            double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
            double Δσ = B * sinσ * (cos2σm + B / 4 *
                                    (cosσ * (-1 + 2 * cos2σm * cos2σm) -
                                     B / 6 * cos2σm * (-3 + 4 * sinσ * sinσ) *
                                     (-3 + 4 * cos2σm * cos2σm)));

            double distance = b * A * (σ - Δσ);
            double initialBearing = RadiansToDegrees(Math.Atan2(cosU2 * sinλ,
                                            cosU1 * sinU2 - sinU1 * cosU2 * cosλ));
            double finalBearing = RadiansToDegrees(Math.Atan2(cosU1 * sinλ,
                                          -sinU1 * cosU2 + cosU1 * sinU2 * cosλ));

            return new VincentyResult
            {
                Distance = distance,
                InitialBearing = NormalizeBearing(initialBearing),
                FinalBearing = NormalizeBearing(finalBearing)
            };
        }
        
        public static VincentyDirectResult Direct(double lat1, double lon1, double initialBearing, double distance)
        {
            double α1 = DegreesToRadians(initialBearing);
            double s = distance;
            double φ1 = DegreesToRadians(lat1);
            double λ1 = DegreesToRadians(lon1);

            double U1 = Math.Atan((1 - f) * Math.Tan(φ1));
            double sinU1 = Math.Sin(U1), cosU1 = Math.Cos(U1);
            double σ1 = Math.Atan2(Math.Tan(U1), Math.Cos(α1));
            double sinα = cosU1 * Math.Sin(α1);
            double cosSqα = 1 - sinα * sinα;
            double uSq = cosSqα * (a * a - b * b) / (b * b);
            double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
            double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));

            double σ = s / (b * A);
            double σprev, cos2σm, sinσ, cosσ, Δσ;

            int iter = 0;
            do
            {
                cos2σm = Math.Cos(2 * σ1 + σ);
                sinσ = Math.Sin(σ);
                cosσ = Math.Cos(σ);
                Δσ = B * sinσ * (cos2σm + B / 4 * (cosσ * (-1 + 2 * cos2σm * cos2σm) -
                        B / 6 * cos2σm * (-3 + 4 * sinσ * sinσ) * (-3 + 4 * cos2σm * cos2σm)));
                σprev = σ;
                σ = s / (b * A) + Δσ;
            } while (Math.Abs(σ - σprev) > 1e-12 && ++iter < 100);

            double tmp = sinU1 * sinσ - cosU1 * cosσ * Math.Cos(α1);
            double φ2 = Math.Atan2(sinU1 * cosσ + cosU1 * sinσ * Math.Cos(α1),
                                   (1 - f) * Math.Sqrt(sinα * sinα + tmp * tmp));
            double λ = Math.Atan2(sinσ * Math.Sin(α1),
                                  cosU1 * cosσ - sinU1 * sinσ * Math.Cos(α1));
            double C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));
            double L = λ - (1 - C) * f * sinα *
                       (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * cos2σm * cos2σm)));

            double λ2 = λ1 + L;
            double α2 = Math.Atan2(sinα, -tmp);

            return new VincentyDirectResult
            {
                Latitude = RadiansToDegrees(φ2),
                Longitude = RadiansToDegrees(λ2),
                FinalBearing = NormalizeBearing(RadiansToDegrees(α2))
            };
        }



        private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;
        private static double RadiansToDegrees(double radians) => radians * 180.0 / Math.PI;

        private static double NormalizeBearing(double bearing)
        {
            bearing %= 360;
            if (bearing < 0) bearing += 360;
            return bearing;
        }
    }

    public class VincentyResult
    {
        public double Distance { get; set; }          // In meters
        public double InitialBearing { get; set; }    // In degrees
        public double FinalBearing { get; set; }      // In degrees
    }
    
    public class VincentyDirectResult
    {
        public double Latitude { get; set; }        // Destination latitude in degrees
        public double Longitude { get; set; }       // Destination longitude in degrees
        public double FinalBearing { get; set; }    // Final bearing at destination in degrees
    }

}
