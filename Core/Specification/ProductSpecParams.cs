using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specification
{

    // We will store here the parameters that we are using in the controller
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex {get;set;}= 1; // By default we want to return the very first page

        private int _pageSize = 6;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }




        public int? BrandId {get;set;}
        public int? TypeId {get; set;}

        public string Sort {get;set;}
    }
}