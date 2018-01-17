using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.GeographicLib
{
    public class Accumulator
    {
        private double _s, _t;

        public Accumulator(double y)
        {
            _s = y;
            _t = 0;
        }

        public Accumulator(Accumulator a)
        {
            _s = a._s;
            _t = a._t;
        }

        public void Set(double y)
        {
            _s = y; _t = 0;
        }

        public double Sum()
        {
            return _s;
        }

        public double Sum(double y)
        {
            Accumulator a = new Accumulator(this);
            a.Add(y);
            return a._s;
        }

        public void Add(double y)
        {
            double u;
            { Pair r = GeoMath.Sum(y, _t); y = r.first; u = r.second; }
            { Pair r = GeoMath.Sum(y, _s); _s = r.first; _t = r.second; }
            if (_s == 0)              // This implies t == 0,
                _s = u;                 // so result is u
            else
                _t += u;                // otherwise just accumulate u to t.
        }

        public void Negate()
        {
            _s = -_s; _t = -_t;
        }
    }
}