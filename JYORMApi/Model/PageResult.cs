using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JYORMApi.Model
{
    /// <summary>
    /// 封装分页相关数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> where T : new()
    {
        private int _pageNum;

        private int _pageSize;

        private int _pages;

        private long _rows;

        /// <summary>
        /// 成功返回分页数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageSize"></param>
        /// <param name="rows"></param>
        public PageResult(List<T> data, int pageSize, long rows)
        {
            Data = data;
            PageSize = pageSize;
            Rows = rows;
        }

        /// <summary>
        /// 空参构造方法
        /// </summary>
        public PageResult()
        {
            Data = new List<T>();
            PageSize = 0;
            Rows = 0;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        [JsonIgnore]
        public int PageNum
        {
            get => _pageNum;

            set
            {
                if (value < 1)
                    _pageNum = 1;
                else if (value > _pages)
                    _pageNum = _pages;
                else
                    _pageNum = value;
            }
        }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        [JsonIgnore]
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (_pageSize <= 0)
                    _pageSize = 20;
                else
                    _pageSize = value;
                if (_rows > 0)
                    _pages = (int)Math.Ceiling(_rows / (double)_pageSize);
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages
        {
            get
            {
                if (_pages == 0)
                    return 1;
                else
                    return _pages;
            }
        }

        /// <summary>
        /// 总数据行数
        /// </summary>
        public long Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;

                _pages = (int)Math.Ceiling(_rows / (double)PageSize);
            }
        }
    }
}
