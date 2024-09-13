using Xunit;

namespace PyxisInt.GeographicLib.Tests
{
    public class PolygonAreaTests
    {
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