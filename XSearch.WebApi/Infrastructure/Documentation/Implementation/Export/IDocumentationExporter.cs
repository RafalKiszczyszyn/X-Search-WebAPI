//  Copyright © Titian Software Ltd

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Export
{
  internal interface IDocumentationExporter
  {
    void ExportDocs(string outputDirectory);
  }
}