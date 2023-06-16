using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query.
  /// </summary>
  [JsonSchema("Query")]
  public class QuerySpecificationDto
  {
    /// <summary>
    /// Document fields must match provided value. 
    /// </summary>
    [JsonProperty("match", Required = Required.Default)]
    public MatchQuerySpecificationDto? Match { get; set; }

    /// <summary>
    /// Document field must be in provided range. 
    /// </summary>
    [JsonProperty("range", Required = Required.Default)]
    public RangeQuerySpecificationDto? Range { get; set; }

    /// <summary>
    /// Document must satisfy all conditions. 
    /// </summary>
    [JsonProperty("must", Required = Required.Default)]
    public List<QuerySpecificationDto>? Must { get; set; }

    /// <summary>
    /// Document must satisfy at least one condition.. 
    /// </summary>
    [JsonProperty("must", Required = Required.Default)]
    public List<QuerySpecificationDto>? Should { get; set; }

    /// <summary>
    /// Matching documents may be limited by filter query which does not affect the ranking. 
    /// </summary>
    [JsonProperty("filter", Required = Required.Default)]
    public QuerySpecificationDto? Filter { get; set; }
  }
}
