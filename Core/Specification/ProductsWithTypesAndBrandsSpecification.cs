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
       
       // add the base, because we need to use as where keyword, but where it is used in the BaseSpecification class
        public ProductsWithTypesAndBrandsSpecification(string sort, int?brnadId, int? typeId) 
        : base(x => 
        (!brnadId.HasValue || x.ProductBrandId == brnadId) && 
        (!typeId.HasValue || x.ProductTypeId ==typeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            // Order function
            AddOrderBy(x =>x.Name);

            // condition to check value of the sort, and then apply correct order

            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                            AddOrderBy(p => p.Price);
                            break;
                    case "priceDesc":
                            AddOrderByDescending(p => p.Price);
                            break;
                    default:
                            AddOrderBy(n => n.Name);
                            break;
                }
            }
        }

        // Using this constructor for getting a specific product with id, and to display the Brand and the Type as well
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
              AddInclude(x => x.ProductType);
              AddInclude(x => x.ProductBrand);
        }
    }
}