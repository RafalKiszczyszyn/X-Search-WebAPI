//  Copyright © Titian Software Ltd

namespace XSearch.WebApi.Infrastructure.Documentation.Attributes
{
    /// <summary>
    /// Removes all responses from json schema.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OpenApiClearResponsesAttribute : Attribute
    {
    }
}