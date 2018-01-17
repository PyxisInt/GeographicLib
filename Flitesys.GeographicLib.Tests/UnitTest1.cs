using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flitesys.GeographicLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestJFKToLHR()
        {
            Pair jfk = new Pair(40.639801, -73.7789002);
            Pair lhr = new Pair(51.4706001, -0.461941);
            GeodesicData g = Geodesic.WGS84.Inverse(jfk.first, jfk.second, lhr.first, lhr.second, GeodesicMask.ALL);
            Assert.IsNotNull(g);
            double expectedDistance = 5554.539; //in kilometers
            double actualDistance = g.s12 / 1000.0;
            Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) <= 0.001);

            double expectedInitialCourse = 51.38;
            double actualInitialCourse = g.azi1;
            Assert.IsTrue(Math.Abs(actualInitialCourse - expectedInitialCourse) <= 0.01);
        }

        /// <summary>
        /// This tests two anti-podal or near anti-podal points
        /// </summary>
        [TestMethod]
        public void TestJFKToCAN()
        {
            Pair jfk = new Pair(40.639801, -73.7789002);
            Pair can = new Pair(23.3924007, 113.2990036);
            GeodesicData g = Geodesic.WGS84.Inverse(jfk.first, jfk.second, can.first, can.second, GeodesicMask.ALL);
            Assert.IsNotNull(g);

            double expectedDistance = 12877.8358;
            double actualDistance = g.s12 / 1000.0;

            Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) <= 0.0001);
        }
    }
}