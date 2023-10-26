using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static Dapper.SqlMapper;

namespace RectanglesManagmentApi.Dapper
{
    [ExcludeFromCodeCoverage]
    public abstract class DapperRepository : BaseRepository
    {
        protected DapperRepository(string connectionStringName) : base(connectionStringName)
        {
        }

        protected int Execute(string procName, DynamicParameters p)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Execute(procName, p, commandType: CommandType.StoredProcedure);
        }

        protected async Task<int> ExecuteAsync(string procName, DynamicParameters p)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.ExecuteAsync(procName, p, commandType: CommandType.StoredProcedure);
        }

        protected int Execute(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Execute(sql, param);
        }

        protected async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction trans = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.ExecuteAsync(sql, param, trans);
        }

        protected int Insert<T>(IEnumerable<T> records, string tableName, string schema = "dbo", bool omitIdentityColumn = true) where T : class
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            var fields = new StringBuilder();
            var variables = new StringBuilder();

            foreach (var property in ColumnsToMap<T>(tableName, schema, omitIdentityColumn))
            {
                fields.Append($", [{property}] ");
                variables.Append($", @{property} ");
            }

            var insertQuery = $"INSERT INTO [{schema}].[{tableName}] ({fields.Remove(0, 2)}) VALUES ({variables.Remove(0, 2)})";

            using var connection = new SqlConnection(ConnectionString);
            return connection.Execute(insertQuery, records);
        }

        protected IEnumerable<dynamic> GetList(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query(sql, param).ToList();
        }

        protected async Task<IEnumerable<dynamic>> GetListAsync(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.QueryAsync(sql, param);
        }

        protected IEnumerable<T> GetList<T>(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query<T>(sql, param).ToList();
        }

        protected async Task<IEnumerable<T>> GetListAsync<T>(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.QueryAsync<T>(sql, param);
        }

        protected IEnumerable<TReturn> GetList<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> f, string splitOnFieldName, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query(sql, f, param, splitOn: splitOnFieldName).ToList();
        }

        protected async Task<IEnumerable<TReturn>> GetListAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> func, string splitOnFieldName, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.QueryAsync(sql, func, param, splitOn: splitOnFieldName);
        }

        protected async Task<IEnumerable<TReturn>> GetListAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> func, string splitOnFieldName, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.QueryAsync(sql, func, param, splitOn: splitOnFieldName);
        }

        protected IEnumerable<T> GetList<T>(string storedProcName, CommandType commandType, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query<T>(storedProcName, param, commandType: commandType).ToList();
        }

        protected async Task<IEnumerable<T>> GetListAsync<T>(string storedProcName, CommandType commandType, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return await connection.QueryAsync<T>(storedProcName, param, commandType: commandType);
        }

        protected dynamic GetSingleOrDefault(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query(sql, param).SingleOrDefault();
        }

        protected T GetSingleOrDefault<T>(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query<T>(sql, param).SingleOrDefault();
        }

        protected TReturn GetSingleOrDefault<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> f, string splitOnFieldName, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query(sql, f, param, splitOn: splitOnFieldName).SingleOrDefault();
        }

        protected async Task<T> GetSingleOrDefaultAsync<T>(string sql, object param = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var result = await connection.QueryAsync<T>(sql, param).ConfigureAwait(false);

            return result.SingleOrDefault();
        }

        public async Task<TResult> GetMultipleAsync<TResult>(string sql, object param, Func<GridReader, TResult> getMultipleObjects)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var reader = await connection.QueryMultipleAsync(sql, param).ConfigureAwait(false);

            return getMultipleObjects(reader);
        }
    }
}
