MICROSCOPE
==========

Backend as a service for ambitious digital factory (work in progress)

* postgres
* hasura
* Identity & SSO
* Storage
* Remote configs
* REST & GraphQL API
* ...


ROADMAP
=======

[roadmap](https://github.com/bhtz/microscope/blob/master/wwwroot/docs/roadmap.md)


REQUIREMENTS
============

Development :

* dotnet core SDK 3.1
* docker engine

GETTING STARTED
---------------

    git clone https://github.com/bhtz/microscope.git
    dotnet restore
    dotnet build
    update appsettings.json 
    dotnet run

SETTINGS
--------

```json
{
  "ConnectionStrings": {
    "IRONHASURA_DATA_CONNECTION_STRING": "User ID =;Password=;Server=;Port=5432;Database=hasura;Integrated Security=true;Pooling=true;",
    "IRONHASURA_IDENTITY_CONNECTION_STRING": "User ID =;Password=;Server=;Port=5432;Database=hasura;Integrated Security=true;Pooling=true;"
  },

  "IRONHASURA_CONSOLE_URL": "http://localhost:8080/console/data/schema/public",
  "IRONHASURA_FILE_ADAPTER": "filesystem",
  "IRONHASURA_BLOB_CS": "DefaultEndpointsProtocol=https;AccountName=;AccountKey=;EndpointSuffix=core.windows.net",
  "IRONHASURA_STORAGE_CONTAINER": "uploads",
  "IRONHASURA_AUTHORITY_ENDPOINT": "http://localhost:5000",
  "IRONHASURA_AUDIENCE": "ironhasura.api",
}
```
