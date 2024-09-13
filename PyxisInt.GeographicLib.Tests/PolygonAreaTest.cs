using System;
using Xunit;
using PyxisInt.GeographicLib;

namespace PyxisInt.GeographicLib.Tests
{
    public class PolygonAreaTests : IDisposable
    {
        private Geodesic _earth;
        private PolygonArea _polygonArea;

        public PolygonAreaTests()
        {
            _earth = Geodesic.WGS84;
            _polygonArea = new PolygonArea(_earth, false);
        }

        public void Dispose()
        {
            // Cleanup if needed
        }

        [Fact]
        public void TestAddPoint()
        {
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddPoint(0, 1);
            _polygonArea.AddPoint(1, 1);
            _polygonArea.AddPoint(1, 0);

            PolygonResult result = _polygonArea.Compute();

            Assert.Equal(4, result.num);
            Assert.True(result.perimeter > 0);
            Assert.True(result.area > 0);
        }

        [Fact]
        public void TestAddEdge()
        {
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddEdge(90, 100000); // Move east 100 km
            _polygonArea.AddEdge(0, 100000);  // Move north 100 km
            _polygonArea.AddEdge(-90, 100000); // Move west 100 km
            _polygonArea.AddEdge(180, 100000); // Move south 100 km

            PolygonResult result = _polygonArea.Compute();

            Assert.Equal(5, result.num);
            Assert.True(result.perimeter > 0);
            Assert.True(result.area < 0);   //we are going counter-clockwise, so area is negative
        }

        [Fact]
        public void TestClear()
        {
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddPoint(0, 1);
            _polygonArea.Clear();

            PolygonResult result = _polygonArea.Compute();

            Assert.Equal(0, result.num);
            Assert.Equal(0, result.perimeter);
            Assert.Equal(0, result.area);
        }

        [Fact]
        public void TestComputeWithReverse()
        {
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddPoint(0, 1);
            _polygonArea.AddPoint(1, 1);
            _polygonArea.AddPoint(1, 0);

            PolygonResult result = _polygonArea.Compute(true, true);

            Assert.Equal(4, result.num);
            Assert.True(result.perimeter > 0);
            Assert.True(result.area < 0); // Area should be negative when reversed
        }

        [Fact]
        public void TestPolyline()
        {
            _polygonArea = new PolygonArea(_earth, true);
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddPoint(0, 1);
            _polygonArea.AddPoint(1, 1);
            _polygonArea.AddPoint(1, 0);

            PolygonResult result = _polygonArea.Compute();

            Assert.Equal(4, result.num);
            Assert.True(result.perimeter > 0);
            Assert.True(double.IsNaN(result.area)); // Area should be NaN for polyline
        }

        [Fact]
        public void TestTestPoint()
        {
            _polygonArea.AddPoint(0, 0);
            _polygonArea.AddPoint(0, 1);
            _polygonArea.AddPoint(1, 1);

            PolygonResult result = _polygonArea.TestPoint(1, 0, false, true);

            Assert.Equal(4, result.num);
            Assert.True(result.perimeter > 0);
            Assert.True(result.area > 0);
        }
        
        [Fact]
        public void TestPolygonArea()
        {
            var polygonArea = new PolygonArea(Geodesic.WGS84, false);
            polygonArea.AddPoint(0,0);
            polygonArea.AddPoint(0, 1);
            polygonArea.AddPoint(1,1);
            polygonArea.AddPoint(1,0);

            PolygonResult result = polygonArea.Compute();
            Assert.NotNull(result);
            
        }
    }
}