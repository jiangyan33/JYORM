using Microsoft.Extensions.Configuration;
using MyCommonTool.Models;
using MyCommonTool.Utils;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace JYORMApi.Persistence
{
    public class DefaultDBClient
    {
        private readonly SqlSugarClient _client;

        /// <summary>
        /// 数据库配置信息
        /// </summary>
        public DBOption DBOption { get; private set; }

        public DefaultDBClient(IConfiguration Configuration)
        {
            var serivce = new ConfigureExternalServices
            {
                // 设置映射数据库表名,列名
                EntityNameService = (type, entity) => entity.DbTableName = type.Name.StrChange(),
                EntityService = (property, column) => column.DbColumnName = property.Name.StrChange()
            };

            // 实例化数据库操作对象
            var dbOptionList = new List<DBOption>();
            Configuration.GetSection("ConnectionOptions").Bind(dbOptionList);
            DBOption = dbOptionList[0];
            var connectionConfig = new ConnectionConfig
            {
                DbType = DbType.MySql,
                ConnectionString = DBOption.ToString(),
                IsAutoCloseConnection = true,
                ConfigureExternalServices = serivce
            };
//#if DEBUG
//            connectionConfig.AopEvents = new AopEvents
//            {
//                OnLogExecuting = (sql, p) =>
//                {
//                    System.Console.WriteLine(sql);
//                    System.Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));
//                }
//            };
//#endif
            _client = new SqlSugarClient(connectionConfig);
        }

        /// <summary>
        /// 获取SqlSugarClient实例
        /// </summary>
        public SqlSugarClient GetInstance => _client;
    }
}
