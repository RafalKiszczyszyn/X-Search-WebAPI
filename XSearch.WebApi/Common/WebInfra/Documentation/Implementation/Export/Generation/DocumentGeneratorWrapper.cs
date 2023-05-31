using Newtonsoft.Json;
using NJsonSchema;
using NSwag.Generation;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export.Generation
{
  internal class DocumentGeneratorWrapper : IDocumentGenerator
  {
    private readonly IReadOnlyCollection<string> m_registeredDocuments;
    private readonly IOpenApiDocumentGenerator m_documentGenerator;

    public DocumentGeneratorWrapper(
      IEnumerable<string> registeredDocuments,
      IOpenApiDocumentGenerator documentGenerator)
    {
      m_registeredDocuments = registeredDocuments.ToList().AsReadOnly();
      m_documentGenerator = documentGenerator;
    }

    public bool IsDocumentConfigured(string documentName)
      => m_registeredDocuments.Contains(documentName);

    public string CreateDocumentJson(string documentName)
    {
      if (IsDocumentConfigured(documentName) == false)
      {
        throw new InvalidOperationException(
          $"Cannot generate document '{documentName}' because it is not configured.");
      }

      return m_documentGenerator.GenerateAsync(documentName).Result
        .ToJson(SchemaType.OpenApi3, Formatting.Indented);
    }
  }
}