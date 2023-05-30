using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace XSearch.WebApi.Infrastructure.Startup
{
  internal class Startup : IStartup
  {
    private readonly WebApiConfiguration m_config;

    public Startup(WebApiConfiguration config)
    {
      m_config = config;
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      m_config.ApplyTo(services);
      return services.BuildServiceProvider();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
      m_config.ApplyTo(applicationBuilder);
    }
  }
}