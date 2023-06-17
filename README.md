### Search Engine configuration
XSearch.WebAPI is compatible with the [x-search-elasticsearch:8.8.1](https://hub.docker.com/r/rafalkiszczyszyn/x-search-elasticsearch) search engine image. Connection to the underlying search engine can be configured in `./XSearch.WebApi/appsettings.json`. Default values are:
```
{
  "SearchEngineBaseUrl": "https://elasticsearch:9200/wikipedia/_search",
  "SearchEngineIndex": "wikipedia"
}
```
__Notice__ `elasticsearch`  host should point to the machine running ElasticSearch.

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
    "match": {"fields": ["title", "content"], "value": "Education"}
  }
},
```

Search for documents revised after 2022 or having word "rat" in the title:
```
{
  "query": {
      "should": [
          {"match": {"fields": ["title"], "value": "rat"}},
          {"range": {"field": "revisionDate", "gte": "2022-01-01T00:00:00Z"}}
      ]
  }
}
```

### Limitations
Sorting works only for RevisionDate field by default. If sorting by other fields is required, they must be configured to be sortable in ElasticSearch.
