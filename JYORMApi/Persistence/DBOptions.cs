using System.Linq;

namespace JYORMApi.Persistence
{
    /// <summary>
    /// 数据库连接选项
    /// </summary>
    public class DBOptions
    {
        public DBOptions()
        {
        }

        public DBOptions(string connStr)
        {
            ConnectionString = connStr;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库执行语句时的超时时间(s)
        /// </summary>
        public int CommandTimeout { get; set; } = 1800;

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }
    }
}