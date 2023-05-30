using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Formatting = System.Xml.Formatting;
using XSearch.WebApi.Infrastructure.Documentation.Implementation.Export;

namespace XSearch.WebApi.Infrastructure.Startup
{
  internal class WebApiProgram
  {
    private const string c_exportKeyword = "EXPORT";

    private readonly string[] m_args;
    private readonly WebApiConfiguration m_config;
    private readonly Action<IWebHostBuilder>? m_configureBuilderAction;

    public WebApiProgram(
      string[] args,
      WebApiConfiguration config,
      Action<IWebHostBuilder>? configureBuilderAction = null)
    {
      m_args = args;
      m_config = config;
      m_configureBuilderAction = configureBuilderAction;
    }

    public bool IsDocumentationExportRequested
      => m_args.FirstOrDefault()?.ToUpperInvariant() == c_exportKeyword;

    public void ExportDocumentation()
    {
      var outputDirectory = m_args.Skip(1).FirstOrDefault();
      var webHost = CreateWebHost();

      var documentationExporter = webHost.Services.GetRequiredService<IDocumentationExporter>();
      documentationExporter.ExportDocs(outputDirectory ?? string.Empty);
    }

    public void RunWebHost()
      => CreateWebHost().Run();

    private IWebHost CreateWebHost()
    {
      var startup = new Startup(m_config);
      var builder = WebHost.CreateDefaultBuilder(m_args)
        .ConfigureServices(services => services.AddSingleton<IStartup>(startup));

      m_configureBuilderAction?.Invoke(builder);

      return builder
        .Build();
    }
  }
}