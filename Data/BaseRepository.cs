using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace RectanglesManagmentApi.Dapper
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository
    {
        protected BaseRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        public int BulkInsert<T>(IEnumerable<T> data, string tableToInsert, bool generateSchema = true) where T : class
        {
            int recordsInserted;

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = tableToInsert;

                    if (generateSchema)
                    {
                        foreach (var column in ColumnsToMap<T>(tableToInsert))
                        {
                            bulkCopy.ColumnMappings.Add(column, column);
                        }
                    }

                    connection.Open();

                    var bulkData = data.ToList();

                    using (var reader = new GenericListDataReader<T>(bulkData))
                    {
                        bulkCopy.WriteToServer(reader);
                    }

                    recordsInserted = bulkData.Count;
                }
            }

            return recordsInserted;
        }

        protected IEnumerable<string> ColumnsToMap<T>(string tableToInsert, string schema = "dbo", bool omitIdentityColumn = true) where T : class
        {
            var properties = typeof(T).GetProperties().Select(s => s.Name);

            return GetSchema(tableToInsert, schema, omitIdentityColumn).Intersect(properties, StringComparer.OrdinalIgnoreCase);
        }

        protected IEnumerable<string> GetSchema(string tableName, string schema, bool omitIdentityColumn = true)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (string.IsNullOrEmpty(schema))
                throw new ArgumentNullException(nameof(schema));

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SET NOCOUNT ON;
                        SELECT col.COLUMN_NAME AS[column_name],
                            COALESCE(COLUMNPROPERTY(object_id(tab.TABLE_SCHEMA+'.'+tab.TABLE_NAME), col.COLUMN_NAME, 'IsIdentity'), 0) AS [IsIdentity]
                        FROM INFORMATION_SCHEMA.TABLES tab
                          JOIN INFORMATION_SCHEMA.Columns col
                        ON tab.TABLE_NAME = col.TABLE_NAME
                            AND tab.TABLE_SCHEMA = col.TABLE_SCHEMA
                        WHERE tab.TABLE_NAME = @tableName
                          AND tab.TABLE_SCHEMA = @schema";

                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("@tableName", SqlDbType.NVarChar, 384).Value = tableName;
                    command.Parameters.Add("@schema", SqlDbType.NVarChar, 384).Value = schema;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if ((int)reader["IsIdentity"] == 1 && omitIdentityColumn)
                                continue;

                            yield return (string)reader["column_name"];
                        }
                    }
                }
            }
        }

        protected string ConnectionString { get; }
    }
}
