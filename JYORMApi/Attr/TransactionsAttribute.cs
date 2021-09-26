using System;

namespace JYORMApi.Attr
{
    /// <summary>
    /// 声明式事务特性，仅仅作为事务声明
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionsAttribute : Attribute
    {
        /// <summary>
        /// 声明式事务特性的构造方法
        /// </summary>
        /// <param name="transactions">为true时启用事务</param>
        public TransactionsAttribute(bool transactions = false)
        {
            IsTransactions = transactions;
        }

        public bool IsTransactions { get; }
    }
}