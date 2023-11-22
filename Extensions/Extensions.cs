using RectanglesManagmentApi.Models;

public static class RectangleExtensions
{

    public static bool PointInSimpleRectangle(this SimpleRectangleModel rect, Point2D point)
    {
        return point.X > rect.From.X && point.X < rect.To.X
        && point.Y > rect.From.Y && point.Y < rect.To.Y;
    }

    public static bool PointInRectangle(this RectangleModel rect, Point2D point)
    {
        return true;
    }
}

public static class PointExtensions
{
    public static IEnumerable<Point2D> ListToPoints(this List<int> numbers) =>
        numbers.Chunk(2).Select(d => new Point2D(d[0], d[1]));

}
