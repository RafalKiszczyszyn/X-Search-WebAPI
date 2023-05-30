using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XSearch.WebApi.Infrastructure.Documentation;

namespace XSearch.WebApi.Infrastructure.Startup
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
      m_applicationConfigActions.Add(app => app
        .UseRouting()
        .UseEndpoints(endpoints => endpoints.MapControllers()));

      return this;
    }

    /*public IRestApiConfiguration UseFluentValidation()
    {
      m_services.ConfigureFluentValidation(ApiAssembly.AsList());
      return this;
    }

    public IRestApiConfiguration UseHttpStatusCodeDictionary<T>() where T : class, IHttpStatusCodeDictionary
    {
      m_services.AddOrReplaceTransient<IHttpStatusCodeDictionary, T>();
      return this;
    }

    /// <inheritdoc cref="IRestApiConfiguration.UseJsonMergePatch"/>
    public IRestApiConfiguration UseJsonMergePatch()
    {
      m_services.ConfigureJsonMergePatch();
      return this;
    }

    /// <inheritdoc cref="IRestApiConfiguration.UseNullCollectionElementsValidation"/>
    public IRestApiConfiguration UseNullCollectionElementsValidation()
    {
      m_services.ConfigureNullCollectionElementValidation();
      return this;
    }

    /// <inheritdoc cref="IRestApiConfiguration.UsePackageErrorDictionary{T}"/>
    public IRestApiConfiguration UsePackageErrorDictionary<T>() where T : class, IPackageErrorDictionary
    {
      m_services.AddSingleton<IPackageErrorDictionary, T>();
      return this;
    }

    /// <inheritdoc cref="IRestApiConfiguration.UsePackageErrorDictionary(IPackageErrorDictionary)"/>
    public IRestApiConfiguration UsePackageErrorDictionary(IPackageErrorDictionary dictionary)
    {
      m_services.AddSingleton(dictionary);
      return this;
    }

    /// <inheritdoc cref="IRestApiConfiguration.UseProcInstanceProvider"/>
    public IRestApiConfiguration UseProcInstanceProvider(IProcInstanceProvider procInstanceProvider)
    {
      m_services.AddOrReplaceSingleton(procInstanceProvider);
      return this;
    }
    /// <inheritdoc cref="IRestApiConfiguration.ConfigureCustomApplication"/>
    public IRestApiConfiguration ConfigureCustomApplication(Action<IApplicationBuilder> configureApplicationAction)
    {
      m_applicationConfigActions.Add(configureApplicationAction);
      return this;
    }*/

    /// <inheritdoc cref="IRestApiConfiguration.UseCustomServices"/>
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