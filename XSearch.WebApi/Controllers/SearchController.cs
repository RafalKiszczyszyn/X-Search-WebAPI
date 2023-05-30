
using NSwag.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace XSearch.WebApi.Controllers
{
  [ApiController]
  [Route("/search")]
  [OpenApiTag("Search Articles")]
  [Produces("application/json")]
  [Consumes("application/json")]
  [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ErrorResponseDto), Description = "Bad request")]
  [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseDto), Description = "Internal server error")]
  public class SearchController : ControllerBase
  {
    /// <summary>Advance Search</summary>
    [HttpGet("/search")]
    public ActionResult<string> AdvancedSearch()
    {
      return "Ok";
    }
  }

  public class ErrorResponseDto
  {
    public string Error { get; set; }
  }
}
