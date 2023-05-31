

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using XSearch.WebApi.Common.Errors;
using XSearch.WebApi.Common.WebInfra.ErrorHandling.Translation;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Filters
{
  internal class ExceptionFilter : IExceptionFilter
  {
    private readonly IHttpStatusCodeDictionary m_httpStatusCodeDictionary;

    public ExceptionFilter(
      IHttpStatusCodeDictionary httpStatusCodeDictionary)
    {
      m_httpStatusCodeDictionary = httpStatusCodeDictionary;
    }

    public void OnException(ExceptionContext context)
    {
      var error = Error.CreateFrom(context.Exception);
      context.Result = CreateErrorResponse(error, DateTime.Now);
    }

    private ObjectResult CreateErrorResponse(Error error, DateTime timestamp)
    {
      var errorDto = ErrorDto.CreateFromError(error);

      return new ObjectResult(error)
      {
        StatusCode = (int)m_httpStatusCodeDictionary[error.Code],
        Value = new ErrorResponseDto(errorDto, timestamp)
      };
    }
  }
}
