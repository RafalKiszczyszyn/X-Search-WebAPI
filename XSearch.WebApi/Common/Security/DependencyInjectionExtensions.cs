
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.DependencyInjection;

namespace XSearch.WebApi.Common.Security
{
  internal static class DependencyInjectionExtensions
  {
    public static IServiceCollection ConfigureBasicAuthentication(
      this IServiceCollection services)
    {
      services.AddAuthentication("Basic")
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
          "Basic", null);

      return services.AddScoped<BasicAuthenticationService, BasicAuthenticationService>()
        .AddScoped<IAuthenticationService>(x => x.GetService<BasicAuthenticationService>()!)
        .AddScoped<ICredentialsProvider>(x => x.GetService<BasicAuthenticationService>()!);
    }
  }
}
