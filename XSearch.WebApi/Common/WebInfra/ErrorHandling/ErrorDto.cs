

using Newtonsoft.Json;
using NJsonSchema.Annotations;
using XSearch.WebApi.Common.Errors;
using XSearch.WebApi.Common.WebInfra.ErrorHandling.Codes;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling
{
  /// <summary>
  /// The representation of the error that occured during API request execution.
  /// </summary>
  // [JsonSchemaProcessor(typeof(ErrorSchemaProcessor))]
  [JsonSchema("Error")]
  internal class ErrorDto
  {
    /// <summary>
    /// One of a server-defined set of error codes.
    /// </summary>
    [JsonProperty("code", Required = Required.Always)]
    [JsonConverter(typeof(ErrorCodeJsonConverter))]
    public ErrorCode Code { get; }

    /// <summary>
    /// A human-readable representation of the error.
    /// </summary>
    [JsonProperty("message", Required = Required.Always)]
    public string Message { get; }

    /// <summary>
    /// The target of the error.
    /// </summary>
    [JsonProperty("target", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Target { get; }

    /// <summary>
    /// An array of details about specific errors that led to this reported error.
    /// </summary>
    [JsonProperty("details", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<ErrorDto>? Details { get; }

    /// <summary>
    /// An object containing more specific information than the current object about the error.
    /// </summary>
    [JsonProperty("innerError", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ErrorDto? InnerError { get; }

    /// <summary>
    /// Creates instance of an <see cref="ErrorDto"/>.
    /// </summary>
    /// <param name="code">One of a server-defined set of error codes.</param>
    /// <param name="message">A human-readable representation of the error.</param>
    /// <param name="target">The target of the error.</param>
    /// <param name="details">An array of details about specific errors that led to this reported error.</param>
    /// <param name="innerError">An object containing more specific information than the current object about the error.</param>
    public ErrorDto(
      ErrorCode code,
      string message,
      string? target = null,
      IEnumerable<ErrorDto>? details = null,
      ErrorDto? innerError = null)
    {
      if (string.IsNullOrWhiteSpace(message))
      {
        throw new ArgumentNullException(nameof(message), message: "Error message cannot be empty.");
      }

      Code = code;
      Message = message;

      Target = target;
      Details = details?.ToList();
      InnerError = innerError;
    }

    /// <summary>
    /// Creates ErrorDto based on given Error.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ErrorDto CreateFromError(Error error)
    {
      var detailsDto = error.Details?.Select(CreateFromError);
      var innerErrorDto =
        error.InnerError != null
          ? CreateFromError(error.InnerError)
          : null;

      return new ErrorDto(
        error.Code,
        error.Message,
        error.Target,
        detailsDto,
        innerErrorDto);
    }

    /// <summary>
    /// Converts ErrorDto to Error.
    /// </summary>
    /// <returns></returns>
    public Error ConvertToError()
    {
      var details = Details?.Select(x => x.ConvertToError());
      var innerErrorDto = InnerError?.ConvertToError();

      return new Error(
        Code,
        Message,
        Target,
        details,
        innerErrorDto);
    }
  }
}
