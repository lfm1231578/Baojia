using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HS.SupportComponents
{
    public static class SortHelper
    {
        public static IEnumerable<T> DataSorting<T>(this IEnumerable<T> source, string sortProperty, string sortDirection)
        {
            return source.AsQueryable().DataSorting(sortProperty, sortDirection).AsEnumerable();
        }

        public static IQueryable<T> DataSorting<T>(this IQueryable<T> source, string sortProperty, string sortDirection)
        {
            if (string.IsNullOrEmpty(sortProperty))
            {
                return source;
            }
            string sortingDir = string.Empty;
            if (sortDirection.ToUpper().Trim() == "ASC")
                sortingDir = "OrderBy";
            else if (sortDirection.ToUpper().Trim() == "DESC")
                sortingDir = "OrderByDescending";

            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            MemberExpression sortPropertyExpression = GetSortPropertyExpression<T>(param, sortProperty);// Expression.Property(Expression.Property(param, typeof(T).GetProperty("Child")), "A");
            var lambda = Expression.Lambda(sortPropertyExpression, param);//p=>p.Child.A

            Type[] paramsTypes = new Type[2];
            paramsTypes[0] = typeof(T);//输入参数类型
            paramsTypes[1] = sortPropertyExpression.Type;//返回参数类型

            Expression sortExpression = Expression.Call(typeof(Queryable), sortingDir, paramsTypes, source.Expression, lambda);
            IQueryable<T> query = source.Provider.CreateQuery<T>(sortExpression);
            return query;
        }

        private static MemberExpression GetSortPropertyExpression<T>(ParameterExpression param, string sortExpression)
        {
            Expression result = param;
            string[] propertys = sortExpression.Split('.');
            foreach (string property in propertys)
            {
                result = Expression.Property(result, property);
            }
            return result as MemberExpression;
        }
    }
}
