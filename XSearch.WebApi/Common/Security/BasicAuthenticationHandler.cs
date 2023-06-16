
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XSearch.WebApi.Common.Security
{
  public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
  {
    private readonly IAuthenticationService _authenticationService;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAuthenticationService authenticationService) : base(options, logger, encoder, clock)
    {
      _authenticationService = authenticationService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var authHeader = Request.Headers["Authorization"].ToString();

      if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
      {
        var token = authHeader["Basic ".Length..].Trim();
        var credentials = Encoding.UTF8
          .GetString(Convert.FromBase64String(token))
          .Split(':');

        _authenticationService.Authenticate(credentials[0], credentials[1]);

        var claims = new[] { new Claim(ClaimTypes.Name, credentials[0]) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
      }

      Response.StatusCode = 401;
      return Task.FromResult(AuthenticateResult.Fail("No Authorization Header"));
    }
  }
}
