using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities.Identity;

namespace Route.Talabat.Infrastructure.Identity
{
	public class ApplicationIdentityDbContext:IdentityDbContext<ApplicationUser>
	{
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options):base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Address>().ToTable("Adresses");
		}
	}
}
