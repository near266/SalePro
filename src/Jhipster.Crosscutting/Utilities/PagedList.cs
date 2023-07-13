using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Jhipster.Service.Utilities
{
    public class PagedList<T> where T : class
    {
        public PagedList()
        {
            Data = new List<T>();
        }

        public PagedList(IEnumerable<T> _Data, int _TotalCount, int _Page, int _PageSize)
        {
            Data = _Data;
            TotalCount = _TotalCount;
            Page = _Page;
            PageSize = _PageSize;
        }
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
