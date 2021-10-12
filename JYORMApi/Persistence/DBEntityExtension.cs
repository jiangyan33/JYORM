using System.Text;
using System.Text.RegularExpressions;

namespace JYORMApi.Persistence
{
    public static class DBEntityExtension
    {
        /// <summary>
        /// 字符串大驼峰和下划线相互转换
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string StrChange(this string src)
        {
            if (string.IsNullOrEmpty(src)) return "";

            var regex = new Regex("[A-Z]");
            var charStr = '_';
            if (regex.IsMatch(src[0].ToString()))
            {
                // 大驼峰
                var stringBuilder = new StringBuilder();
                foreach (var item in src)
                {
                    if (regex.IsMatch(item.ToString()))
                    {
                        stringBuilder.Append(charStr);
                        stringBuilder.Append(item.ToString().ToLower());
                    }
                    else
                    {
                        stringBuilder.Append(item);
                    }
                }
                return stringBuilder.ToString().TrimStart(charStr);
            }
            else
            {
                // 下划线
                var strArr = src.Split(charStr);
                var stringBuilder = new StringBuilder();
                foreach (var item in strArr)
                {
                    for (var i = 0; i < item.Length; i++)
                    {
                        stringBuilder.Append(i == 0 ? item[i].ToString().ToUpper() : item[i].ToString());
                    }
                }
                return stringBuilder.ToString().TrimStart(charStr);
            }
        }

        public static string TypeChange(this string src, bool isNull = false)
        {
            var result = src switch
            {
                "varchar" => "string",
                "datetime" => "DateTime",
                "decimal" => "decimal",
                "int" or "tinyint" => "int",
                "bigint" => "long",
                _ => throw new System.Exception("类型匹配失败，未知类型，请联系管理员"),
            };
            if (isNull && result != "string") result += "?";
            return result;
        }
    }
}
