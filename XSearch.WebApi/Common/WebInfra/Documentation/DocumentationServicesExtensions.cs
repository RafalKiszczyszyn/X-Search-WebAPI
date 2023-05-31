using Microsoft.Extensions.DependencyInjection;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Generation;

namespace XSearch.WebApi.Common.WebInfra.Documentation
{
  internal static class DocumentationServicesExtensions
  {
    public static IServiceCollection ConfigureOpenApiDocument(
      this IServiceCollection services,
      string documentName,
      OpenApiInfo openApiInfo,
      ReDocInfo? reDocInfo = null)
    {
      return services.AddOpenApiDocument(
        config => config.ConfigureDocument(documentName, openApiInfo, reDocInfo));
    }
  }
}