using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.Infrastructure.Data.Config.Order_Configs
{
	internal class DelivreyMethodConfigs : IEntityTypeConfiguration<DelivreyMethod>
	{
		public void Configure(EntityTypeBuilder<DelivreyMethod> builder)
		{
			builder.Property(deliveryMethod => deliveryMethod.Cost).HasColumnType("decimal(12,2)");
		}
	}
}
