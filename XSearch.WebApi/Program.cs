// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XSearch.WebApi.Common.WebInfra.Documentation;
using XSearch.WebApi.Common.WebInfra.Startup;
using XSearch.WebApi.Domain;
using XSearch.WebApi.Implementations;

var program = new WebApiProgram(args, new WebApiConfiguration(
      apiAssembly: typeof(Program).Assembly,
      openApiInfo: new OpenApiInfo(
        contractVersion: new ContractVersion(major: 1, minor: 0),
        title: "X-Search WebAPI",
        description: "REST API for searching Wikipedia articles.",
        basePath: "/"))
      .UseReDoc(
        tagsOrder: new List<string>
        {
          "Search Articles"
        })
      .UseCustomServices(services => services
          .AddSingleton<ISearchEngine>(x =>
          {
            var config = x.GetService<IConfiguration>()!;
            return new ElasticSearch(config["SearchEngineBaseUrl"], config["SearchEngineIndex"]);
          })));

if (program.IsDocumentationExportRequested)
{
  program.ExportDocumentation();
}
else
{
  program.RunWebHost();
}


