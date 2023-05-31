namespace XSearch.WebApi.Common.WebInfra.Documentation.Attributes
{
  /// <summary>
  /// Removes all responses from json schema.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class OpenApiClearResponsesAttribute : Attribute
  {
  }
}