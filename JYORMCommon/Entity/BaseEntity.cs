﻿using JYORMCommon.Attr;
using System;

namespace JYORMCommon.Entity
{
    /// <summary>
    /// Entity基础类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
