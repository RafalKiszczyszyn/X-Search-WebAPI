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
    /// List of articles produces by a query.
    /// </summary>
    [JsonProperty("matchingArticles", Required = Required.Default)]
    public List<ArticleDto> matches { get; set; }
  }
}
