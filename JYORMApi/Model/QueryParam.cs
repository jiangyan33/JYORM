using SqlSugar;

namespace JYORMApi.Model
{
    public class QueryParam
    {
        #region 通用查询参数

        /// <summary>
        /// 搜索
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Search { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrderBy { get; set; }

        /// <summary>
        /// 排序方式是否为升序
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public OrderByType OrderByType { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int PageNum { get; set; }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int PageSize { get; set; }

        #endregion 通用查询参数
    }
}
