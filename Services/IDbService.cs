namespace RectanglesManagmentApi.Services;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object parms);
    Task<List<T>> GetAll<T>(string command, object parms);
    Task BulkInsert<T>(List<T> data);
    Task<int> EditData(string command, object parms);
}
