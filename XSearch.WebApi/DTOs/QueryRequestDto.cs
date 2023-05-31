using Newtonsoft.Json;
using NJsonSchema.Annotations;

namespace XSearch.WebApi.DTOs
{
  /// <summary>
  /// Representation of a query request.
  /// </summary>
  [JsonSchema("QueryRequest")]
  public class QueryRequestDto
  {
    /// <summary>
    /// Query specification.
    /// </summary>
    [JsonProperty("query", Required = Required.Always)]
    public QueryDto Query { get; set; }

    /// <summary>
    /// Order documents by fields.
    /// </summary>
    [JsonProperty("orderBy", Required = Required.Default)]
    public List<string>? OrderBy { get; set; }
  }
}
