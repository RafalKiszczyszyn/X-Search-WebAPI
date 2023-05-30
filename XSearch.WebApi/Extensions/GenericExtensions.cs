using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.WebApi.Extensions
{
  internal static class GenericExtensions
  {
    public static List<T> AsList<T>(this T value)
      => new List<T> { value };
  }
}
