using NSwag.Annotations;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Attributes.Files
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
