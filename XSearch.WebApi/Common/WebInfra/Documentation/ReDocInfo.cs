

using XSearch.WebApi.Extensions;

namespace XSearch.WebApi.Common.WebInfra.Documentation
{
  internal class ReDocInfo
  {
    public string AssemblyFileVersion { get; }

    public IReadOnlyList<string> TagsOrder { get; }

    public IReadOnlyList<DocumentationSection> DocumentationSections { get; }

    public ReDocInfo(
      string assemblyFileVersion,
      IEnumerable<string>? tagsOrder = null,
      IEnumerable<DocumentationSection>? documentationSections = null)
    {
      if (string.IsNullOrWhiteSpace(assemblyFileVersion))
      {
        throw new ArgumentNullException(nameof(assemblyFileVersion));
      }

      var evaluatedTagsOrder = (tagsOrder?.ToList() ?? new List<string>()).AsReadOnly();

      if (evaluatedTagsOrder.IsUnique() == false)
      {
        throw new Exception($"Found duplicate values in {nameof(tagsOrder)}.");
      }

      var evaluatedDocumentationSections = (documentationSections?.ToList() ?? new List<DocumentationSection>()).AsReadOnly();

      if (AreSectionTitlesDuplicated(evaluatedDocumentationSections))
      {
        throw new Exception($"Found duplicate titles in {nameof(documentationSections)}.");
      }

      AssemblyFileVersion = assemblyFileVersion;
      TagsOrder = evaluatedTagsOrder;
      DocumentationSections = evaluatedDocumentationSections;
    }

    private static bool AreSectionTitlesDuplicated(IEnumerable<DocumentationSection> documentationSections)
    {
      return documentationSections
        .Select(section => section.Title)
        .IsUnique() == false;
    }
  }
}