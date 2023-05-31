
using NSwag.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using XSearch.WebApi.Common.WebInfra.ErrorHandling;
using XSearch.WebApi.DTOs;

namespace XSearch.WebApi.Controllers
{
  [ApiController]
  [Route("/search")]
  [OpenApiTag("Search Articles")]
  [Produces("application/json")]
  [Consumes("application/json")]
  [SwaggerResponse(HttpStatusCode.OK, typeof(QueryResultDto), Description = "Successful search")]
  [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ErrorResponseDto), Description = "Bad request")]
  [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseDto), Description = "Internal server error")]
  public class SearchController : ControllerBase
  {
    private static readonly string LoremIpsum =
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sit amet sapien a ipsum sollicitudin malesuada vel non nunc. Aliquam quis justo vel est dictum pellentesque. Mauris tempor tellus at suscipit tincidunt. Sed commodo eros mauris, eget varius orci pulvinar tincidunt. Vivamus nec ullamcorper lorem. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis pellentesque vitae dolor ac bibendum. Nam ligula diam, bibendum sollicitudin ligula quis, volutpat dignissim est. Nulla dignissim sem libero, sit amet varius massa varius in.\r\n\r\nSed vel tempor neque. Cras posuere imperdiet rutrum. Fusce vel eleifend arcu. Phasellus egestas in magna sed sodales. Proin at magna ac purus ultricies convallis. Mauris at finibus est. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam non diam nec lectus tincidunt bibendum ut id elit. Praesent placerat metus est, in vehicula nibh interdum vitae.\r\n\r\nSed blandit lectus lectus, eu dignissim libero porttitor tempus. Sed vulputate orci eget libero tristique bibendum. Etiam ut lacus eleifend, gravida nisl vel, rutrum sapien. Praesent non ultrices nunc, ut aliquam purus. Sed est ipsum, luctus quis velit nec, scelerisque egestas augue. Praesent consectetur urna risus, eu tristique est rutrum a. Aenean id mattis eros, eget ornare felis. Cras auctor finibus libero. Nam ut vestibulum nibh. Duis feugiat nunc et facilisis pulvinar. Fusce dictum pulvinar augue a sodales. Proin euismod dui nec mattis tincidunt. Aenean quis turpis sodales, tristique est nec, commodo felis. Quisque posuere viverra ornare. Morbi volutpat sapien vitae enim lacinia, in pretium felis bibendum.\r\n\r\nSed sed pellentesque turpis. Quisque porttitor consectetur volutpat. Sed libero leo, suscipit et ipsum luctus, ultricies finibus ex. Aenean ut bibendum augue. Phasellus cursus volutpat eros nec malesuada. Proin mollis sed ligula nec pharetra. Proin ornare ultrices nulla, ut condimentum diam vestibulum id.\r\n\r\nPellentesque non consectetur lectus. Fusce facilisis elementum odio, at maximus augue suscipit ac. Etiam eu est urna. Vivamus at turpis metus. Ut ut orci a tellus efficitur posuere eleifend sit amet nunc. Nullam quis accumsan dolor. Duis lorem nunc, scelerisque quis commodo id, fermentum eget dui. In venenatis tellus nec erat tempor cursus. Donec sit amet ligula suscipit, blandit est quis, pretium diam. Praesent nec tincidunt odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque congue est in lacus blandit luctus. Sed ornare libero ultricies nunc tincidunt feugiat. Fusce malesuada magna ut nisl venenatis scelerisque. Nullam ipsum enim, pretium id lacus non, pretium accumsan est. Vivamus ut purus pulvinar, porttitor elit sed, lobortis lorem.";

    private static readonly List<ArticleDto> _articles = new()
    {
      new ArticleDto
      {
        Id = 1,
        Keywords = new List<string> {"Education", "Education in Canada"},
        Content = LoremIpsum,
        RevisionDate = DateTime.Now,
        Title = "Education in Canada"
      },
      new ArticleDto
      {
        Id = 2,
        Keywords = new List<string> {"Education", "Education in Poland"},
        Content = LoremIpsum,
        RevisionDate = DateTime.Now,
        Title = "Education in Poland"
      },
      new ArticleDto
      {
        Id = 3,
        Keywords = new List<string> {"Education", "Education in India"},
        Content = LoremIpsum,
        RevisionDate = DateTime.Now,
        Title = "Education in India"
      }
    };

    private static readonly Random _rand = new();

    /// <summary>Advanced Search</summary>
    [HttpPost("/search")]
    public ActionResult<QueryResultDto> AdvancedSearch([FromBody] QueryRequestDto queryRequestDto)
    {
      return new QueryResultDto
      {
        matches = _articles.OrderBy(_ => _rand.Next()).ToList()
      };
    }
  }
}
