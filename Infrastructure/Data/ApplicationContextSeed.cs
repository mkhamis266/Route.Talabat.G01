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

			if (dbContext.ProductBrands.Count() == 0)
			{
				foreach (var brand in Brands)
				{
					dbContext.ProductBrands.Add(brand);
				}
			}

			if (dbContext.ProductCategories.Count() == 0)
			{
				foreach (var category in Categories)
				{
					dbContext.ProductCategories.Add(category);
				}
			}
			dbContext.SaveChangesAsync();			
		}
	}
}
