using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Recipe.Service.Data.Extensions
{
    public static class EntityFrameworkQueryExtenders
    {
        public static IQueryable<T> AddIncludedProperties<T>(this IQueryable<T> query, Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null && includes.Any())
            {
              query = includes.Aggregate(query, (current, include) =>
              {
                  //Allows for Includes on Many to Many where a strongly typed navigation isn't possible
                  if (include.Body.Type == typeof(string))
                  {
                      return current.Include(include.Body.ToString().Trim('"'));
                  }
                  return current.Include(include);
              });
            }

            return query;
        }
    }
}
