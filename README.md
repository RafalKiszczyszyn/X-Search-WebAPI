### Run in container
```
cd ./XSearch.WebApi
docker build -t x-search-webapi:1.0 -f Dockerfile .
docker run --name x-search-webapi -p 5000:80 x-search-webapi:1.0
```