using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Infrastructure.Data
{
	public static class ApplicationContextSeed
	{
		public static async Task SeedData(ApplicationDbContext dbContext) 
		{
			var BrandsJson = File.ReadAllText("../Infrastructure/Data/DataSeed/brands.json");
			var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsJson);

			var CatigoriesJson = File.ReadAllText("../Infrastructure/Data/DataSeed/categories.json");
			var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CatigoriesJson);

			var ProductsJson = File.ReadAllText("../Infrastructure/Data/DataSeed/products.json");
			var Products = JsonSerializer.Deserialize<List<Product>>(ProductsJson);

			if (Brands is not null && dbContext.ProductBrands.Count() == 0)
			{
				foreach (var brand in Brands)
				{
					dbContext.ProductBrands.Add(brand);
				}
			}

			if (Categories is not null && dbContext.ProductCategories.Count() == 0)
			{
				foreach (var category in Categories)
				{
					dbContext.ProductCategories.Add(category);
				}
			}

			if (Products is not null && dbContext.Products.Count()== 0)
			{
				foreach (var product in Products)
				{
					dbContext.Products.Add(product);
				}
			}

			await dbContext.SaveChangesAsync();			
		}
	}
}
