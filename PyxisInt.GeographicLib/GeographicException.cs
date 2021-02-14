using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.GeographicLib
{
    public class GeographicException : Exception
    {
        public GeographicException()
            : base()
        {
        }


        public GeographicException(string message)
            : base(message)
        {
        }

        public GeographicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}