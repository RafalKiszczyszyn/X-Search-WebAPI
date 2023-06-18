
using System.Text.Json;
using System.Text.Json.Nodes;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using XSearch.WebApi.Common.Security;
using XSearch.WebApi.Domain;
using SearchQuery = XSearch.WebApi.Domain.SearchQuery;
using SortOrder = XSearch.WebApi.Domain.SortOrder;

namespace XSearch.WebApi.Implementations
{
  internal class ElasticSearch : ISearchEngine
  {
    private readonly string _baseUrl;
    private readonly string _index;

    public ElasticSearch(string baseUrl, string index)
    {
      _baseUrl = baseUrl;
      _index = index;
    }

    public async Task<SearchQueryResult> SearchAsync(SearchQuery query, ICredentialsProvider credentialsProvider)
    {
      var client = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(_baseUrl))
        .Authentication(new BasicAuthentication(credentialsProvider.Username!, credentialsProvider.Password!))
        .DefaultIndex(_index)
        .EnableDebugMode()
        .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
        .RequestTimeout(TimeSpan.FromMinutes(2)));
      
      var res = await client.SearchAsync<ArticleDoc>(s =>
      {
        s.Query(q => SetupQuery(q, query.Spec))
          .From(query.PageIndex * query.PageSize)
          .Size(query.PageSize);

        if (query.SortBy != null)
        {
          s.Sort(x => 
          {
            foreach (var field in query.SortBy)
            {
              x.Field(MapField(field.Field), z => z.Order(field.Order == SortOrder.Asc ? Elastic.Clients.Elasticsearch.SortOrder.Asc : Elastic.Clients.Elasticsearch.SortOrder.Desc));
            }
          });
        }
      });

      if (!res.IsValidResponse)
        throw new InvalidOperationException(res.DebugInformation);

      var matches = res.Hits
        .Select(x => new Article(
          long.Parse(x.Id), x.Source!.Title,
          x.Source.DateModified, x.Source.Categories,
          x.Source.Content)).ToList();

      return new SearchQueryResult(matches, res.Took, res.Total);
    }

    private static void SetupQuery<T>(
      QueryDescriptor<T> q, IQuerySpecification querySpecification)
    {
      switch (querySpecification)
      {
        case MustQuerySpecification spec:
        {
          var configure = spec.SubQueries
            .Select<IQuerySpecification, Action<QueryDescriptor<T>>>(
              subQuerySpec => sq => SetupQuery(sq, subQuerySpec))
            .ToArray();
          
          q.Bool(b =>
          {
            b.Must(configure);
            if (spec.Filter != null)
            {
              b.Filter(sq => SetupQuery(sq, spec.Filter));
            }
          });
          break;
        }
        case ShouldQuerySpecification spec:
        {
          var configure = spec.SubQueries
            .Select<IQuerySpecification, Action<QueryDescriptor<T>>>(
              subQuerySpec => sq => SetupQuery(sq, subQuerySpec))
            .ToArray();

          q.Bool(b =>
          {
            b.Should(configure);
            if (spec.Filter != null)
            {
              b.Filter(sq => SetupQuery(sq, spec.Filter));
            }
          });
          break;
        }
        case MatchQuerySpecification spec:
        {
          q.Bool(b =>
          {
            if (spec.Value == "*")
            {
              b.Must(m => m.MatchAll());
            }
            else
            {
              b.Must(m => m.MultiMatch(mm => mm
                .Fields(spec.Fields.Select(f => new Field(MapField(f))).ToArray())
                .Query(spec.Value)));
            }

            if (spec.Filter != null)
            {
              b.Filter(sq => SetupQuery(sq, spec.Filter));
            }
          });
          break;
        }
        case RangeQuerySpecification<double?> spec:
        {
          q.Bool(b =>
          {
            b.Must(m => m.Range(r => r.NumberRange(nr =>
            {
              nr.Field(MapField(spec.Field));
              if (spec.GreaterThanEqual != null)
                nr.Gte(spec.GreaterThanEqual);
              if (spec.LessThanEqual != null)
                nr.Lte(spec.LessThanEqual);
            })));

            if (spec.Filter != null)
            {
              b.Filter(sq => SetupQuery(sq, spec.Filter));
            }
          });
          break;
        }
        case RangeQuerySpecification<DateTime?> spec:
        {
          q.Bool(b =>
          {
            b.Must(m => m.Range(r => r.DateRange(dt =>
            {
              dt.Field(MapField(spec.Field));
              if (spec.GreaterThanEqual != null)
                dt.Gte(spec.GreaterThanEqual);
              if (spec.LessThanEqual != null)
                dt.Lte(spec.LessThanEqual);
            })));

            if (spec.Filter != null)
            {
              b.Filter(sq => SetupQuery(sq, spec.Filter));
            }
          });
          break;
        }
        default: throw new NotSupportedException("Not supported query specification");
      }
    }

    private static string MapField(string field)
    {
      return field switch
      {
        "keywords" => "categories",
        "revisionDate" => "dateModified",
        _ => field
      };
    }

    private class ArticleDoc
    {
      public string Id { get; set; }
      public string Title { get; set; }
      public DateTime DateModified { get; set; }
      public List<string> Categories { get; set; }
      public string Content { get; set; }
    }
  }
}
