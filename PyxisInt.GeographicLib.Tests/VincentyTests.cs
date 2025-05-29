using System;
using Xunit;

namespace PyxisInt.GeographicLib.Tests;

public class VincentyTests
{
    [Fact]
    public void SydneyToLosAngelesComparisonTest()
    {
        // Sydney to Los Angeles - Southern to Northern hemisphere, across Pacific
        double syd_lat = -33.9399;
        double syd_lon = 151.1753;
        double lax_lat = 33.9416;
        double lax_lon = -118.4085;
        
        // Calculate using Vincenty
        VincentyResult vResult = Vincenty.Inverse(syd_lat, syd_lon, lax_lat, lax_lon);
        
        // Calculate using Geodesic
        GeodesicData gResult = Geodesic.WGS84.Inverse(syd_lat, syd_lon, lax_lat, lax_lon, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Distance - gResult.Distance) < 100); // Within 100 meters
        Assert.True(Math.Abs(vResult.InitialBearing - gResult.InitialAzimuth) < 0.1); // Within 0.1 degrees
    }
    
    [Fact]
    public void CapeTownToSingaporeComparisonTest()
    {
        // Cape Town to Singapore - Southern to Northern hemisphere, across Indian Ocean
        double cpt_lat = -33.9649;
        double cpt_lon = 18.6017;
        double sin_lat = 1.3644;
        double sin_lon = 103.9915;
        
        // Calculate using both methods
        VincentyResult vResult = Vincenty.Inverse(cpt_lat, cpt_lon, sin_lat, sin_lon);
        GeodesicData gResult = Geodesic.WGS84.Inverse(cpt_lat, cpt_lon, sin_lat, sin_lon, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Distance - gResult.Distance) < 100);
        Assert.True(Math.Abs(vResult.InitialBearing - gResult.InitialAzimuth) < 0.1);
        Assert.True(Math.Abs(vResult.FinalBearing - gResult.FinalAzimuth) < 0.1);
    }
    
    [Fact]
    public void BerlinToMoscowComparisonTest()
    {
        // Berlin to Moscow - Same hemisphere, shorter distance
        double ber_lat = 52.5548;
        double ber_lon = 13.2900;
        double svo_lat = 55.9736;
        double svo_lon = 37.4125;
        
        // Calculate using both methods
        VincentyResult vResult = Vincenty.Inverse(ber_lat, ber_lon, svo_lat, svo_lon);
        GeodesicData gResult = Geodesic.WGS84.Inverse(ber_lat, ber_lon, svo_lat, svo_lon, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Distance - gResult.Distance) < 50); // Higher precision for shorter distance
        Assert.True(Math.Abs(vResult.InitialBearing - gResult.InitialAzimuth) < 0.1);
    }
    
    [Fact]
    public void DirectCalculationTest()
    {
        // Starting in New York City
        double startLat = 40.7128;
        double startLon = -74.0060;
        double bearing = 65.0;     // Heading northeast
        double distance = 5000000; // 5000 km
        
        // Calculate endpoint using Vincenty Direct
        VincentyDirectResult vResult = Vincenty.Direct(startLat, startLon, bearing, distance);
        
        // Calculate using Geodesic Direct
        GeodesicData gResult = Geodesic.WGS84.Direct(startLat, startLon, bearing, distance, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Latitude - gResult.Latitude2) < 0.01);
        Assert.True(Math.Abs(vResult.Longitude - gResult.Longitude2) < 0.01);
        Assert.True(Math.Abs(vResult.FinalBearing - gResult.FinalAzimuth) < 0.1);
    }
    
    [Fact]
    public void EquatorialDirectCalculationTest()
    {
        // Starting near equator
        double startLat = 0.1;
        double startLon = 20.0;
        double bearing = 90.0;      // Due east
        double distance = 1000000;  // 1000 km
        
        // Calculate using both methods
        VincentyDirectResult vResult = Vincenty.Direct(startLat, startLon, bearing, distance);
        GeodesicData gResult = Geodesic.WGS84.Direct(startLat, startLon, bearing, distance, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Latitude - gResult.Latitude2) < 0.01);
        Assert.True(Math.Abs(vResult.Longitude - gResult.Longitude2) < 0.01);
    }
    
    [Fact]
    public void NearPoleDirectCalculationTest()
    {
        // High latitude test
        double startLat = 85.0;
        double startLon = 0.0;
        double bearing = 180.0;     // Due south
        double distance = 500000;   // 500 km
        
        // Calculate using both methods
        VincentyDirectResult vResult = Vincenty.Direct(startLat, startLon, bearing, distance);
        GeodesicData gResult = Geodesic.WGS84.Direct(startLat, startLon, bearing, distance, GeodesicMask.ALL);
        
        // Compare results
        Assert.True(Math.Abs(vResult.Latitude - gResult.Latitude2) < 0.01);
        Assert.True(Math.Abs(vResult.Longitude - gResult.Longitude2) < 0.01);
    }
    
    [Fact]
    public void RoundTripConsistencyTest()
    {
        // Start location
        double startLat = 33.6891;  // Phoenix, AZ
        double startLon = -112.0410;
        double bearing = 120.0;
        double distance = 3000000;  // 3000 km
        
        // Forward calculation
        VincentyDirectResult vDirectResult = Vincenty.Direct(startLat, startLon, bearing, distance);
        
        // Backward calculation
        VincentyResult vInverseResult = Vincenty.Inverse(startLat, startLon, 
                                                        vDirectResult.Latitude, 
                                                        vDirectResult.Longitude);
        
        // Check consistency
        Assert.True(Math.Abs(vInverseResult.Distance - distance) < 1);  // Within 1 meter
        Assert.True(Math.Abs(vInverseResult.InitialBearing - bearing) < 0.1);
    }
    
    [Fact]
    public void AntipodalPointsTest()
    {
        // Nearly antipodal points
        double lat1 = 40.0;
        double lon1 = 10.0;
        double lat2 = -39.9;
        double lon2 = -170.0;
        
        // Calculate using both methods
        VincentyResult vResult = Vincenty.Inverse(lat1, lon1, lat2, lon2);
        GeodesicData gResult = Geodesic.WGS84.Inverse(lat1, lon1, lat2, lon2, GeodesicMask.ALL);
        
        // For nearly antipodal points, we expect higher tolerance
        Assert.True(Math.Abs(vResult.Distance - gResult.Distance) < 1000); // Within 1 km
    }
}