using System.Net;
using System.Reflection;
using Namotion.Reflection;
using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using XSearch.WebApi.Common.WebInfra.Documentation.Attributes;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors
{
  /// <summary>Processes the OpenApiCallbackAttribute on the operation method.</summary>
  internal class OperationCallbackProcessor : IOperationProcessor
  {
    private const string c_defaultErrorStatusCodeString = "4xx/5xx";

    /// <summary>
    /// Processes the specified method information for any callbacks.
    /// </summary>
    /// <param name="context">The processor context.</param>
    /// <returns>Always returns true, since callbacks are optional.</returns>
    public bool Process(OperationProcessorContext context)
    {
      var operation = context.OperationDescription.Operation;

      operation.Callbacks ??= new Dictionary<string, OpenApiCallback>();

      foreach (var callbackAttribute in context.MethodInfo.GetCustomAttributes<OpenApiCallbackAttribute>())
      {
        var callbackOperation = CreateCallbackOperation(context, callbackAttribute);

        string key = callbackAttribute.Name ?? context.OperationDescription.Operation.OperationId + "_Callback";

        operation.Callbacks[key] =
          new OpenApiCallback
          {
            {
              callbackAttribute.CallbackUrl,
              new OpenApiPathItem
              {
                {
                  callbackAttribute.Method ?? OpenApiOperationMethod.Post,
                  callbackOperation
                }
              }
            }
          };
      }

      return true;
    }

    private static OpenApiOperation CreateCallbackOperation(
      OperationProcessorContext context,
      OpenApiCallbackAttribute callbackAttribute)
    {
      var requestBody = CreateRequestBody(context, callbackAttribute);

      var callbackOperation = new OpenApiOperation
      {
        RequestBody = requestBody,
        Description = callbackAttribute.Description,
        Summary = callbackAttribute.Summary,
      };

      var successResponse = CreateOpenApiResponse(
        callbackAttribute.SuccessResponseHttpStatusCode ?? ((int)HttpStatusCode.OK).ToString(),
        callbackAttribute.SuccessResponseType,
        callbackAttribute.SuccessResponseDescription,
        context);

      callbackOperation.Responses.Add(successResponse);

      if (callbackAttribute.ErrorResponseHttpStatusCode != null || callbackAttribute.ErrorResponseType != null)
      {
        var errorResponse = CreateOpenApiResponse(
          callbackAttribute.ErrorResponseHttpStatusCode ?? c_defaultErrorStatusCodeString,
          callbackAttribute.ErrorResponseType,
          callbackAttribute.ErrorResponseDescription,
          context);

        callbackOperation.Responses.Add(errorResponse);
      }

      return callbackOperation;
    }

    private static OpenApiRequestBody? CreateRequestBody(
      OperationProcessorContext context,
      OpenApiCallbackAttribute callbackAttribute)
    {
      if (callbackAttribute.RequestType == null)
      {
        return null;
      }

      var requestBody = new OpenApiRequestBody { IsRequired = true };

      var requestSchema = GenerateSchema(callbackAttribute.RequestType, context);

      var mimeType2 = callbackAttribute.MimeType ?? GetDefaultMimeType(requestSchema);

      requestBody.Content[mimeType2] = new OpenApiMediaType { Schema = requestSchema };

      return requestBody;
    }

    private static KeyValuePair<string, OpenApiResponse> CreateOpenApiResponse(
      string statusCodeString,
      Type? responseType,
      string? responseDescription,
      OperationProcessorContext context)
    {
      var responseSchema = responseType != null ? GenerateSchema(responseType, context) : null;

      var response = new OpenApiResponse
      {
        Description = responseDescription,
        Schema = responseSchema
      };

      return new KeyValuePair<string, OpenApiResponse>(statusCodeString, response);
    }

    private static string GetDefaultMimeType(JsonSchema requestSchema) =>
      requestSchema.IsBinary
        ? "application/octet-stream"
        : "application/json";

    private static JsonSchema GenerateSchema(Type type, OperationProcessorContext context)
    {
      var schema =
        context.SchemaGenerator.GenerateWithReferenceAndNullability<JsonSchema>(
          type.ToContextualType(),
          context.SchemaResolver);

      if (schema.OneOf.Count == 1)
      {
        var innerSchema = schema.OneOf.Single();
        schema.OneOf.Clear();
        schema.Reference = innerSchema;
      }

      return schema;
    }
  }
}