using System;
using Xunit;
using PyxisInt.GeographicLib;

namespace PyxisInt.GeographicLib.Tests
{
    public class GeodesicTests
    {
        [Fact]
        public void Constructor_InvalidEquatorialRadius_ThrowsException()
        {
            // Arrange
            double invalidRadius = -1;
            double flattening = 0.1;

            // Act & Assert
            Assert.Throws<GeographicException>(() => new Geodesic(invalidRadius, flattening));
        }

        [Fact]
        public void Constructor_InvalidPolarSemiAxis_ThrowsException()
        {
            // Arrange
            double equatorialRadius = 6378137.0;
            double invalidFlattening = 2.0; // This will result in a negative polar semi-axis

            // Act & Assert
            Assert.Throws<GeographicException>(() => new Geodesic(equatorialRadius, invalidFlattening));
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange
            double equatorialRadius = 6378137.0;
            double flattening = 1 / 298.257223563;

            // Act
            var geodesic = new Geodesic(equatorialRadius, flattening);

            // Assert
            Assert.NotNull(geodesic);
            Assert.Equal(equatorialRadius, geodesic.GetEquatorialRadius());
            Assert.Equal(flattening, geodesic.GetEllipsoidFlattening());
        }

        [Fact]
        public void Direct_ValidInput_ReturnsCorrectResult()
        {
            // Arrange
            var geodesic = Geodesic.WGS84;
            double lat1 = 34.0;
            double lon1 = -118.0;
            double azi1 = 45.0;
            double s12 = 1000000.0;

            // Act
            var result = geodesic.Direct(lat1, lon1, azi1, s12);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lat1, result.Latitude1);
            Assert.Equal(lon1, result.Longitude1);
            Assert.Equal(azi1, result.InitialAzimuth);
            Assert.Equal(s12, result.Distance);
        }

        [Fact]
        public void ArcDirect_ValidInput_ReturnsCorrectResult()
        {
            // Arrange
            var geodesic = Geodesic.WGS84;
            double lat1 = 34.0;
            double lon1 = -118.0;
            double azi1 = 45.0;
            double a12 = 10.0;

            // Act
            var result = geodesic.ArcDirect(lat1, lon1, azi1, a12);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lat1, result.Latitude1);
            Assert.Equal(lon1, result.Longitude1);
            Assert.Equal(azi1, result.InitialAzimuth);
            Assert.Equal(a12, result.ArcLength);
        }
    }
}