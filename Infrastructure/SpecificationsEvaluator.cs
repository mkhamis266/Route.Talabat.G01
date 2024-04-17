using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.specifications;

namespace Route.Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<T> where T: BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specs) 
		{
			var query = inputQuery;

			if(specs.Criteria is not null)
				query = query.Where(specs.Criteria);
			
			if(specs.OrderBy is not null)
				query = query.OrderBy(specs.OrderBy);

			if (specs.OrderByDescending is not null)
				query = query.OrderByDescending(specs.OrderByDescending);

			query = specs.Includes.Aggregate(query,(curruntQuery,includeExpression)=> curruntQuery.Include(includeExpression));
			return query;
		}
	}
}
