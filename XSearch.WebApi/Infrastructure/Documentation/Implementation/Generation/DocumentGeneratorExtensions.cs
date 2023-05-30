
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using XSearch.WebApi.Infrastructure.Documentation.Implementation.Processors;

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Generation
{
  internal static class DocumentGeneratorExtensions
  {
    private const ReferenceTypeNullHandling c_referenceTypeNullHandling = ReferenceTypeNullHandling.NotNull;

    private static readonly IReadOnlyCollection<IOperationProcessor> s_operationProcessors = new List<IOperationProcessor>
    {
      new OperationCallbackProcessor(),
      new OpenApiClearResponsesProcessor()
    };

    private static readonly IReadOnlyCollection<string> s_securitySchemes = new List<string>
    {
      "Basic"
    };

    private static readonly IReadOnlyCollection<OpenApiParameterKind> s_notNullableParameterKinds = new List<OpenApiParameterKind>
    {
      OpenApiParameterKind.Query,
      OpenApiParameterKind.Path,
      OpenApiParameterKind.Header
    };

    public static void ConfigureDocument(
      this AspNetCoreOpenApiDocumentGeneratorSettings config,
      string documentName,
      OpenApiInfo openApiInfo,
      ReDocInfo? reDocInfo)
    {
      config.DocumentName = documentName;
      config.DefaultResponseReferenceTypeNullHandling = c_referenceTypeNullHandling;
      config.AddHttpSecuritySchemes(s_securitySchemes);
      config.PostProcess = document => PostProcess(document, openApiInfo, reDocInfo);

      foreach (var operationProcessor in s_operationProcessors)
      {
        config.OperationProcessors.Add(operationProcessor);
      }
    }

    public static void AddHttpSecuritySchemes(
      this OpenApiDocumentGeneratorSettings config,
      IEnumerable<string> schemes)
    {
      foreach (var scheme in schemes)
      {
        config.AddSecurity(scheme, CreateHttpSecurityScheme(scheme));
      }
    }

    private static OpenApiSecurityScheme CreateHttpSecurityScheme(string scheme)
    {
      return new OpenApiSecurityScheme
      {
        Type = OpenApiSecuritySchemeType.Http,
        In = OpenApiSecurityApiKeyLocation.Header,
        Scheme = scheme.ToLowerInvariant()
      };
    }

    private static void PostProcess(OpenApiDocument document, OpenApiInfo openApiInfo, ReDocInfo? reDocInfo)
    {
      document.Info.Version = openApiInfo.ContractVersion != null
        ? openApiInfo.ContractVersion.ToString()
        : "undefined";

      if (reDocInfo != null)
      {
        document.Info.Description = $"Assembly version: {reDocInfo.AssemblyFileVersion}<br/>{openApiInfo.Description}";
        document.IncludeAdditionalData(reDocInfo);
      }
      else
      {
        document.Info.Description = openApiInfo.Description;
        document.RemoveCallbacks();
      }

      document.Info.Contact = new OpenApiContact
      {
        Name = "Copyright © X-Search"
      };

      document.Host = "https://example.com";
      document.Schemes = new List<OpenApiSchema> { OpenApiSchema.Https };
      document.Info.Title = openApiInfo.Title;
      document.BasePath = openApiInfo.BasePath;

      document.FixAndCleanup(s_notNullableParameterKinds);
    }

    private static void RemoveCallbacks(this OpenApiDocument document)
    {
      document.Paths
        .Where(DocumentPathHasCallbacks)
        .ToList()
        .ForEach(path => document.Paths.Remove(path));
    }

    private static bool DocumentPathHasCallbacks(KeyValuePair<string, OpenApiPathItem> documentPath)
    {
      return documentPath.Value
        .Any(innerValue => innerValue.Value.Callbacks.Any());
    }
  }
}
