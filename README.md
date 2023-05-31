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

Search for documents revised after date 01.01.2020, having "Refactoring" or "Code smells" keyword, filtered to author R. Kiszcz:
```
{
  "query": {
    "must": [
      {
        "range": {"field": "revisionDate", "gte": "01.01.2020"}
      },
      {
        "should": [
          {
            "match": {"fields": ["keywords"], "value": "Refactoring"}
          },
          {
            "match": {"fields": ["keywords"], "value": "Code smells"}
          }
        ] 
      }
    ],
    "filter": {
      "match": {"fields": ["author"], "value": "R. Kiszcz"}
    }
  }
}
```