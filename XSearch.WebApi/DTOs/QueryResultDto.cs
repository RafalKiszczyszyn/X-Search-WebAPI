using Newtonsoft.Json;
using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query result.
  /// </summary>
  [JsonSchema("QueryResult")]
  public class QueryResultDto
  {
    /// <summary>
    /// Articles matching the query.
    /// </summary>
    [JsonProperty("matches", Required = Required.Always)]
    public List<ArticleDto> Matches { get; set; }

    /// <summary>
    /// Search time in millis.
    /// </summary>
    [JsonProperty("searchTime", Required = Required.Always)]
    public long SearchTime { get; set; }

    /// <summary>
    /// Number of all articles matching the query.
    /// </summary>
    [JsonProperty("matchingArticles", Required = Required.Always)]
    public long TotalHits { get; set; }
  }
}
