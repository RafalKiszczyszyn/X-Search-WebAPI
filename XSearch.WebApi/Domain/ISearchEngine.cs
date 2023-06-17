
using XSearch.WebApi.Common.Security;

namespace XSearch.WebApi.Domain
{
  public interface ISearchEngine
  { 
    Task<SearchQueryResult> SearchAsync(SearchQuery query, ICredentialsProvider credentialsProvider);
  }
}
