using RectanglesManagmentApi.Data;
using RectanglesManagmentApi.Entities;
using RectanglesManagmentApi.Mappings;
using RectanglesManagmentApi.Models;
using RectanglesManagmentApi.Repositories;


namespace RectanglesManagmentApi.Services;

public class RectangleService : IRectangleService
{
    private readonly IDbService _dbService;
    private readonly IRectangleRepository _rectangleRepo;

    public RectangleService(IRectangleRepository rectangleRepo, IDbService dbService)
    {
        _rectangleRepo = rectangleRepo;
        _dbService = dbService;
    }

    public async Task<bool> AddRectangle(Rectangle rect)
    {
        var result =
            await _dbService.EditData(
                "INSERT INTO public.rectangle (x,y,xx,yy) VALUES (@x, @y, @xx, @yy)", rect);
        return true;
    }

    public Task BulkInsert(List<Rectangle> data) =>
        _dbService.BulkInsert(data);

    public async Task<List<Rectangle>> GetRectangles(int page = 0, int pageSize = 1000)
    {
        var data = await _dbService.GetAll<Rectangle>("SELECT * FROM public.rectangle LIMIT @pageSize OFFSET @page",
            new { page = (page - 1) * pageSize, pageSize });
        return data;
    }


    public async Task<Rectangle> GetRectangle(int id)
    {
        var data = await _dbService.GetAsync<Rectangle>("SELECT * FROM public.rectangle where id=@id", new { id });
        return data;
    }

    public IEnumerable<SimpleRectangleModel> GenerateSimpleRectangles(int count)
    {
        Random rand = new();
        return Enumerable.Range(1, count).Select(indx =>
        {
            var x = rand.Next(2000);
            var y = rand.Next(2000);
            var w = rand.Next(6000);
            var h = rand.Next(6000);
            return new SimpleRectangleModel() { From = new(x, y), To = new(x + w, y + h) };
        });
    }
    public IEnumerable<RectangleModel> GenerateRectangles(int count)
    {
        Random rand = new();
        return Enumerable.Range(1, count).Select(indx =>
        {
            var x = rand.NextDouble() * 2000;
            var y = rand.NextDouble() * 2000;
            var w = rand.NextDouble() * 6000;
            var h = rand.NextDouble() * 6000;
            var alpha = rand.NextDouble() * Math.PI / 2;
            return new RectangleModel() { From = new(x, y), width = w, high = h, alpha = alpha };
        });
    }

    public async Task<IEnumerable<PointInRectangles>> FilterRectanglesByCoordinates(IEnumerable<Point2D> coords)
    {
        var rectangles = (await GetRectangles()).Select(r => r.ToModel());
        List<PointInRectangles> result = new();
        foreach (var coord in coords)
        {
            var rects = rectangles.Where(r => r.PointInRectangle(coord)).ToList();
            if (rects.Any())
                result.Add(new PointInRectangles()
                {
                    Point = coord,
                    Rectangles = rects
                });
        }
        return result;
    }

}

public interface IRectangleService
{
    Task<bool> AddRectangle(Rectangle rect);
    Task BulkInsert(List<Rectangle> data);
    Task<List<Rectangle>> GetRectangles(int page = 0, int pageSize = 1000);
    IEnumerable<RectangleModel> GenerateRectangles(int count);
    IEnumerable<SimpleRectangleModel> GenerateSimpleRectangles(int count);
    Task<IEnumerable<PointInRectangles>> FilterRectanglesByCoordinates(IEnumerable<Point2D> coords);
    Task<Rectangle> GetRectangle(int id);
}
