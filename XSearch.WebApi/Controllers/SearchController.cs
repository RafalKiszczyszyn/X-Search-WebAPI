
using NSwag.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using XSearch.WebApi.Common.Security;
using XSearch.WebApi.Common.WebInfra.ErrorHandling;
using XSearch.WebApi.Domain;
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
  [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
  public class SearchController : ControllerBase
  {
    private readonly ISearchEngine _engine;
    private readonly ICredentialsProvider _credentialsProvider;
    
    public SearchController(
      ISearchEngine engine,
      ICredentialsProvider provider)
    {
      _engine = engine;
      _credentialsProvider = provider;
    }

    /// <summary>Advanced Search</summary>
    /// <remarks>Requires basic authentication header with credentials used by search engine.</remarks>>
    [HttpPost("/search")]
    [Authorize(AuthenticationSchemes = "Basic")]
    public async Task<ActionResult<QueryResultDto>> AdvancedSearchAsync([FromBody] QueryRequestDto queryRequestDto)
    {
      var res = await _engine.SearchAsync(ParseRequest(queryRequestDto), _credentialsProvider);

      return new QueryResultDto
      {
        matches = res.Select(x => new ArticleDto
        {
          Id = x.Id,
          Title = x.Title,
          RevisionDate = x.RevisionDate,
          Content = x.Content,
          Keywords = x.Keywords
        }).ToList(),
      };
    }

    private static SearchQuery ParseRequest(QueryRequestDto requestDto)
    {
      return new SearchQuery(
        ParseQuerySpecification(requestDto.Query), 
        requestDto.PageIndex, requestDto.PageSize, requestDto.SortBy?.Select(x => new SortField(x.Field, x.Order)));
    }

    private static IQuerySpecification ParseQuerySpecification(QuerySpecificationDto specDto)
    {
      var specs = new List<IQuerySpecification>();
      
      if (specDto.Must != null)
      {
        specs.Add(new MustQuerySpecification(
          specDto.Must.Select(ParseQuerySpecification),
          specDto.Filter != null ? ParseQuerySpecification(specDto.Filter) : null));
      }

      if (specDto.Should != null)
      {
        specs.Add(new ShouldQuerySpecification(
          specDto.Should.Select(ParseQuerySpecification),
          specDto.Filter != null ? ParseQuerySpecification(specDto.Filter) : null));
      }

      if (specDto.Match != null)
      {
        specs.Add(new MatchQuerySpecification(
          specDto.Match.Fields, specDto.Match.Value,
          specDto.Filter != null ? ParseQuerySpecification(specDto.Filter) : null));
      }

      if (specDto.Range != null)
      {
        try
        {
          var gte = specDto.Range.Gte != null ? ParseValueAs<double>(specDto.Range.Gte) : (double?) null;
          var lte = specDto.Range.Lte != null ? ParseValueAs<double>(specDto.Range.Lte) : (double?) null;
          specs.Add(new NumberRangeQuerySpecification(
            specDto.Range.Field, gte, lte,
            specDto.Filter != null ? ParseQuerySpecification(specDto.Filter) : null));
        }
        catch (Exception)
        {
          try
          {
            var gte = specDto.Range.Gte != null ? ParseValueAs<DateTime>(specDto.Range.Gte) : (DateTime?)null;
            var lte = specDto.Range.Lte != null ? ParseValueAs<DateTime>(specDto.Range.Lte) : (DateTime?)null;
            specs.Add(new DateRangeQuerySpecification(
              specDto.Range.Field, gte, lte,
              specDto.Filter != null ? ParseQuerySpecification(specDto.Filter) : null));
          }
          catch (Exception)
          {
            throw new Exception("Invalid range query specification");
          }
        }
      }

      return specs.Count == 1 ? specs.Single() : new MustQuerySpecification(specs);
    }

    private static T ParseValueAs<T>(string value)
    {
      var x = JsonSerializer.Deserialize<JsonValue>($"\"{value.Replace("\"", "\\\"")}\"", new JsonSerializerOptions
      {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
      });

      return x.GetValue<T>();
    }
  }
}
