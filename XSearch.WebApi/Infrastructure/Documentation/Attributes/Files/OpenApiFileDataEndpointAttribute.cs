using NSwag.Annotations;
using XSearch.WebApi.Infrastructure.Documentation.Implementation.Processors;

namespace XSearch.WebApi.Infrastructure.Documentation.Attributes.Files
{
    /// <summary>
    /// Add If-None-Match request header, and ETag and Content-Disposition response headers.
    /// </summary>
    public class OpenApiFileDataEndpointAttribute : OpenApiOperationProcessorAttribute
    {
        /// <summary>
        /// Add If-None-Match request header, and ETag and Content-Disposition response headers.
        /// </summary>
        public OpenApiFileDataEndpointAttribute()
          : base(typeof(OpenApiFileDataEndpointProcessor))
        {
        }
    }
}
