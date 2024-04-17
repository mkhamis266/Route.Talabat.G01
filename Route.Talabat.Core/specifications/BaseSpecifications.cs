﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Core.specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; } = null; // default
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

		public Expression<Func<T, object>> OrderBy { get; set; } = null;
		public Expression<Func<T, object>> OrderByDescending { get; set; } = null;

		public BaseSpecifications()
        {
			//Criteria = null
		}

        public BaseSpecifications(Expression<Func<T, bool>> critrtiaExpression)
        {
            Criteria = critrtiaExpression;
        }

		public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}

		public void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
		{
			OrderByDescending = orderByDescendingExpression;
		}
	}
}
