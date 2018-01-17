using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.GeographicLib
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
        public double lat1 { get; set; }

        //Longitude of point 1 in degrees
        public double lon1 { get; set; }

        //Azimuth at point 1 in degrees
        public double azi1 { get; set; }

        //Latitude of point 2 in degrees
        public double lat2 { get; set; }

        //Longitude of point 2 in degrees
        public double lon2 { get; set; }

        /// <summary>
        /// Azimuth at point 2 in degrees
        /// </summary>
        public double azi2 { get; set; }

        /// <summary>
        /// Distance in meters between points 1 and 2 (s12)
        /// </summary>
        public double s12 { get; set; }

        /// <summary>
        /// Arc Length in degrees on the auxiliary sphere between points 1 and 2 (a12)
        /// </summary>
        public double a12 { get; set; }

        //Reduced length of geodesic in meters
        public double m12 { get; set; }

        //Geodesic scale of point 2 relative to point 1 (dimensionless)
        public double M12 { get; set; }

        //Geodesic scale of point 1 relative to point 2 (dimensionless)
        public double M21 { get; set; }

        //Area in square meters under the geodesic
        public double S12 { get; set; }

        public GeodesicData() => lat1 = lon2 = azi1 = lat2 = lon2 = azi2 = s12 = a12 = m12 = M12 = M21 = S12 = Double.NaN;
    }
}