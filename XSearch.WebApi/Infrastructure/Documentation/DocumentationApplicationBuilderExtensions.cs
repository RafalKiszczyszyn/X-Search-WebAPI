//  Copyright © Titian Software Ltd

using Microsoft.AspNetCore.Builder;
using NSwag.AspNetCore;
using XSearch.WebApi.Infrastructure.Documentation.Implementation;

namespace XSearch.WebApi.Infrastructure.Documentation
{
  internal static class DocumentationApplicationBuilderExtensions
  {
    public static void ConfigureReDocViewer(this IApplicationBuilder app)
      => app.UseReDoc(ConfigureReDoc);

    private static void ConfigureReDoc(ReDocSettings config)
    {
      config.Path = "/redoc";
      config.DocumentPath = "/swagger/full/swagger.json";

      ReDocOptions.ApplyToSettings(config);
    }
  }
}