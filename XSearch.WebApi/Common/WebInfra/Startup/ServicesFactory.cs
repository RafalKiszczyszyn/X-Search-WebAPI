using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using NSwag.Generation;
using XSearch.WebApi.Common.Security;
using XSearch.WebApi.Common.WebInfra.Documentation;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export.Generation;
using XSearch.WebApi.Common.WebInfra.ErrorHandling;

namespace XSearch.WebApi.Common.WebInfra.Startup
{
  internal static class ServicesFactory
  {
    public static IServiceCollection CreateDefaultServices(Assembly apiAssembly, OpenApiInfo openApiInfo)
    {
      return new ServiceCollection()
        .AddHttpContextAccessor()
        .ConfigureMvc(apiAssembly)
        .AddSingleton(openApiInfo)
        .ConfigureCookiePolicy()
        .ConfigureDocumentation(openApiInfo)
        .ConfigureErrorHandling()
        .ConfigureBasicAuthentication();
    }

    private static IServiceCollection ConfigureMvc(
      this IServiceCollection services,
      Assembly apiAssembly)
    {
      services
        .AddControllers(ConfigureMvcOptions)
        .AddApplicationPart(apiAssembly);

      return services;
    }

    private static void ConfigureMvcOptions(MvcOptions options)
    {
      options.Filters.Add(new ConsumesAttribute("application/json"));
      options.Filters.Add(new ProducesAttribute("application/json"));
      options.OutputFormatters.RemoveType<TextOutputFormatter>();
      options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
    }

    public static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
    {
      return services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
    }

    private static IServiceCollection ConfigureDocumentation(
      this IServiceCollection services,
      OpenApiInfo openApiInfo)
    {
      return services
        .AddStringEnumConverter()
        .AddSingleton<IDocumentationExporter, DocumentationExporter>()
        .AddSingleton(CreateDocumentGenerator)
        .ConfigureOpenApiDocument("plain", openApiInfo);
    }

    private static IServiceCollection AddStringEnumConverter(this IServiceCollection services)
    {
      var converter = new JsonStringEnumConverter(
        JsonNamingPolicy.CamelCase,
        allowIntegerValues: false);

      services.AddMvc().AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(converter));

      return services;
    }

    private static IDocumentGenerator CreateDocumentGenerator(IServiceProvider serviceProvider)
    {
      var registeredDocuments = serviceProvider.GetServices<OpenApiDocumentRegistration>()
        .Select(registration => registration.DocumentName)
        .ToList();

      var documentGenerator = serviceProvider.GetRequiredService<IOpenApiDocumentGenerator>();
      return new DocumentGeneratorWrapper(registeredDocuments, documentGenerator);
    }
  }
}
