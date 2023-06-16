using NJsonSchema.Annotations;
using Newtonsoft.Json;
using XSearch.WebApi.Domain;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a field taking part in sorting. 
  /// </summary>
  [JsonSchema("SortField")]
  public class SortFieldSpecificationDto
  {
    /// <summary>
    /// Name of the field.
    /// </summary>
    [JsonProperty("field", Required = Required.Always)]
    public string Field { get; set; }

    /// <summary>
    /// Sort order. Defaults to ascending order. 
    /// </summary>
    [JsonProperty("order", Required = Required.Default)]
    public SortOrder Order { get; set; } = SortOrder.Asc;
  }
}
