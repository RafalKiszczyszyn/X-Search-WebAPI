//  Copyright © Titian Software Ltd

using System.Drawing;
using System.Net;
using NSwag.AspNetCore;

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation
{
  internal static class ReDocOptions
  {
    private const int c_logoGutterPixels = 5;

    private static readonly Color s_primaryColor
      = Color.FromArgb(red: 68, green: 68, blue: 68);

    private static readonly Color s_linksColor
      = Color.FromArgb(red: 109, green: 109, blue: 187);

    private static readonly ReDocOptionsBuilder s_builder = CreateBuilder();

    public static void ApplyToSettings(ReDocSettings settings)
      => s_builder.ApplyToSettings(settings);

    public static string GetJson()
      => s_builder.ToJson();

    private static ReDocOptionsBuilder CreateBuilder()
    {
      return new ReDocOptionsBuilder()
        .ExpandResponses(HttpStatusCode.OK, HttpStatusCode.Created)
        .HideDownloadButton(true)
        .HideLoading(true)
        .MenuToggle(true)
        .NoAutoAuth(true)
        .PathInMiddlePanel(true)
        .UseLinkColors(s_linksColor)
        .UseLogoGutter(c_logoGutterPixels)
        .UsePrimaryColor(s_primaryColor);
    }
  }
}