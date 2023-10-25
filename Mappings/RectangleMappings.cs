using RectanglesManagmentApi.Entities;
using RectanglesManagmentApi.Models;

namespace RectanglesManagmentApi.Mappings;

public static class RectangleMappings
{
    public static Rectangle ToEntity(this RectangleModel item) =>
    new()
    {
        X = item.From.X,
        Y = item.From.Y,
        Xx = item.To.X,
        Yy = item.To.Y,
    };

    public static RectangleModel ToModel(this Rectangle entity) =>
    new()
    {
        From = new(entity.X, entity.Y),
        To = new(entity.Xx, entity.Yy),
    };
}
