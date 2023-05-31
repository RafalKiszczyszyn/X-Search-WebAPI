using System.Drawing;
using System.Net;
using Newtonsoft.Json;
using NSwag.AspNetCore;
using XSearch.WebApi.Extensions;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation
{
  internal class ReDocOptionsBuilder
  {
    private readonly ReDocOptionsDto m_options = new ReDocOptionsDto();

    public ReDocOptionsBuilder ExpandResponses(params HttpStatusCode[] statusCodes)
    {
      m_options.ExpandResponses = string.Join(
        separator: ",",
        values: statusCodes.Cast<int>());

      return this;
    }

    public ReDocOptionsBuilder HideDownloadButton(bool hideDownloadButton)
    {
      m_options.HideDownloadButton = hideDownloadButton;
      return this;
    }

    public ReDocOptionsBuilder HideLoading(bool hideLoading)
    {
      m_options.HideLoading = hideLoading;
      return this;
    }

    public ReDocOptionsBuilder MenuToggle(bool menuToggle)
    {
      m_options.MenuToggle = menuToggle;
      return this;
    }

    public ReDocOptionsBuilder NoAutoAuth(bool noAutoAuth)
    {
      m_options.NoAutoAuth = noAutoAuth;
      return this;
    }

    public ReDocOptionsBuilder PathInMiddlePanel(bool pathInMiddlePanel)
    {
      m_options.PathInMiddlePanel = pathInMiddlePanel;
      return this;
    }

    public ReDocOptionsBuilder UseLinkColors(Color color, Color? visited = null, Color? hover = null)
    {
      m_options.Theme ??= new Theme();
      m_options.Theme.Typography ??= new Typography();

      m_options.Theme.Typography.Links = new Links
      {
        Color = color.ToHex(),
        Visited = visited?.ToHex() ?? color.ToHex(),
        Hover = hover?.ToHex() ?? color.ToHex()
      };

      return this;
    }

    public ReDocOptionsBuilder UseLogoGutter(int pixels)
    {
      m_options.Theme ??= new Theme();
      m_options.Theme.Logo ??= new Logo();
      m_options.Theme.Logo.Gutter = $"{pixels}px";

      return this;
    }

    public ReDocOptionsBuilder UsePrimaryColor(Color color)
    {
      m_options.Theme ??= new Theme();

      m_options.Theme.Colors ??= new Colors();
      m_options.Theme.Colors.Primary ??= new Primary();
      m_options.Theme.Colors.Primary.Main = color.ToHex();

      m_options.Theme.RightPanel ??= new RightPanel();
      m_options.Theme.RightPanel.BackgroundColor = color.ToHex();

      return this;
    }

    public void ApplyToSettings(ReDocSettings reDocSettings)
    {
      reDocSettings.AdditionalSettings.Add("expandResponses", m_options.ExpandResponses);
      reDocSettings.AdditionalSettings.Add("hideDownloadButton", m_options.HideDownloadButton);
      reDocSettings.AdditionalSettings.Add("hideLoading", m_options.HideLoading);
      reDocSettings.AdditionalSettings.Add("menuToggle", m_options.MenuToggle);
      reDocSettings.AdditionalSettings.Add("noAutoAuth", m_options.NoAutoAuth);
      reDocSettings.AdditionalSettings.Add("pathInMiddlePanel", m_options.PathInMiddlePanel);
      reDocSettings.AdditionalSettings.Add("theme", m_options.Theme);
    }

    public string ToJson()
    {
      return JsonConvert.SerializeObject(m_options);
    }

    private class ReDocOptionsDto
    {
      [JsonProperty("expandResponses", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public string? ExpandResponses { get; set; }

      [JsonProperty("hideDownloadButton", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public bool? HideDownloadButton { get; set; }

      [JsonProperty("hideLoading", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public bool? HideLoading { get; set; }

      [JsonProperty("menuToggle", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public bool? MenuToggle { get; set; }

      [JsonProperty("noAutoAuth", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public bool? NoAutoAuth { get; set; }

      [JsonProperty("pathInMiddlePanel", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public bool? PathInMiddlePanel { get; set; }

      [JsonProperty("theme", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
      public Theme? Theme { get; set; }
    }

    private class Theme
    {
      [JsonProperty("colors")]
      public Colors? Colors { get; set; }

      [JsonProperty("rightPanel")]
      public RightPanel? RightPanel { get; set; }

      [JsonProperty("logo")]
      public Logo? Logo { get; set; }

      [JsonProperty("typography")]
      public Typography? Typography { get; set; }
    }

    private class Colors
    {
      [JsonProperty("primary")]
      public Primary? Primary { get; set; }
    }

    private class Primary
    {
      [JsonProperty("main")]
      public string? Main { get; set; }
    }

    private class RightPanel
    {
      [JsonProperty("backgroundColor")]
      public string? BackgroundColor { get; set; }
    }

    private class Logo
    {
      [JsonProperty("gutter")]
      public string? Gutter { get; set; }
    }

    private class Typography
    {
      [JsonProperty("links")]
      public Links? Links { get; set; }
    }

    private class Links
    {
      [JsonProperty("color")]
      public string? Color { get; set; }

      [JsonProperty("visited")]
      public string? Visited { get; set; }

      [JsonProperty("hover")]
      public string? Hover { get; set; }
    }
  }
}
