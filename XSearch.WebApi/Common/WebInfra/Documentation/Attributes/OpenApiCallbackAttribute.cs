

namespace XSearch.WebApi.Common.WebInfra.Documentation.Attributes
{
    /// <summary>Indicates a callback to be added to the Swagger definition.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class OpenApiCallbackAttribute : Attribute
    {
        /// <summary>
        /// The name of the callback.
        /// If unspecified defaults to "{operationId}_callback".
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// The URL of the endpoint that will receive the callback.
        /// </summary>
        public string CallbackUrl { get; }

        /// <summary>
        /// The HTTP method that will be used to call the endpoint.
        /// If unspecified defaults to POST.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// The content type of the callback bodies.
        /// If unspecified defaults to "octet-stream" for binary content, "application/json" otherwise.
        /// </summary>
        public string? MimeType { get; }

        /// <summary>
        /// Summary of the callback operation.
        /// </summary>
        public string? Summary { get; }

        /// <summary>
        /// Description of the callback operation.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// The possible object types passed as an argument to the callback.
        /// If none are specified, the definition will indicate a callback with an empty body.
        /// </summary>
        public Type? RequestType { get; }

        /// <summary>
        /// Type of response object, when operation ends successfully.
        /// </summary>
        public Type? SuccessResponseType { get; }

        /// <summary>
        /// Description of successful response.
        /// </summary>
        public string? SuccessResponseDescription { get; }

        /// <summary>
        /// HTTP status code for successful response.
        /// </summary>
        public string? SuccessResponseHttpStatusCode { get; }

        /// <summary>
        /// Type of error response.
        /// </summary>
        public Type? ErrorResponseType { get; }

        /// <summary>
        /// Description of error response.
        /// </summary>
        public string? ErrorResponseDescription { get; }

        /// <summary>
        /// HTTP status code for error response. Wild cards can be specified in format 4xx or 5xx. By default is set wildcard status code 4xx/5xx.
        /// </summary>
        public string? ErrorResponseHttpStatusCode { get; }

        /// <summary>Initializes a new instance of the <see cref="OpenApiCallbackAttribute"/> class.</summary>
        /// <param name="callbackUrl">The URL of the endpoint that will receive the callback.</param>
        /// <param name="method">The HTTP method that will be used to call the endpoint. If unspecified defaults to POST.</param>
        /// <param name="mimeType">The content type of the callback bodies. If unspecified defaults to "octet-stream" for binary content, "application/json" otherwise.</param>
        /// <param name="name">The name of the callback. If unspecified defaults to "{operationId}_callback".</param>
        /// <param name="summary">Summary of the callback operation.</param>
        /// <param name="description">Description of the callback operation.</param>
        /// <param name="requestType">The possible object types passed as an argument to the callback. If none are specified, the definition will indicate a callback with an empty body.</param>
        /// <param name="successResponseType">Type of response object, when operation ends successfully.</param>
        /// <param name="successResponseDescription">Description of successful response.</param>
        /// <param name="successResponseHttpStatusCode">HTTP status code for successful response.</param>
        /// <param name="errorResponseType">Type of error response.</param>
        /// <param name="errorResponseDescription">Description of error response.</param>
        /// <param name="errorResponseHttpStatusCode">HTTP status code for error response. Wild cards can be specified in format 4xx or 5xx. By default is set wildcard status code 4xx/5xx.</param>
        public OpenApiCallbackAttribute(
          string callbackUrl,
          string method = "post",
          string? mimeType = null,
          string? name = null,
          string? summary = null,
          string? description = null,
          Type? requestType = null,
          Type? successResponseType = null,
          string? successResponseDescription = null,
          string? successResponseHttpStatusCode = null,
          Type? errorResponseType = null,
          string? errorResponseDescription = null,
          string? errorResponseHttpStatusCode = null)
        {
            Name = name;
            RequestType = requestType;
            ErrorResponseHttpStatusCode = errorResponseHttpStatusCode;
            SuccessResponseType = successResponseType;
            SuccessResponseDescription = successResponseDescription;
            SuccessResponseHttpStatusCode = successResponseHttpStatusCode;
            CallbackUrl = callbackUrl;
            Method = method;
            MimeType = mimeType;
            Summary = summary;
            Description = description;
            ErrorResponseType = errorResponseType;
            ErrorResponseDescription = errorResponseDescription;
        }
    }
}