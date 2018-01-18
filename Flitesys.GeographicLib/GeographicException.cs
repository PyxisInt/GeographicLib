using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.GeographicLib
{
    public class GeographicException : Exception
    {
        public GeographicException()
            : base()
        {
        }

        protected GeographicException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
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