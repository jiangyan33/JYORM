using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace JYORMApi.Entity
{
    /// <summary>
    /// 数据库表关系映射
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 作者名称
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码（加密后的数据）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// token刷新时间
        /// </summary>
        public DateTime UpdateTokenTime { get; set; }

        /// <summary>
        /// 状态 1 启用 -1弃用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
}