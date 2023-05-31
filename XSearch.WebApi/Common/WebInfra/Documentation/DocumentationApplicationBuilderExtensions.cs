using Microsoft.AspNetCore.Builder;
using NSwag.AspNetCore;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation;

namespace XSearch.WebApi.Common.WebInfra.Documentation
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