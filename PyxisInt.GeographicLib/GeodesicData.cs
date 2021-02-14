using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    /// <summary>
    /// The GeodesicData class is used to return the results for a geodesic between
    /// points 1 and 2. Any fields that have not been set will be denoted by the value
    /// Double.NaN. The returned GeodesicData always includes the input paramters, the
    /// latitude &amp; longitude of the points 1 and 2 along with the ArcLength.
    /// </summary>
    public class GeodesicData
    {
        //Latitude of point 1 in degrees
        public double Latitude1 { get; set; }

        //Longitude of point 1 in degrees
        public double Longitude1 { get; set; }

        //Azimuth at point 1 in degrees
        public double InitialAzimuth { get; set; }

        //Latitude of point 2 in degrees
        public double Latitude2 { get; set; }

        //Longitude of point 2 in degrees
        public double Longitude2 { get; set; }

        /// <summary>
        /// Azimuth at point 2 in degrees
        /// </summary>
        public double FinalAzimuth { get; set; }

        /// <summary>
        /// Distance in meters between points 1 and 2 (s12)
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Arc Length in degrees on the auxiliary sphere between points 1 and 2 (a12)
        /// </summary>
        public double ArcLength { get; set; }

        //Reduced length of geodesic in meters
        public double ReducedLength { get; set; }

        //Geodesic scale of point 2 relative to point 1 (dimensionless)
        public double GeodesicScale12 { get; set; }

        //Geodesic scale of point 1 relative to point 2 (dimensionless)
        public double GeodesicScale21 { get; set; }

        //Area in square meters under the geodesic
        public double AreaUnderGeodesic { get; set; }

        public GeodesicData() => Latitude1 = Longitude2 = InitialAzimuth = Latitude2 = Longitude2 = FinalAzimuth = Distance = ArcLength = ReducedLength = GeodesicScale12 = GeodesicScale21 = AreaUnderGeodesic = Double.NaN;
    }
}