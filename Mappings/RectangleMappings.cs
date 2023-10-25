using RectanglesManagmentApi.Entities;
using RectanglesManagmentApi.Models;

namespace RectanglesManagmentApi.Mappings;

public static class RectangleMappings
{
    public static Rectangle ToEntity(this RectangleModel item) =>
    new()
    {
        X = item.From.x,
        Y = item.From.y,
        Xx = item.To.x,
        Yy = item.To.y,
    };

    public static RectangleModel ToModel(this Rectangle entity) =>
    new()
    {
        From = new(entity.X, entity.Y),
        To = new(entity.Xx, entity.Yy),
    };
}
