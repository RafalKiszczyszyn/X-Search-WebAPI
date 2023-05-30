namespace XSearch.WebApi.Infrastructure.Startup
{
  /// <summary>
  /// Determines version of the Open API specification.
  /// </summary>
  public class ContractVersion
  {
    /// <summary>
    /// Major version. Should be incremented if contract change is not backward-compatible. Note: incrementing <see cref="Major"/> should result in setting <see cref="Minor"/> to 0.
    /// </summary>
    public int Major { get; }

    /// <summary>
    /// Minor version. Should be incremented if contract change is backward-compatible.
    /// </summary>
    public int Minor { get; }

    /// <summary>
    /// Annotation. Can be used to mark the contract version as pre-release.
    /// </summary>
    public string? Annotation { get; }

    /// <summary>
    /// Creates new instance of <see cref="ContractVersion"/>.
    /// </summary>
    /// <param name="major">Major version.</param>
    /// <param name="minor">Minor version.</param>
    /// <param name="annotation">Annotation. Can be used to mark the contract version as pre-release.</param>
    public ContractVersion(int major, int minor, string? annotation = null)
    {
      AssertValueIsNotNegative(nameof(Major), major);
      AssertValueIsNotNegative(nameof(Minor), minor);
      AssertVersionIsCorrect(major, minor);
      AssertAnnotationIsCorrect(annotation);

      Major = major;
      Minor = minor;
      Annotation = annotation?.Trim();
    }

    /// <summary>
    /// Determines whether <see cref="ContractVersion"/> is equal to another <see cref="object"/>.
    /// </summary>
    /// <param name="anotherObject">Object to compare.</param>
    public override bool Equals(object? anotherObject)
    {
      if (anotherObject is ContractVersion anotherContractVersion)
      {
        return anotherContractVersion.Major == Major
          && anotherContractVersion.Minor == Minor
          && anotherContractVersion.Annotation?.ToUpperInvariant() == Annotation?.ToUpperInvariant();
      }

      return false;
    }

    /// <summary>
    /// Returns hash code representation of <see cref="ContractVersion"/>.
    /// </summary>
    public override int GetHashCode()
    {
      var annotationHashCode = Annotation?.GetHashCode() ?? 0;
      return Major.GetHashCode() ^ Minor.GetHashCode() ^ annotationHashCode;
    }

    /// <summary>
    /// Returns <see cref="string"/> representation of <see cref="ContractVersion"/>.
    /// </summary>
    public override string ToString()
    {
      var stringRepresentation = $"{Major}.{Minor}";

      if (Annotation != null)
      {
        stringRepresentation += $"-{Annotation}";
      }

      return stringRepresentation;
    }

    private static void AssertValueIsNotNegative(string parameterName, int value)
    {
      if (value < 0)
      {
        throw new ArgumentException($"Expected {parameterName} to be non-negative value, but found: '{value}'.");
      }
    }

    private static void AssertVersionIsCorrect(int major, int minor)
    {
      if (major == 0 && minor == 0)
      {
        throw new ArgumentException("Invalid contract version: 0.0.");
      }
    }

    private static void AssertAnnotationIsCorrect(string? annotation)
    {
      if (annotation == null)
      {
        return;
      }

      if (string.IsNullOrWhiteSpace(annotation))
      {
        throw new ArgumentException("Annotation cannot be empty or whitespace.");
      }
    }
  }
}
