using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 带有统计功能的List
    /// </summary>
    /// <typeparam name="T">可以实例化的类型</typeparam>
    public class PagingList<T> where T : new()
    {
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 分页总数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 总数据
        /// </summary>
        public int TotleCount { get; set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> PageList { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">分页数据</param>
        /// <param name="count">总量</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        public PagingList(List<T> items, int count, int pageIndex, int pageSize)
        {
            TotleCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageList = new List<T>();
            if (items != null)
                PageList.AddRange(items);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagingList()
        {
            PageList = new List<T>();
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagingList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagingList<T>(items, count, pageIndex, pageSize);
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagingList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return PagingList<T>.Create(items, count, pageIndex, pageSize);
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagingList<T> Create(IList<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return PagingList<T>.Create(items, count, pageIndex, pageSize);
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagingList<T> Create(List<T> source, int count, int pageIndex, int pageSize)
        {
            return new PagingList<T>(source, count, pageIndex, pageSize);
        }
    }
}
