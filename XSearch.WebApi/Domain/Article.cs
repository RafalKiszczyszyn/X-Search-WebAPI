
namespace XSearch.WebApi.Domain
{
  public class Article
  {
    public long Id { get; }
    public string Title { get; }
    public DateTime RevisionDate { get; }
    public List<string> Keywords { get; }
    public string Content { get; }

    public Article(long id, string title, DateTime revisionDate, List<string> keywords, string content)
    {
      Id = id;
      Title = title;
      RevisionDate = revisionDate;
      Keywords = keywords;
      Content = content;
    }
  }
}
