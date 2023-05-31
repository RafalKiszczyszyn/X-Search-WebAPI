

using XSearch.WebApi.Common.Errors;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Codes
{
  /// <summary>
  /// Infrastructure error code.
  /// </summary>
  internal static class InfrastructureErrorCode
  {
    public static ErrorCode CallFromWebPage { get; } = ErrorCode.Create(nameof(CallFromWebPage));

    public static ErrorCode DocumentationGenerationError { get; } = ErrorCode.Create(nameof(DocumentationGenerationError));

    public static ErrorCode IdempotencyError { get; } = ErrorCode.Create(nameof(IdempotencyError));

    public static ErrorCode InvalidLicence { get; } = ErrorCode.Create(nameof(InvalidLicence));

    public static ErrorCode ProcInstanceError { get; } = ErrorCode.Create(nameof(ProcInstanceError));

    public static ErrorCode UserGroupsNotFound { get; } = ErrorCode.Create(nameof(UserGroupsNotFound));

    public static ErrorCode UserNotFound { get; } = ErrorCode.Create(nameof(UserNotFound));
  }
}
