using System;
using System.Collections.Generic;
using System.Text;

namespace JYORMCommon.Entity
{
    /// <summary>
    /// 数据库表列信息
    /// </summary>

    public class Columns
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public string ColumnDefault { get; set; }

        /// <summary>
        /// 是否为空 NO
        /// </summary>
        public string IsNullable { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 列注释
        /// </summary>
        public string ColumnComment { get; set; }
    }
}
