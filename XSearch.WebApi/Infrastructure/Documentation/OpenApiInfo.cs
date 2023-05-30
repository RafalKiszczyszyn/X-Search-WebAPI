//  Copyright © Titian Software Ltd

using System.Reflection;
using XSearch.WebApi.Infrastructure.Startup;

namespace XSearch.WebApi.Infrastructure.Documentation
{
  /// <summary>
  /// Open API info.
  /// </summary>
  public class OpenApiInfo
  {
    /// <summary>
    /// REST API contract version.
    /// </summary>
    public ContractVersion? ContractVersion { get; }

    /// <summary>
    /// API document title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// API document description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// API document base path.
    /// </summary>
    public string BasePath { get; }

    /// <summary>
    /// Creates default <see cref="OpenApiInfo"/> for provided <see cref="Assembly"/>.
    /// </summary>
    /// <param name="apiAssembly"><see cref="Assembly"/> to create <see cref="OpenApiInfo"/> for.</param>
    public static OpenApiInfo CreateDefault(Assembly apiAssembly)
    {
      return new OpenApiInfo(
        contractVersion: new ContractVersion(major: 1, minor: 0),
        title: apiAssembly.GetName().Name!,
        description: "Auto-generated Open API specification.",
        basePath: "/api");
    }

    /// <summary>
    /// Creates new instance of <see cref="OpenApiInfo"/>.
    /// </summary>
    /// <param name="contractVersion">REST API contract version.</param>
    /// <param name="title">API document title.</param>
    /// <param name="description">API document description.</param>
    /// <param name="basePath">API document base path.</param>
    public OpenApiInfo(
      ContractVersion? contractVersion,
      string title,
      string description,
      string basePath)
    {
      AssertNotEmpty(title, nameof(title));
      AssertNotEmpty(description, nameof(description));
      AssertNotEmpty(basePath, nameof(basePath));

      ContractVersion = contractVersion;
      Title = title;
      Description = description;
      BasePath = basePath;
    }

    private static void AssertNotEmpty(string value, string name)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentNullException(name);
      }
    }
  }
}
