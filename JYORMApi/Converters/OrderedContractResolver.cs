using System;
using System.Collections.Generic;
using System.Linq;

namespace JYORMApi.Converters
{
    public class OrderedContractResolver : Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 设置Newtonsoft序列化时的顺序，根据字母顺序进行排列,小驼峰
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            return properties.OrderBy(p => p.PropertyName).ToList();
        }
    }
}
