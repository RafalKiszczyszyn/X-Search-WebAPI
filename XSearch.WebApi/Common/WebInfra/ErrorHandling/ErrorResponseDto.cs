

using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling
{
  /// <summary>
  /// HTTP error response
  /// </summary>
  [JsonSchema("ErrorResponse")]
  internal class ErrorResponseDto
  {
    /// <summary>
    /// Error that occured during HTTP call.
    /// </summary>
    [JsonProperty("error", Required = Required.Always)]
    public ErrorDto Error { get; }

    /// <summary>
    /// The property holds the datetime with time zone instance of when the error happened.
    /// Datetime is using the ISO 8601 notation (example: 2008-09-15T15:53:00+05:00).
    /// </summary>
    [JsonProperty("timestamp", Required = Required.Always)]
    public DateTime Timestamp { get; }

    /// <summary>
    /// Creates instance of <see cref="ErrorResponseDto"/>
    /// </summary>
    /// <param name="error">Error to return.</param>
    /// <param name="timestamp">
    /// The property holds the datetime with time zone instance of when the error happened.
    /// Datetime is using the ISO 8601 notation (example: 2008-09-15T15:53:00+05:00).
    /// </param>
    public ErrorResponseDto(ErrorDto error, DateTime? timestamp = null)
    {
      Error = error;
      Timestamp = timestamp ?? DateTime.Now;
    }
  }
}
