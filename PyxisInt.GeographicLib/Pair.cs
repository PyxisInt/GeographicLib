using System;

namespace PyxisInt.GeographicLib
{
    /// <summary>
    /// A pair of double precision numbers
    /// </summary>
    public class Pair
    {
        public double First { get; set; }
        public double Second { get; set; }

        public Pair(double first, double second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}