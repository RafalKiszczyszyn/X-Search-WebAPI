using XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export.Generation;
using XSearch.WebApi.Properties;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Export
{
  internal class DocumentationExporter : IDocumentationExporter
  {
    private readonly IDocumentGenerator m_documentGenerator;
    private readonly string m_apiTitle;

    public DocumentationExporter(
      IDocumentGenerator documentGenerator,
      OpenApiInfo openApiInfo)
    {
      m_documentGenerator = documentGenerator;
      m_apiTitle = openApiInfo.Title;
    }

    public void ExportDocs(string outputDirectory)
    {
      var fileNames = new DocumentationFileNames(m_apiTitle, outputDirectory);

      ExportOpenApiSpecification(fileNames.OutputOpenApiFilePath);
      ExportReDocDocumentation(fileNames);
    }

    private void ExportOpenApiSpecification(string outputPath)
    {
      if (m_documentGenerator.IsDocumentConfigured("plain") == false)
        throw new Exception($"There was no document configured under name: 'plain'.");

      var plainOpenApi = m_documentGenerator.CreateDocumentJson("plain");
      File.WriteAllText(outputPath, plainOpenApi);
    }

    private void ExportReDocDocumentation(DocumentationFileNames fileNames)
    {
      if (m_documentGenerator.IsDocumentConfigured("full") == false)
        return;

      var openApiSpec = m_documentGenerator.CreateDocumentJson("full");
      GenerateStaticReDoc(fileNames, openApiSpec);
    }

    private void GenerateStaticReDoc(DocumentationFileNames fileNames, string openApiSpec)
    {
      var reDocOptions = ReDocOptions.GetJson();

      var staticHtmlFile = Resources.redoc_template
        .Replace("{{Title}}", m_apiTitle)
        .Replace("{{OpenApiSpec}}", openApiSpec)
        .Replace("{{ReDocOptions}}", reDocOptions);

      File.WriteAllText(fileNames.OutputReDocFilePath, staticHtmlFile);
    }

    private class DocumentationFileNames
    {
      public DocumentationFileNames(string apiTitle, string outputDirectory)
      {
        ApiTitle = apiTitle;
        OutputDirectory = outputDirectory;
      }

      public string ApiTitle { get; }

      public string OutputDirectory { get; }

      public string OutputOpenApiFilePath
        => Path.Combine(OutputDirectory, $"{ApiTitle}.json");

      public string OutputReDocFileName
        => $"{ApiTitle}.html";

      public string OutputReDocFilePath
        => Path.Combine(OutputDirectory, OutputReDocFileName);
    }
  }
}
