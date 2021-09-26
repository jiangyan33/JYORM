using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCommonTool.Utils;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JYORMApi.Model;

namespace JYORMApi.Persistence
{
    /// <summary>
    /// 执行数据库相关操作,数据库日志相关的内容全部放到这里处理
    /// </summary>
    public class MySqlDB
    {
        private readonly ILogger<MySqlDB> _logger;

        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public DBOptions _dBOptions = new();

        public MySqlDB(ILogger<MySqlDB> logger, IConfiguration configuration)
        {
            configuration.GetSection("ConnectionOptions").Bind(_dBOptions);
            _dBOptions.DataBaseName = _dBOptions.ConnectionString.Split(';')[1].Split('=')[1];
            _logger = logger;
        }

        /// <summary>
        /// 获取数据库查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetEntityAsync<T>(string sql, params object[] param) where T : new()
        {
            List<T> result = null;
            var conn = await GetConnectionAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            InitParameters(cmd, param);
            cmd.CommandTimeout = _dBOptions.CommandTimeout;
            try
            {
                var dbReader = await cmd.ExecuteReaderAsync();

                result = await dbReader.SerializeToObject<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                await cmd.DisposeAsync();
                await conn.DisposeAsync();
            }
            return result;
        }

        /// <summary>
        /// 获取数据库查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(string sql, params object[] param)
        {
            bool result;
            var conn = await GetConnectionAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            InitParameters(cmd, param);
            cmd.CommandTimeout = _dBOptions.CommandTimeout;
            try
            {
                var dbReader = await cmd.ExecuteReaderAsync();
                result = dbReader.IsExist();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                await cmd.DisposeAsync();
                await conn.DisposeAsync();
            }
            return result;
        }

        /// <summary>
        /// 获取数据库查询分页结果集
        /// </summary>
        /// <param name="pageNum">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageBy">分页排序时用到的字段</param>
        /// <param name="isAsc">是否时升序</param>
        /// <param name="sql">sql语句模板</param>
        /// <param name="param">sql语句的参数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetDataTableAsync<T>(int pageNum, int pageSize, string pageBy, bool isAsc, string sql, params object[] param) where T : new()
        {
            if (pageNum == -1)
            {
                var res = await GetEntityAsync<T>(sql, param);
                return new PageResult<T> { Data = res };
            }
            if (pageNum <= 0)
                throw new ArgumentOutOfRangeException("页码必须是大于0的整数!");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("每页数量必须是大于0的整数!");

            PageResult<T> result = null;
            var conn = await GetConnectionAsync();

            var cmd = conn.CreateCommand();
            string order;
            if (string.IsNullOrEmpty(pageBy))
                order = " order by 1 ";
            else
                order = $" order by {pageBy} {(isAsc ? "asc" : "desc")},1 ";
            sql = $@"select SQL_CALC_FOUND_ROWS * from ({sql}) tmpSql
                             {order}
                             limit {(pageNum - 1) * pageSize},{pageSize};
                            select FOUND_ROWS() as totalRecords;";
            cmd.CommandText = sql;
            InitParameters(cmd, param);
            cmd.CommandTimeout = _dBOptions.CommandTimeout;
            try
            {
                var dbReader = await cmd.ExecuteReaderAsync();

                var (list, rows) = await dbReader.SerializeToPageObject<T>();
                result = new PageResult<T>(list, pageSize, rows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                await cmd.DisposeAsync();
                await conn.DisposeAsync();
            }
            return result;
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns></returns>
        public async Task<long> ExecuteAsync(string sql, params object[] param)
        {
            long result = 0;
            var conn = await GetConnectionAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = _dBOptions.CommandTimeout;
            InitParameters(cmd, param);
            try
            {
                var res = await cmd.ExecuteNonQueryAsync();
                result = sql.TrimStart().ToLower().StartsWith("insert") ? cmd.LastInsertedId : res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                await cmd.DisposeAsync();
                await conn.DisposeAsync();
            }
            return result;
        }

        /// <summary>
        /// 初始化SqlCommand的参数
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="param">参数数组</param>
        public static void InitParameters(MySqlCommand cmd, object[] param)
        {
            if (param == null || param.Length == 0)
                return;
            cmd.Parameters.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] is KeyValuePair<string, object> p)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
                else
                    cmd.Parameters.AddWithValue($"@{i + 1}", param[i]);
            }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        private async Task<MySqlConnection> GetConnectionAsync()
        {
            // 创建一个数据库连接
            _logger.LogTrace("创建一个数据库连接");
            var conn = new MySqlConnection(_dBOptions.ConnectionString);
            await conn.OpenAsync();

            return conn;
        }
    }
}