using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a wikipedia article.
  /// </summary>
  [JsonSchema("Article")]
  public class ArticleDto
  {
    [JsonProperty("id", Required = Required.Always)]
    public long Id { get; set; }

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; set; }

    [JsonProperty("revisionDate", Required = Required.Always)]
    public DateTime RevisionDate { get; set; }

    [JsonProperty("keywords", Required = Required.Always)]
    public List<string> Keywords { get; set; }

    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; set; }
  }
}
