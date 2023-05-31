using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors
{
  internal class OpenApiFileDataEndpointProcessor : IOperationProcessor
  {
    private const string c_okStatusCode = "200";
    private const string c_etagName = "ETag";
    private const string c_ifNoneMatchName = "If-None-Match";
    private const string c_contentDisposition = "Content-Disposition";
    private const string c_contentType = "Content-Type";
    private const string c_applicationOctetStream = "application/octet-stream";
    private const string c_binaryFormat = "binary";

    private const string c_etagDescription = "An identifier for a specific version of the resource.";

    private const string c_ifNoneMatchDescription =
      "Return the resource if the provided value does not match the ETag value for this resource, or a 304 response otherwise.";

    private const string c_contentDispositionDescription =
      "Information about the resource, including `filename` and `filename*` fields.";

    private const string c_contentTypeDescription =
      "MIME type of the resource if it is known, or `application/octet-stream` otherwise.";

    private static readonly string s_fileDescription =
      $"The retrieved file is returned as the response body. The `{c_contentType}` is the {c_contentTypeDescription}";

    public bool Process(OperationProcessorContext context)
    {
      var ifNoneMatchRequestHeader = new OpenApiParameter
      {
        Kind = OpenApiParameterKind.Header,
        Name = c_ifNoneMatchName,
        Description = c_ifNoneMatchDescription,
        Type = JsonObjectType.String,
        Schema = new JsonSchema { Type = JsonObjectType.String, Description = c_ifNoneMatchDescription }
      };

      var etagResponseHeader =
        new JsonSchema { Type = JsonObjectType.String, Description = c_etagDescription };

      var contentDispositionResponseHeader =
        new JsonSchema { Type = JsonObjectType.String, Description = c_contentDispositionDescription };

      var contentTypeResponseHeader =
        new JsonSchema { Type = JsonObjectType.String, Description = c_contentTypeDescription };

      var fileResultMediaType = new OpenApiMediaType
      {
        Schema = new JsonSchema
        { Type = JsonObjectType.String, Format = c_binaryFormat, Description = s_fileDescription }
      };
      context.OperationDescription.Operation.Parameters.Add(ifNoneMatchRequestHeader);

      var okResponse = context.OperationDescription.Operation.Responses[c_okStatusCode];
      okResponse.Headers[c_contentDisposition] = new OpenApiHeader { Schema = contentDispositionResponseHeader };
      okResponse.Headers[c_contentType] = new OpenApiHeader { Schema = contentTypeResponseHeader };
      okResponse.Headers[c_etagName] = new OpenApiHeader { Schema = etagResponseHeader };
      okResponse.Content[c_applicationOctetStream] = fileResultMediaType;

      return true;
    }
  }
}
