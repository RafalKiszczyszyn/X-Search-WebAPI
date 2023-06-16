using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XSearch.WebApi.Common.WebInfra.Documentation;

namespace XSearch.WebApi.Common.WebInfra.Startup
{
  internal class WebApiConfiguration
  {
    private readonly IServiceCollection m_services;
    private readonly List<Action<IApplicationBuilder>> m_applicationConfigActions = new();

    public Assembly ApiAssembly { get; }
    public OpenApiInfo OpenApiInfo { get; }

    public WebApiConfiguration(
      Assembly apiAssembly,
      OpenApiInfo? openApiInfo = null)
    {
      ApiAssembly = apiAssembly;
      OpenApiInfo = openApiInfo ?? OpenApiInfo.CreateDefault(apiAssembly);

      m_services = ServicesFactory.CreateDefaultServices(apiAssembly, OpenApiInfo);
      m_applicationConfigActions.AddRange(new List<Action<IApplicationBuilder>>
      {
        app => app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader())
          .UseRouting()
          .UseAuthorization()
          .UseEndpoints(endpoints => endpoints.MapControllers())
      });

    }

    public WebApiConfiguration UseReDoc(
      IEnumerable<string>? tagsOrder = null,
      IEnumerable<DocumentationSection>? documentationSections = null)
    {
      var reDocInfo = new ReDocInfo(
        assemblyFileVersion: System.Diagnostics.FileVersionInfo.GetVersionInfo(ApiAssembly.Location).FileVersion!,
        tagsOrder,
        documentationSections);

      m_services.ConfigureOpenApiDocument("full", OpenApiInfo, reDocInfo);
      m_applicationConfigActions.Add(app => app.ConfigureReDocViewer());
      m_applicationConfigActions.Add(app => app.UseOpenApi());
      
      return this;
    }

    public WebApiConfiguration UseCustomServices(Action<IServiceCollection> configureServicesAction)
    {
      configureServicesAction(m_services);
      return this;
    }

    public void ApplyTo(IServiceCollection externalServices)
    {
      m_services.AddSingleton(this);

      foreach (var service in m_services)
      {
        externalServices.Add(service);
      }
    }

    public void ApplyTo(IApplicationBuilder applicationBuilder)
    {
      foreach (var applicationConfigAction in m_applicationConfigActions)
      {
        applicationConfigAction(applicationBuilder);
      }
    }
  }
}