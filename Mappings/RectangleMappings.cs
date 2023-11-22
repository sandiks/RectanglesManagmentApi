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
        width = item.width,
        hight = item.high,
        alpha = item.alpha,
    };

    public static SimpleRectangle ToEntity(this SimpleRectangleModel item) =>
    new()
    {
        x = item.From.X,
        y = item.From.Y,
        xx = item.To.X,
        yy = item.To.Y,
    };

    public static SimpleRectangleModel ToModel(this SimpleRectangle entity) =>
    new()
    {
        From = new(entity.x, entity.y),
        To = new(entity.xx, entity.yy),
    };

    public static RectangleModel ToModel(this Rectangle entity) =>
    new()
    {
        From = new(entity.x, entity.y),
        width = entity.width,
        high = entity.hight,
        alpha = entity.alpha,
    };
}
