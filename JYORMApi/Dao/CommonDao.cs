using JYORMApi.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JYORMApi.Dao
{
    public class CommonDao
    {
       

        //public CommonDao(MySqlDB mysqlDB)
        //{
        //    _mysqlDB = mysqlDB;
        //}

        //public async Task<List<JYORMCommon.Entity.Columns>> GetTableCreateDesc()
        //{
        //    var sql = @$"SELECT
	       //                     t.table_comment,
	       //                     c.table_name,
	       //                     c.column_name,
	       //                     c.ordinal_position,
	       //                     c.column_default,
	       //                     c.is_nullable,
	       //                     c.data_type,
	       //                     column_comment 
        //                    FROM
	       //                     INFORMATION_SCHEMA.TABLES t
	       //                     LEFT JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_SCHEMA = c.TABLE_SCHEMA 
	       //                     AND t.TABLE_NAME = c.TABLE_NAME 
        //                    WHERE
	       //                     t.TABLE_SCHEMA = '{_mysqlDB._dBOptions.DataBaseName}';";
        //    return await _mysqlDB.GetEntityAsync<JYORMCommon.Entity.Columns>(sql);
        //}
    }
}
