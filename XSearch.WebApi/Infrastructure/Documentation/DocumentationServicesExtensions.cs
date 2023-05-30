//  Copyright © Titian Software Ltd

using Microsoft.Extensions.DependencyInjection;
using XSearch.WebApi.Infrastructure.Documentation.Implementation.Generation;

namespace XSearch.WebApi.Infrastructure.Documentation
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