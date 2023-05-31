
namespace XSearch.WebApi.Common.Errors
{
  internal class Error
  {
    /// <summary>
    /// One of a server-defined set of error codes.
    /// </summary>
    public ErrorCode Code { get; }

    /// <summary>
    /// A human-readable representation of the error.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The target of the error.
    /// </summary>
    public string? Target { get; }

    /// <summary>
    /// An array of details about specific errors that led to this reported error.
    /// </summary>
    public IReadOnlyList<Error>? Details { get; }

    /// <summary>
    /// An object containing more specific information than the current object about the error.
    /// </summary>
    public Error? InnerError { get; }

    /// <summary>
    /// Creates instance of an <see cref="Error"/>.
    /// </summary>
    /// <param name="code">One of a server-defined set of error codes.</param>
    /// <param name="message">A human-readable representation of the error.</param>
    /// <param name="target">The target of the error.</param>
    /// <param name="details">An array of details about specific errors that led to this reported error.</param>
    /// <param name="innerError">An object containing more specific information than the current object about the error.</param>
    public Error(
      ErrorCode code,
      string message,
      string? target = null,
      IEnumerable<Error>? details = null,
      Error? innerError = null)
    {
      if (string.IsNullOrWhiteSpace(message))
      {
        throw new ArgumentNullException(
          nameof(message), message: "Error message cannot be empty.");
      }

      Code = code;
      Message = message;

      Target = target;
      Details = details?.ToList();
      InnerError = innerError;
    }

    /// <summary>
    /// Throws <see cref="ErrorException"/> created from the error instance.
    /// </summary>
    /// <exception cref="ErrorException"/>
    public void Throw()
      => throw new ErrorException(this);

    /// <summary>
    /// Returns <see cref="string"/> representation of <see cref="Error"/>. 
    /// </summary>
    public override string ToString()
    {
      var errorMessageLines = new List<string>
      {
        $"Code: {Code}",
        $"Message: {Message}"
      };

      if (Target != null)
      {
        errorMessageLines.Add($"Target: {Target}");
      }

      if (Details?.Any() ?? false)
      {
        errorMessageLines.Add("Details:");
        errorMessageLines.AddRange(Details
          .Select(detail => $"-{detail.ToString().Indent()}"));
      }

      if (InnerError != null)
      {
        errorMessageLines.Add("InnerError:");
        errorMessageLines.Add(InnerError.ToString().Indent());
      }

      return string.Join(Environment.NewLine, errorMessageLines);
    }

    public static Error CreateFrom(Exception exception)
    {
      if (exception is ErrorException e)
        return e.Error;

      var stackTrace = exception.StackTrace.IsNotNullOrWhitespace()
        ? new Error(
          code: ErrorCode.StackTrace,
          message: exception.StackTrace ?? "")
        : null;

      var innerError = new Error(
        code: ErrorCode.ExceptionMessage,
        message: exception.Message,
        innerError: stackTrace);

      return new Error(
        code: ErrorCode.ServerFailure,
        message: $"An exception occurred – {exception.GetType()}",
        innerError: innerError);
    }
  }
}
