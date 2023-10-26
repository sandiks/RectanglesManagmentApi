
using Dapper.Contrib.Extensions;

namespace RectanglesManagmentApi.Entities;

[Table("rectangle")]
public class Rectangle
{

    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int xx { get; set; }
    public int yy { get; set; }
    public DateTime created { get; set; }
}
