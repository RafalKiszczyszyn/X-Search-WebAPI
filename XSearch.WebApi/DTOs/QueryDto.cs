using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query.
  /// </summary>
  [JsonSchema("Query")]
  public class QueryDto
  {
    /// <summary>
    /// Document fields must match provided value. 
    /// </summary>
    [JsonProperty("match", Required = Required.Default)]
    public MatchQueryDto? Match { get; set; }

    /// <summary>
    /// Document field must be in provided range. 
    /// </summary>
    [JsonProperty("range", Required = Required.Default)]
    public RangeQueryDto? Range { get; set; }

    /// <summary>
    /// Document must satisfy all conditions. 
    /// </summary>
    [JsonProperty("must", Required = Required.Default)]
    public List<QueryDto>? Must { get; set; }

    /// <summary>
    /// Document must satisfy at least one condition.. 
    /// </summary>
    [JsonProperty("must", Required = Required.Default)]
    public List<QueryDto>? Should { get; set; }

    /// <summary>
    /// Matching documents may be limited by filter query which does not affect the ranking. 
    /// </summary>
    [JsonProperty("filter", Required = Required.Default)]
    public QueryDto? Filter { get; set; }
  }
}
