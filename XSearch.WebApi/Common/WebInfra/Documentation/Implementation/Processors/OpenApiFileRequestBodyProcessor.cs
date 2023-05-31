using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors
{
  internal class OpenApiFileRequestBodyProcessor : IOperationProcessor
  {
    private readonly string m_formFieldName;

    private const string c_headersInfo =
      "Must include the `Content-Type` and `Content-Disposition` (with `filename` and optionally `filename*`) headers.";

    private static readonly string s_arrayDescription = $"Files to upload. {c_headersInfo}";
    private static readonly string s_singleFileDescription = $"File to upload. {c_headersInfo}";

    public OpenApiFileRequestBodyProcessor(string formFieldName = "files")
    {
      m_formFieldName = formFieldName;
    }

    public bool Process(OperationProcessorContext context)
    {
      var fileItems = new JsonSchema
      {
        Type = JsonObjectType.String,
        Format = "binary",
        Description = s_singleFileDescription
      };

      var arrayProperty = new JsonSchemaProperty
      {
        Type = JsonObjectType.Array,
        Description = s_arrayDescription
      };
      arrayProperty.Items.Add(fileItems);

      var mediaType = new OpenApiMediaType
      {
        Schema = new JsonSchema
        {
          Type = JsonObjectType.Object,
          Properties = { new KeyValuePair<string, JsonSchemaProperty>(m_formFieldName, arrayProperty) }
        }
      };

      context.OperationDescription.Operation.RequestBody = new OpenApiRequestBody
      {
        Content = { new KeyValuePair<string, OpenApiMediaType>("multipart/form-data", mediaType) }
      };

      return true;
    }
  }
}
