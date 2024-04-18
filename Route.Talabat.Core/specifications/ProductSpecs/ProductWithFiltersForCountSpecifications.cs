using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Core.specifications.ProductSpecs
{
	public class ProductWithFiltersForCountSpecifications : BaseSpecifications<Product>
	{
        public ProductWithFiltersForCountSpecifications(ProductSpecificationsParams productParams):
            base(P =>
                    (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId) &&
                    (!productParams.CategoryId.HasValue || P.CategoryId == productParams.CategoryId)
                )
        {
            
        }
    }
}
