using System.Collections.Immutable;

namespace XSearch.WebApi.Domain
{
  public enum SortOrder
  {
    Asc, Desc
  }

  public interface IQuerySpecification
  {
    public IQuerySpecification? Filter { get; }
  }

  public class SortField
  {
    public string Field { get; }
    public SortOrder Order { get; }

    public SortField(string field, SortOrder order = SortOrder.Asc)
    {
      Field = field;
      Order = order;
    }
  }

  public class SearchQuery
  {
    public IQuerySpecification Spec { get; }
    public IEnumerable<SortField>? SortBy { get; }
    public int PageIndex { get; }
    public int PageSize { get; }

    public SearchQuery(
      IQuerySpecification spec, int pageIndex = 0, int pageSize = 10, 
      IEnumerable<SortField>? sortBy = null)
    {
      Spec = spec;
      PageIndex = pageIndex;
      PageSize = pageSize;
      SortBy = sortBy;
    }
  }

  public class SearchQueryResult
  {
    public ImmutableList<Article> Matches { get; }
    public long SearchTime { get; }
    public long TotalHits { get; }

    public SearchQueryResult(IEnumerable<Article> matches, long searchTime, long totalHits)
    {
      Matches = matches.ToImmutableList();
      SearchTime = searchTime;
      TotalHits = totalHits;
    }
  }

  internal class MustQuerySpecification : IQuerySpecification
  {
    public ImmutableList<IQuerySpecification> SubQueries { get; }
    public IQuerySpecification? Filter { get; }

    public MustQuerySpecification(
      IEnumerable<IQuerySpecification> subQueries, 
      IQuerySpecification? filter = null)
    {
      SubQueries = subQueries.ToImmutableList();
      Filter = filter;
    }
  }

  internal class ShouldQuerySpecification : IQuerySpecification
  {
    public ImmutableList<IQuerySpecification> SubQueries { get; }
    public IQuerySpecification? Filter { get; }

    public ShouldQuerySpecification(
      IEnumerable<IQuerySpecification> subQueries,
      IQuerySpecification? filter = null)
    {
      SubQueries = subQueries.ToImmutableList();
      Filter = filter;
    }
  }

  internal class MatchQuerySpecification : IQuerySpecification
  {
    public ImmutableHashSet<string> Fields { get; }
    public string Value { get; }
    public IQuerySpecification? Filter { get; }

    public MatchQuerySpecification(
      IEnumerable<string> fields, string value, 
      IQuerySpecification? filter = null)
    {
      Fields = fields.ToImmutableHashSet();
      Value = value;
      Filter = filter;
    }
  }

  internal abstract class RangeQuerySpecification<T> : IQuerySpecification
  {
    public string Field { get; }
    public T GreaterThanEqual { get; }
    public T LessThanEqual { get; }
    public IQuerySpecification? Filter { get; }

    protected RangeQuerySpecification(
      string field, T greaterThanEqual, T lessThanEqual, 
      IQuerySpecification? filter = null)
    {
      Field = field;
      GreaterThanEqual = greaterThanEqual;
      LessThanEqual = lessThanEqual;
      Filter = filter;
    }
  }

  internal class NumberRangeQuerySpecification : RangeQuerySpecification<double?>
  {
    public NumberRangeQuerySpecification(
      string field, double? greaterThanEqual, double? lessThanEqual, 
      IQuerySpecification? filter = null) : base(field, greaterThanEqual, lessThanEqual, filter)
    {
    }
  }

  internal class DateRangeQuerySpecification : RangeQuerySpecification<DateTime?>
  {
    public DateRangeQuerySpecification(
      string field, DateTime? greaterThanEqual, DateTime? lessThanEqual, IQuerySpecification? filter = null) : base(field, greaterThanEqual, lessThanEqual, filter)
    {
    }
  }
}
