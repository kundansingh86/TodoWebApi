using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace ToDo.Infra.Helpers
{
    public class SortHelper<T>
    {
        public static IQueryable<T> ApplySort(IQueryable<T> source, string? orderBy)
        {
            if (!source.Any())
                return source;

            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            var orderParams = orderBy.Trim().Split(',');
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];

                var objectProperty = propertyInfo.FirstOrDefault(p => p.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null) continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source;

            return source.OrderBy(orderQuery);

        }
    }
}
