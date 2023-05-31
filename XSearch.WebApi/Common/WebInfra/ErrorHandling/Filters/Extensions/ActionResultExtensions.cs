

using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Filters.Extensions
{
  internal static class ActionResultExtensions
  {
    private const string c_statusCodePropertyName = "StatusCode";

    public static bool TryGetStatusCode(this IActionResult actionResult, out int? statusCode)
    {
      statusCode = actionResult.TryGetPropertyValue(
        c_statusCodePropertyName,
        defaultValue: (int?)null);

      return statusCode.HasValue;
    }
  }
}
