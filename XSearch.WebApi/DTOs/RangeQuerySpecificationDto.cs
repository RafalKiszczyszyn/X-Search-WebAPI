using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query matching a field to a range. 
  /// </summary>
  [JsonSchema("RangeQuery")]
  public class RangeQuerySpecificationDto
  {
    /// <summary>
    /// Document field.
    /// </summary>
    [JsonProperty("field", Required = Required.Always)]
    public string Field { get; set; }

    /// <summary>
    /// Value from which the field should be greater than or equal to. Expected number or JSON date as string.
    /// </summary>
    [JsonProperty("gte", Required = Required.Default)]
    public string? Gte { get; set; }

    /// <summary>
    /// Value from which the field should be less than or equal to. Expected number or JSON date as string.
    /// </summary>
    [JsonProperty("lte", Required = Required.Default)]
    public string? Lte { get; set; }
  }
}
