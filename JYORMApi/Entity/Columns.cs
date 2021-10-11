using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace JYORMApi.Entity
{
    /// <summary>
    /// 数据库表列信息
    /// </summary>

    public class Columns
    {
        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn]
        public string TableName { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        [SugarColumn] 
        public string TableComment { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        [SugarColumn] 
        public string ColumnName { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [SugarColumn] 
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        [SugarColumn] 
        public string ColumnDefault { get; set; }

        /// <summary>
        /// 是否为空 NO
        /// </summary>
        [SugarColumn] 
        public string IsNullable { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        [SugarColumn] 
        public string DataType { get; set; }

        /// <summary>
        /// 列注释
        /// </summary>
        [SugarColumn] 
        public string ColumnComment { get; set; }
    }
}
