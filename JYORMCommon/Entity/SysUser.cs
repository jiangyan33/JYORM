
namespace JYORMCommon.Entity
{
    public class SysUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public string LastLoginTime { get; set; }
        public string UpdateTokenTime { get; set; }
        public string UserStatus { get; set; }
        public string UserType { get; set; }
    }
}
