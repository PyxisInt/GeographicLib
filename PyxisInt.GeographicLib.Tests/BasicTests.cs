using System;
using Xunit;

namespace PyxisInt.GeographicLib.Tests
{
    public class BasicTests
    {
        [Fact]
        public void JFKToLHRShouldPass()
        {
            Pair jfk = new Pair(40.639801, -73.7789002);
            Pair lhr = new Pair(51.4706001, -0.461941);
            GeodesicData g = Geodesic.WGS84.Inverse(jfk.First, jfk.Second, lhr.First, lhr.Second, GeodesicMask.ALL);
            Assert.NotNull(g);

            double expectedDistance = 5554.539; //in kilometers
            double actualDistance = g.Distance / 1000.0;
            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.001);

            double expectedInitialCourse = 51.38;
            double actualInitialCourse = g.InitialAzimuth;
            Assert.True(Math.Abs(actualInitialCourse - expectedInitialCourse) <= 0.01);
        }

        [Fact]
        public void JFKToCANShouldPass()
        {
            Pair jfk = new Pair(40.639801, -73.7789002);
            Pair can = new Pair(23.3924007, 113.2990036);
            GeodesicData g = Geodesic.WGS84.Inverse(jfk.First, jfk.Second, can.First, can.Second, GeodesicMask.ALL);
            Assert.NotNull(g);

            double expectedDistance = 12877.8358;
            double actualDistance = g.Distance / 1000.0;

            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.0001);
        }
    }
}
