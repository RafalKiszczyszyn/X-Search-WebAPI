### Search Engine configuration
Connection to the underlying search engine can be configured in `./XSearch.WebApi/appsettings.json`. Default values are:
```
{
  "SearchEngineBaseUrl": "https://localhost:9200/wikipedia/_search",
  "SearchEngineIndex": "wikipedia"
}
```
If run in container, localhost must be replaced by accessible address.

### Run in container
```
cd ./XSearch.WebApi
docker build -t x-search-webapi:1.0 -f Dockerfile .
docker run --name x-search-webapi -p 5000:80 x-search-webapi:1.0
```

### Request examples
For full API documentation, refer to `X-Search WebAPI.html`.

Search for the phrase "Eduaction" in the title and content:
```
{
  "query": {
    "match": {"fields": ["Title", "Content"], "value": "Education"}
  }
},
```

Search for documents revised after 2022 or having word "rat" in the title:
```
{
  "query": {
      "should": [
          {"match": {"fields": ["Title"], "value": "rat"}},
          {"range": {"field": "RevisionDate", "gte": "2022-01-01T00:00:00Z"}}
      ]
  }
}
```

### Limitations
Sorting works only for RevisionDate field by default. If sorting by other fields is required, they must be configured to be sortable in ElasticSearch.