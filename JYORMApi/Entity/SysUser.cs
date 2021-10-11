using SqlSugar;
using System;

namespace JYORMApi.Entity
{

    /// <summary>
    /// 用户信息
    /// </summary>
    [SugarTable(nameof(SysUser))]
    public class SysUser : BaseEntity
    {

        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn]
        public string UserName { get; set; }

        /// <summary>
        /// 密码:加密后的数据
        /// </summary>
        [SugarColumn]
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [SugarColumn]
        public string Telephone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn]
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn]
        public string NickName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别:男,女,未知
        /// </summary>
        [SugarColumn]
        public string Sex { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn]
        public string Description { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 刷新token时间，如果该值为null，表示需要重新登录
        /// </summary>
        [SugarColumn]
        public DateTime? UpdateTokenTime { get; set; }

        /// <summary>
        /// 状态:NORMAL 正常 STOP停用
        /// </summary>
        [SugarColumn]
        public string UserStatus { get; set; }

        /// <summary>
        /// 用户类型:ADMIN_USER后台用户，BOOK_USER图书端用户
        /// </summary>
        [SugarColumn]
        public string UserType { get; set; }

    }
}