

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export.Generation
{
  internal interface IDocumentGenerator
  {
    public bool IsDocumentConfigured(string documentName);

    public string CreateDocumentJson(string documentName);
  }
}