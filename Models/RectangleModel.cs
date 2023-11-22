namespace RectanglesManagmentApi.Models;

public record Point2D(double X, double Y);

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

public class PointInRectangles
{
    public Point2D Point { get; set; }
    public List<RectangleModel> Rectangles { get; set; }
}
