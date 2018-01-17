using System;

namespace Flitesys.GeographicLib
{
    /// <summary>
    /// A pair of double precision numbers
    /// </summary>
    public class Pair
    {
        public double first { get; set; }
        public double second { get; set; }

        public Pair(double first, double second)
        {
            this.first = first;
            this.second = second;
        }
    }
}