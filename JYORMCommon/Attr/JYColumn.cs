using System;

namespace JYORMCommon.Attr
{
    /// <summary>
    /// 非空特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class JYNotNullAttribute : Attribute
    {
        public bool IsNotNull => this == null;
    }
}
