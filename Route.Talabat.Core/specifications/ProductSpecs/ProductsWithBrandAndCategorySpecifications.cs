using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Core.specifications.ProductSpecs
{
	public class ProductsWithBrandAndCategorySpecifications:BaseSpecifications<Product>
	{
        public ProductsWithBrandAndCategorySpecifications(ProductSpecificationsParams productParams)
            :base(P=>
                    (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId) && 
                    (!productParams.CategoryId.HasValue || P.CategoryId == productParams.CategoryId)    
                )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
					case "priceDesc":
                        AddOrderByDescending(P => P.Price);
						break;
                    default: 
                        AddOrderBy(P => P.Name);
                        break;
				}
			}

            ApplyPagination(productParams.PageSize*(productParams.PageIndex-1),productParams.PageSize);
        }

        public ProductsWithBrandAndCategorySpecifications(int id):base(P=>P.Id == id)
        {
			AddIncludes();
		}

        private void AddIncludes()
        {
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
    }
}
