using System.Diagnostics;
using System.Text.Json;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Transport;

var wd = "./input";
var credentials = ReadCredentials();

var client = new ElasticsearchClient(
  new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
    .Authentication(new BasicAuthentication(credentials.Username, credentials.Password))
    .EnableDebugMode()
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
    .RequestTimeout(TimeSpan.FromMinutes(2)));

try
{
  client.Indices.Create(new CreateIndexRequest(Indices.Index("wikipedia")));
  Console.WriteLine("Created index `wikipedia`.");
}
catch (Exception)
{
  Console.WriteLine("Index `wikipedia` already exists.");
}


var batches = Directory.GetFiles(wd);
foreach (var batch in batches)
{
  var articles = JsonSerializer.Deserialize<List<ArticleDoc>>(File.ReadAllText(batch))!;
  Console.WriteLine($"Found {articles.Count} articles in batch {batch}.");
  
  client.IndexMany(articles, "wikipedia");
  Console.WriteLine($"Batch {batch} successfully indexed.");
}

(string Username, string Password) ReadCredentials()
{
  Console.Write("Enter ElasticSearch user credentials (login:pass): ");
  var input = Console.ReadLine()!.Split(":");

  return (input[0], input[1]);
}

internal class ArticleDoc
{
  public string Id { get; set; }
  public string Title { get; set; }
  public DateTime DateModified { get; set; }
  public List<string> Categories { get; set; }
  public string Content { get; set; }
}
