using RectanglesManagmentApi.Entities;
using RectanglesManagmentApi.Models;

namespace RectanglesManagmentApi.Mappings;

public static class RectangleMappings
{
    public static Rectangle ToEntity(this RectangleModel item) =>
    new()
    {
        x = item.From.X,
        y = item.From.Y,
        xx = item.To.X,
        yy = item.To.Y,
    };

    public static RectangleModel ToModel(this Rectangle entity) =>
    new()
    {
        From = new(entity.x, entity.y),
        To = new(entity.xx, entity.yy),
    };
}
