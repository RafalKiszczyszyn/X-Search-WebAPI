

using System.Text;

namespace XSearch.WebApi.Common.WebInfra.Documentation
{
  /// <summary>
  /// Section of the ReDoc documentation.
  /// </summary>
  public class DocumentationSection
  {
    /// <summary>
    /// Title of the section.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Content of the section.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Creates new instance of <see cref="DocumentationSection"/>.
    /// </summary>
    /// <param name="title">Title of the section.</param>
    /// <param name="contentBytes">Content of the section (UTF-8 encoded bytes).</param>
    public DocumentationSection(string title, byte[] contentBytes)
      : this(title, Encoding.UTF8.GetString(contentBytes))
    {
    }

    /// <summary>
    /// Creates new instance of <see cref="DocumentationSection"/>.
    /// </summary>
    /// <param name="title">Title of the section.</param>
    /// <param name="content">Content of the section.</param>
    public DocumentationSection(string title, string content)
    {
      if (string.IsNullOrWhiteSpace(title))
      {
        throw new ArgumentNullException(nameof(title));
      }

      if (string.IsNullOrWhiteSpace(content))
      {
        throw new ArgumentNullException(nameof(content));
      }

      Title = title;
      Content = content;
    }
  }
}