
namespace XSearch.WebApi.Common.Security
{
  public interface IAuthenticationService
  {
    public void Authenticate(string username, string password);
  }

  public interface ICredentialsProvider
  {
    public string? Username { get; }
    public string? Password { get; }
  }

  internal class BasicAuthenticationService : IAuthenticationService, ICredentialsProvider
  {
    public void Authenticate(string username, string password)
    {
      Username = username;
      Password = password;
    }

    public string? Username { get; private set; }
    public string? Password { get; private set; }
  }
}
