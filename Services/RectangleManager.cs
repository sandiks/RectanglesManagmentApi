using RectanglesManagmentApi.Models;

namespace RectanglesManagmentApi.Services;

public class RectangleManager
{
    //top and left between 0..2000, width and neight betwwen 0..8000
    public IEnumerable<RectangleModel> GenerateRectangles(int count)
    {
        Random rand = new();
        return Enumerable.Range(1, count).Select(indx =>
        {
            var x = rand.Next(2000);
            var y = rand.Next(2000);
            var w = rand.Next(6000);
            var h = rand.Next(6000);
            return new RectangleModel() { From = new(x, y), To = new(x + w, y + h) };
        });
    }

    public IEnumerable<PointInRectangles> FilterRectanglesByCoordinates(IEnumerable<RectangleModel> rectangles, IEnumerable<Point2D> coords)
    {
        foreach (var coord in coords)
        {
            var rects = rectangles.Where(r => r.PointInRectangle(coord)).ToList();
            if (rects.Any())
                yield return new PointInRectangles()
                {
                    Point = coord,
                    Rectangles = rects
                };
        }

    }
}
