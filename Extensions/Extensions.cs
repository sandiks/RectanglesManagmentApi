using System.Numerics;
using RectanglesManagmentApi.Mappings;
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
        var abcRect = rect.ToABCRectangleModel();
        Vector2D ab = Vector(abcRect.A, abcRect.B);
        Vector2D am = Vector(abcRect.A, point);
        Vector2D bc = Vector(abcRect.B, abcRect.C);
        Vector2D bm = Vector(abcRect.B, point);

        return Dot(ab, am) >= 0 && Dot(ab, am) <= Dot(ab, ab) &&
            Dot(bc, bm) >= 0 && Dot(bc, bm) <= Dot(bc, bc);
    }

    static double Dot(Vector2D v, Vector2D u) => u.v1 * v.v1 + u.v2 * v.v2;

    static Vector2D Vector(Point2D a, Point2D b) => new(b.X - a.X, b.Y - a.Y);
}

public static class PointExtensions
{
    public static IEnumerable<Point2D> ListToPoints(this List<int> numbers) =>
        numbers.Chunk(2).Select(d => new Point2D(d[0], d[1]));

}
