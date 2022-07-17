using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{

    // we can pagination everything
    public class Pagination<T> where T: class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex {get;set;}

        public int PageSize { get; set; }
        public int Count { get; set; } //  we want the count after the filters have been applied
        // so for example if somebody want boots and the page size to be 2 , then we want to know how many boots are available in the entire collection
        public IReadOnlyList<T> Data {get;set;}
    }
}