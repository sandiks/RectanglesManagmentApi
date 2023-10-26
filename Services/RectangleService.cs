using System.Collections.Generic;
using System.Threading.Tasks;
using RectanglesManagmentApi.Data;
using RectanglesManagmentApi.Entities;
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
                "INSERT INTO public.rectangle (x,y,xx,yy) VALUES (@X, @Y, @Xx, @Yy)", rect);
        return true;
    }

    public Task BulkInsert(List<Rectangle> data) =>
        _dbService.BulkInsert(data);

    public async Task<List<Rectangle>> GetRectangles()
    {
        var data = await _dbService.GetAll<Rectangle>("SELECT * FROM public.rectangle", new { });
        return data;
    }


    public async Task<Rectangle> GetRectangle(int id)
    {
        var data = await _dbService.GetAsync<Rectangle>("SELECT * FROM public.rectangle where id=@id", new { id });
        return data;
    }

}

public interface IRectangleService
{
    Task<bool> AddRectangle(Rectangle rect);
    Task BulkInsert(List<Rectangle> data);
    Task<List<Rectangle>> GetRectangles();
    Task<Rectangle> GetRectangle(int id);
}
