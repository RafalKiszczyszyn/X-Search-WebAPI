
using XSearch.WebApi.Common.Security;

namespace XSearch.WebApi.Domain
{
  public interface ISearchEngine
  { 
    Task<List<Article>> SearchAsync(SearchQuery query, ICredentialsProvider credentialsProvider);
  }
}
