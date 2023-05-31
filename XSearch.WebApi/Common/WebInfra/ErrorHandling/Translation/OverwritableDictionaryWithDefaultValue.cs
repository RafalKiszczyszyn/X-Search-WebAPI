

using System.Collections;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Translation
{
  /// <summary>
  /// Dictionary that allows its implementing classes to overwrite its values.
  /// It also returns default value if the key is not found.
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  public class OverwritableDictionaryWithDefaultValue<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
  {
    private readonly TValue m_defaultValue;
    private readonly Dictionary<TKey, TValue> m_dictionary;

    /// <summary>
    /// Creates new instance of <see cref="OverwritableDictionaryWithDefaultValue{TKey,TValue}"/> class.
    /// </summary>
    /// <param name="defaultValue"></param>
    public OverwritableDictionaryWithDefaultValue(TValue defaultValue)
    {
      m_defaultValue = defaultValue;
      m_dictionary = new Dictionary<TKey, TValue>();
    }

    /// <inheritdoc/>
    public TValue this[TKey key] => GetValue(key);

    /// <inheritdoc/>
    public int Count => m_dictionary.Count;

    /// <inheritdoc/>
    public IEnumerable<TKey> Keys => m_dictionary.Keys;

    /// <inheritdoc/>
    public IEnumerable<TValue> Values => m_dictionary.Values;

    /// <summary>
    /// Adds or updates the key in the dictionary with provided value.
    /// </summary>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">New value.</param>
    public void Add(TKey key, TValue value)
      => m_dictionary[key] = value;

    /// <inheritdoc/>
    public bool ContainsKey(TKey key)
      => m_dictionary.ContainsKey(key);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
      => m_dictionary.GetEnumerator();

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      => m_dictionary.GetEnumerator();

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, out TValue value)
    {
      value = GetValue(key);
      return true;
    }

    private TValue GetValue(TKey key)
    {
      var valueFound = m_dictionary.TryGetValue(key, out var value);

      return valueFound
        ? value
        : m_defaultValue;
    }
  }
}
