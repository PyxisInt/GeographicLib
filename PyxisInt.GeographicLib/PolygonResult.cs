using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public class PolygonResult
    {
        /**
        * The number of vertices in the polygon
        **********************************************************************/
        public int num;
        /**
         * The perimeter of the polygon or the length of the polyline (meters).
         **********************************************************************/
        public double perimeter;
        /**
         * The area of the polygon (meters<sup>2</sup>).
         **********************************************************************/
        public double area;
        /**
         * Constructor
         * <p>
         * @param num the number of vertices in the polygon.
         * @param perimeter the perimeter of the polygon or the length of the
         *   polyline (meters).
         * @param area the area of the polygon (meters<sup>2</sup>).
         **********************************************************************/

        public PolygonResult(int num, double perimeter, double area)
        {
            this.num = num;
            this.perimeter = perimeter;
            this.area = area;
        }
    }
}