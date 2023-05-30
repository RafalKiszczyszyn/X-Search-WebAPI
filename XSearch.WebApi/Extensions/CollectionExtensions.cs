using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.WebApi.Extensions
{
  internal static class CollectionExtensions
  {
    public static bool IsUnique<T>(this IEnumerable<T> enumerable, IEqualityComparer<T>? equalityComparer = null)
    {
      var evaluated = enumerable.EnsureEvaluated();

      return equalityComparer != null
        ? evaluated.Distinct(equalityComparer).Count() == evaluated.Count
        : evaluated.Distinct().Count() == evaluated.Count;
    }

    public static IReadOnlyCollection<T> EnsureEvaluated<T>(this IEnumerable<T> enumerable)
    {
      return enumerable as IReadOnlyCollection<T> ?? enumerable.ToList();
    }

    public static void AddRange<T>(this IList<T> list, IEnumerable<T> elements)
    {
      foreach (var element in elements)
      {
        list.Add(element);
      }
    }
  }
}
