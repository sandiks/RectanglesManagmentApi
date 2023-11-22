
using Dapper.Contrib.Extensions;

namespace RectanglesManagmentApi.Entities;

[Table("simple_rectangle")]
public class SimpleRectangle
{

    public int id { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public double xx { get; set; }
    public double yy { get; set; }
    public DateTime created { get; set; }
}

[Table("rectangle")]
public class Rectangle
{

    public int id { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public double width { get; set; }
    public double hight { get; set; }
    public double alpha { get; set; }
    public DateTime created { get; set; }
}
