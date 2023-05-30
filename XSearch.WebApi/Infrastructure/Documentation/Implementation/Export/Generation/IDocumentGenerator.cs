//  Copyright © Titian Software Ltd

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Export.Generation
{
  internal interface IDocumentGenerator
  {
    public bool IsDocumentConfigured(string documentName);

    public string CreateDocumentJson(string documentName);
  }
}