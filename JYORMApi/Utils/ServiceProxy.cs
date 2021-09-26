using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using JYORMApi.Attr;

namespace JYORMApi.Utils
{
    /// <summary>
    /// Service层代理对象
    /// </summary>
    public class ServiceProxy : DispatchProxy
    {
        public object TargetInstance { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var parametersTypes = targetMethod.GetParameters().Select(x => x.ParameterType).ToArray();
            // 获取方法时指定名称和方法参数类型
            var runtimeMethodInfo = TargetInstance.GetType().GetMethod(targetMethod.Name, parametersTypes);
            var attr = runtimeMethodInfo.GetCustomAttributes().Where(it => it is TransactionsAttribute).Select(x => (x as TransactionsAttribute).IsTransactions).ToList();
            // 不包含TransactionsAttribute特性，不做任何处理
            if (attr.Count == 0 || !attr[0]) return targetMethod.Invoke(TargetInstance, args);
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = targetMethod.Invoke(TargetInstance, args) as Task;
            result.GetAwaiter().GetResult();
            scope.Complete();
            return result;
        }
    }
}