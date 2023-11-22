namespace RectanglesManagmentApi.Models;

public record Point2D(double X, double Y);
public record Vector2D(double v1, double v2);

public class SimpleRectangleModel
{
    public Point2D From { get; set; }
    public Point2D To { get; set; }
}

public class RectangleModel
{
    public Point2D From { get; set; }
    public double width { get; set; }
    public double high { get; set; }
    public double alpha { get; set; }
}

public class ABCRectangleModel
{
    public Point2D A { get; set; }
    public Point2D B { get; set; }
    public Point2D C { get; set; }
}

public class PointInRectangles
{
    public Point2D Point { get; set; }
    public List<RectangleModel> Rectangles { get; set; }
}
