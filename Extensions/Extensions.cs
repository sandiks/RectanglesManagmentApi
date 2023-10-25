using RectanglesManagmentApi.Models;

public static class RectangleExtensions
{

    public static bool PointInRectangle(this RectangleModel rect, Point2D point)
    {
        return point.x > rect.From.x && point.x < rect.To.x
        && point.y > rect.From.y && point.y < rect.To.y;
    }
}

public static class PointExtensions
{
    public static IEnumerable<Point2D> ListToPoints(this List<int> numbers) =>
        numbers.Chunk(2).Select(d => new Point2D(d[0], d[1]));

}
