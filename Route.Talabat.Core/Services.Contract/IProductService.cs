using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.specifications.ProductSpecs;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecificationsParams productParams);
		Task<Product?> GetProductAsync(int id);
		Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
		Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
		Task<int> GetCountAsync(ProductSpecificationsParams productParams);
	}
}
