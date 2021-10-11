using JYORMApi.Entity;
using JYORMApi.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Dao
{
    public class CommonMapper
    {
        private readonly DefaultDBClient Client;

        public CommonMapper(DefaultDBClient client)
        {
            Client = client;
        }

        public async Task<List<Columns>> GetTableCreateDesc()
        {
            var sql = @$"SELECT
	                            t.table_comment,
	                            c.table_name,
	                            c.column_name,
	                            c.ordinal_position,
	                            c.column_default,
	                            c.is_nullable,
	                            c.data_type,
	                            c.column_comment 
                            FROM
	                            INFORMATION_SCHEMA.TABLES t
	                            LEFT JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_SCHEMA = c.TABLE_SCHEMA 
	                            AND t.TABLE_NAME = c.TABLE_NAME 
                            WHERE
	                            t.TABLE_SCHEMA = @DatabaseName";
            var result = await Client.GetInstance.Ado.SqlQueryAsync<Columns>(sql, new { DatabaseName = Client.DBOption.Database });
            return result;
        }
    }
}
