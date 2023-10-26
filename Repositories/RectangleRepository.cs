using RectanglesManagmentApi.Dapper;
using RectanglesManagmentApi.Entities;

namespace RectanglesManagmentApi.Repositories;

public interface IRectangleRepository
{
    Task<IEnumerable<Rectangle>> GetAllRectangles();
    int BulkInsert(List<Rectangle> data);
}

internal sealed class RectangleRepository : DapperRepository, IRectangleRepository
{
    public RectangleRepository(IConfiguration configuration) : base(configuration.GetConnectionString("RectangleDb"))
    {
    }

    public async Task<IEnumerable<Rectangle>> GetAllRectangles()
    {
        return await GetListAsync<Rectangle>("SELECT * FROM public.rectangle");
    }

    public int BulkInsert(List<Rectangle> data)
    {
        return Insert(data, "rectangle", "public");
    }
}
