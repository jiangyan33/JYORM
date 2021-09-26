using SqlSugar;
using System;

namespace JYORMApi.Entity
{
    [SugarTable(nameof(SysUser))]
    public class SysUser : BaseEntity
    {
        [SugarColumn]
        public string UserName { get; set; }

        [SugarColumn]
        public string Password { get; set; }

        [SugarColumn]
        public string Telephone { get; set; }

        [SugarColumn]
        public string Email { get; set; }

        [SugarColumn]
        public string NickName { get; set; }

        [SugarColumn]
        public DateTime Birthday { get; set; }

        [SugarColumn]
        public string Sex { get; set; }

        [SugarColumn]
        public string Description { get; set; }

        [SugarColumn]
        public DateTime LastLoginTime { get; set; }

        [SugarColumn]
        public DateTime UpdateTokenTime { get; set; }

        [SugarColumn]
        public string UserStatus { get; set; }

        [SugarColumn]
        public string UserType { get; set; }
    }
}
