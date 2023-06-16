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
    public QuerySpecificationDto Query { get; set; }

    /// <summary>
    /// Sort documents by fields.
    /// </summary>
    [JsonProperty("orderBy", Required = Required.Default)]
    public List<SortFieldSpecificationDto>? SortBy { get; set; }

    /// <summary>
    /// Index of a page (0-based).
    /// </summary>
    [JsonProperty("orderBy", Required = Required.Default)]
    public int PageIndex { get; set; } = 0;

    /// <summary>
    /// Number of documents on page (default: 10, max: 10,000).
    /// </summary>
    [JsonProperty("orderBy", Required = Required.Default)]
    public int PageSize { get; set; } = 10;
  }
}
