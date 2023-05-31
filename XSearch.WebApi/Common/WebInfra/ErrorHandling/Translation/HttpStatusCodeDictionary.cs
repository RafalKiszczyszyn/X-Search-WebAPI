

using System.Net;
using XSearch.WebApi.Common.Errors;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Translation
{
  /// <summary>
  /// Dictionary for matching <see cref="ErrorCode"/> into proper <see cref="HttpStatusCode"/>.
  /// </summary>
  internal class HttpStatusCodeDictionary : OverwritableDictionaryWithDefaultValue<ErrorCode, HttpStatusCode>, IHttpStatusCodeDictionary
  {
    /// <summary>
    /// Creates new instance of <see cref="HttpStatusCodeDictionary"/> class.
    /// </summary>
    public HttpStatusCodeDictionary() : base(HttpStatusCode.InternalServerError)
    {
      Add(ErrorCode.CombinedError, HttpStatusCode.BadRequest);
      Add(ErrorCode.GenericError, HttpStatusCode.InternalServerError);
      Add(ErrorCode.InsufficientRights, HttpStatusCode.Forbidden);
      Add(ErrorCode.InvalidRequest, HttpStatusCode.BadRequest);
      Add(ErrorCode.NotFound, HttpStatusCode.NotFound);
      Add(ErrorCode.ServerFailure, HttpStatusCode.InternalServerError);
      Add(ErrorCode.UserNameNotAvailable, HttpStatusCode.BadRequest);
      Add(ErrorCode.ValidationError, HttpStatusCode.BadRequest);
      Add(ErrorCode.WrongUrl, HttpStatusCode.BadRequest);
    }
  }
}
