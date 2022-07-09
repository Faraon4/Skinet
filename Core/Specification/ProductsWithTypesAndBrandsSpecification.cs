using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {

        // Using this contrustor for getting the brand and the type in our request, not to see null
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        // Using this constructor for getting a specific product with id, and to display the Brand and the Type as well
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
              AddInclude(x => x.ProductType);
              AddInclude(x => x.ProductBrand);
        }
    }
}