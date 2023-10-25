namespace RectanglesManagmentApi.Models;

public record Point2D(int x, int y);

public class RectangleModel
{
    public Point2D From { get; set; }
    public Point2D To { get; set; }
}
public class PointInRectangles
{
    public Point2D Point { get; set; }
    public List<RectangleModel> Rectangles { get; set; }
}
