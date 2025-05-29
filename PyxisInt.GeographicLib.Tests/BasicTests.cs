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
        
        [Fact]
        public void SydneyToLAShouldPass()
        {
            // Sydney to Los Angeles - Southern to Northern hemisphere, across Pacific
            Pair syd = new Pair(-33.9399, 151.1753);
            Pair lax = new Pair(33.9416, -118.4085);
            GeodesicData g = Geodesic.WGS84.Inverse(syd.First, syd.Second, lax.First, lax.Second, GeodesicMask.ALL);
            Assert.NotNull(g);
        
            double expectedDistance = 12050.430; // in kilometers
            double actualDistance = g.Distance / 1000.0;
            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.001);
        }
        
        [Fact]
        public void CapeTownToSingaporeShouldPass()
        {
            // Cape Town to Singapore - Southern to Northern hemisphere, across Indian Ocean
            Pair cpt = new Pair(-33.9649, 18.6017);
            Pair sin = new Pair(1.3644, 103.9915);
            GeodesicData g = Geodesic.WGS84.Inverse(cpt.First, cpt.Second, sin.First, sin.Second, GeodesicMask.ALL);
            Assert.NotNull(g);
        
            double expectedDistance = 9671.904; // in kilometers
            double actualDistance = g.Distance / 1000.0;
            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.001);
        }
        
        [Fact]
        public void BerlinToMoscowShouldPass()
        {
            // Berlin to Moscow - Same hemisphere, relatively short distance
            Pair ber = new Pair(52.5548, 13.2900);
            Pair svo = new Pair(55.9736, 37.4125);
            GeodesicData g = Geodesic.WGS84.Inverse(ber.First, ber.Second, svo.First, svo.Second, GeodesicMask.ALL);
            Assert.NotNull(g);
        
            double expectedDistance = 1608.218; // in kilometers
            double actualDistance = g.Distance / 1000.0;
            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.001);
            
            double expectedInitialCourse = 66.742;
            double actualInitialCourse = g.InitialAzimuth;
            Assert.True(Math.Abs(actualInitialCourse - expectedInitialCourse) <= 0.01);
        }
        
        [Fact]
        public void SydneyToAucklandShouldPass()
        {
            // Sydney to Auckland - Both Southern hemisphere, shorter distance
            Pair syd = new Pair(-33.9399, 151.1753);
            Pair akl = new Pair(-36.0077, 174.7918);
            GeodesicData g = Geodesic.WGS84.Inverse(syd.First, syd.Second, akl.First, akl.Second, GeodesicMask.ALL);
            Assert.NotNull(g);
        
            double expectedDistance = 2163.430; // in kilometers
            double actualDistance = g.Distance / 1000.0;
            Assert.True(Math.Abs(actualDistance - expectedDistance) <= 0.001);
        }
    }
}
