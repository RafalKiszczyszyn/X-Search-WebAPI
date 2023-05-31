
namespace XSearch.WebApi.Common.Errors
{
  internal class ErrorCode : IEquatable<ErrorCode>
  {
    /// <summary>
    /// Combined error.
    /// </summary>
    public static ErrorCode CombinedError { get; } = new ErrorCode("CombinedError");

    /// <summary>
    /// E-mail not found.
    /// </summary>
    public static ErrorCode EmailNotFound { get; } = new ErrorCode("EmailNotFound");

    /// <summary>
    /// Error detail.
    /// </summary>
    public static ErrorCode ErrorDetail { get; } = new ErrorCode("ErrorDetail");

    /// <summary>
    /// Exception message.
    /// </summary>
    public static ErrorCode ExceptionMessage { get; } = new ErrorCode("ExceptionMessage");

    /// <summary>
    /// Generic error.
    /// </summary>
    public static ErrorCode GenericError { get; } = new ErrorCode("GenericError");

    /// <summary>
    /// Insufficient rights.
    /// </summary>
    public static ErrorCode InsufficientRights { get; } = Create("InsufficientRights");

    /// <summary>
    /// I/O error.
    /// </summary>
    public static ErrorCode IOError { get; } = Create("IOError");

    /// <summary>
    /// Invalid request.
    /// </summary>
    public static ErrorCode InvalidRequest { get; } = new ErrorCode("InvalidRequest");

    /// <summary>
    /// Not found.
    /// </summary>
    public static ErrorCode NotFound { get; } = new ErrorCode("NotFound");

    /// <summary>
    /// Oracle error.
    /// </summary>
    public static ErrorCode OracleError { get; } = new ErrorCode("OracleError");

    /// <summary>
    /// Oracle error code.
    /// </summary>
    public static ErrorCode OracleErrorCode { get; } = new ErrorCode("OracleErrorCode");

    /// <summary>
    /// Package error.
    /// </summary>
    public static ErrorCode PackageError { get; } = new ErrorCode("PackageError");

    /// <summary>
    /// Package error code.
    /// </summary>
    public static ErrorCode PackageErrorCode { get; } = new ErrorCode("PackageErrorCode");

    /// <summary>
    /// Server failure.
    /// </summary>
    public static ErrorCode ServerFailure { get; } = new ErrorCode("ServerFailure");

    /// <summary>
    /// Stack trace.
    /// </summary>
    public static ErrorCode StackTrace { get; } = new ErrorCode("StackTrace");

    /// <summary>
    /// UserName not found.
    /// </summary>
    public static ErrorCode UserNameNotAvailable { get; } = new ErrorCode("UserNameNotAvailable");

    /// <summary>
    /// User not found.
    /// </summary>
    public static ErrorCode UserNotFound { get; } = new ErrorCode("UserNotFound");

    /// <summary>
    /// Validation error.
    /// </summary>
    public static ErrorCode ValidationError { get; } = new ErrorCode("ValidationError");

    /// <summary>
    /// Validation error code.
    /// </summary>
    public static ErrorCode ValidationErrorCode { get; } = new ErrorCode("ValidationErrorCode");

    /// <summary>
    /// Validation error detail.
    /// </summary>
    public static ErrorCode ValidationErrorDetail { get; } = new ErrorCode("ValidationErrorDetail");

    /// <summary>
    /// Wrong URL.
    /// </summary>
    public static ErrorCode WrongUrl { get; } = new ErrorCode("WrongUrl");

    /// <summary>
    /// Creates instance of <see cref="ErrorCode"/> class.
    /// </summary>
    /// <param name="errorCodeValue">Error code value.</param>
    /// <returns></returns>
    public static ErrorCode Create(string errorCodeValue)
      => new ErrorCode(errorCodeValue);

    /// <summary>
    /// Error code value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates new instance of <see cref="ErrorCode"/>.
    /// </summary>
    /// <param name="value">Error code value.</param>
    private ErrorCode(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentNullException(nameof(value), message: "Error code cannot be empty.");
      }

      Value = value;
    }

    /// <summary>
    /// Compares ErrorCode to another object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
      return obj is ErrorCode other && Equals(other);
    }

    /// <summary>
    /// Compares ErrorCode to another.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ErrorCode? other)
      => other != null && Value.Equals(other.Value);

    /// <summary>
    /// Gets hash code of the error code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
      => Value.GetHashCode();

    /// <summary>
    /// Equality operator.
    /// </summary>
    public static bool operator ==(ErrorCode? errorCode1, ErrorCode? errorCode2)
    {
      return errorCode1 is { } &&
             errorCode2 is { } &&
             errorCode1.Value == errorCode2.Value;
    }

    /// <summary>
    /// Inequality operator.
    /// </summary>
    public static bool operator !=(ErrorCode? errorCode1, ErrorCode? errorCode2)
      => !(errorCode1 == errorCode2);

    /// <summary>
    /// Returns the <see cref="string"/> representation of <see cref="ErrorCode"/>.
    /// </summary>
    public override string ToString()
      => Value;
  }
}
