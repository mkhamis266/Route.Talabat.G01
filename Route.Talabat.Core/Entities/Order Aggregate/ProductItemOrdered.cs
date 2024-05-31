using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class ProductItemOrdered
	{
        private ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productId, string productName, string pictureURL)
		{
			ProductId = productId;
			ProductName = productName;
			PictureURL = pictureURL;
		}

		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public string PictureURL { get; set; } = null!;
	}
}
