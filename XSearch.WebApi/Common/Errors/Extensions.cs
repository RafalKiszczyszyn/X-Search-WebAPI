using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.WebApi.Common.Errors
{
  internal static class Extensions
  {
    public static string Indent(this string original)
    {
      var lines = original
        .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
        .Select(line => $"\t{line}");
      return string.Join(Environment.NewLine, lines);
    }

    public static bool IsNotNullOrWhitespace(this string? stringToCheck)
      => !string.IsNullOrWhiteSpace(stringToCheck);
  }
}
