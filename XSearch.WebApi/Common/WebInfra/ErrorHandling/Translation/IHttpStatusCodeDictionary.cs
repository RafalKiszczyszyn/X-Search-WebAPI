

using System.Net;
using XSearch.WebApi.Common.Errors;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Translation
{
  /// <summary>
  /// Translates <see cref="ErrorCode"/> to <see cref="HttpStatusCode"/>.
  /// </summary>
  internal interface IHttpStatusCodeDictionary
  {
    /// <summary>
    /// Gets <see cref="HttpStatusCode"/> for given <see cref="ErrorCode"/>.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    HttpStatusCode this[ErrorCode errorCode] { get; }
  }
}
