using NSwag.Annotations;
using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Attributes.Files
{
    /// <summary>
    /// Define a multipart/form-data request body for file uploads.
    /// </summary>
    public class OpenApiFileRequestBodyAttribute : OpenApiOperationProcessorAttribute
    {
        /// <summary>
        /// Define a multipart/form-data request body for file uploads.
        /// </summary>
        /// <param name="formFieldName">Name of the form field for file upload.</param>
        public OpenApiFileRequestBodyAttribute(string formFieldName = "files")
          : base(typeof(OpenApiFileRequestBodyProcessor), formFieldName)
        {
        }
    }
}
