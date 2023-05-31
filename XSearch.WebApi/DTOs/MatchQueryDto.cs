using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query matching fields to a value. 
  /// </summary>
  [JsonSchema("MatchQuery")]
  public class MatchQueryDto
  {
    /// <summary>
    /// Document fields in order of relevance.
    /// </summary>
    [JsonProperty("fields", Required = Required.Always)]
    public List<string> Fields { get; set; }

    /// <summary>
    /// Value to be matched.
    /// </summary>
    [JsonProperty("value", Required = Required.Always)]
    public string Value { get; set; }
  }
}
