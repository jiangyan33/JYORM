using JYORMApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace JYORMApi.Converters
{
    /// <summary>
    ///  返回实体类中的属性根据字母进行排序
    ///  如果需要使用小驼峰，可以继承<see cref="CamelCasePropertyNamesContractResolver"/>
    /// </summary>
    public class OrderedContractResolver : DefaultContractResolver
    {
        private readonly List<string> FilterProperties = typeof(QueryParam).GetProperties().Select(x => x.Name).ToList();

        /// <summary>
        /// 设置Newtonsoft序列化时的顺序，根据字母顺序进行排列,
        /// 在实体类的返回结果中过滤掉<see cref="QueryParam"/>的属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).Where(x => !FilterProperties.Contains(x.PropertyName)).OrderBy(p => p.PropertyName).ToList();
        }
    }
}
