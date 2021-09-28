using JYORMApi.Model;
using SqlSugar;
using System;

namespace JYORMApi.Entity
{
    /// <summary>
    /// Entity基础类
    /// </summary>
    public class BaseEntity: QueryParam
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public long CreateBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn]
        public long UpdateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
