
namespace XSearch.WebApi.Common.Utils
{
  internal interface IValueProvider<out T>
  {
    public T Value { get; }
  }

  internal class ValueProvider<T> : IValueProvider<T>
  {
    public T Value { get; }

    public ValueProvider(T value)
    {
      Value = value;
    }
  }
}
