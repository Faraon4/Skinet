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
        public ProductsWithTypesAndBrandsSpecification( ProductSpecParams productParams) 
        : base(x => 
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
        (!productParams.TypeId.HasValue || x.ProductTypeId ==productParams.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            // Order function
            AddOrderBy(x =>x.Name);

            //Pagination
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            // condition to check value of the sort, and then apply correct order

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
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