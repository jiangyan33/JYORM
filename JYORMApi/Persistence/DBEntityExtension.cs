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
    }
}
