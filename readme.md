IRON HASURA
===========

A (work in progress) full Backend As A Service in one docker compose file - play with hasura GraphQL engine

ROADMAP
=======

[roadmap](https://github.com/bhtz/IronHasura/blob/master/roadmap.md)


GETTING STARTED
---------------

    docker-compose up


REQUIREMENTS
============

Run : 

* docker engine

Development :

* .NET Core SDK 3.1


SETTINGS
--------

```json
{
  "ConnectionStrings": {
    "IRONHASURA_DATA_CONNECTION_STRING": "User ID =username;Password=password;Server=host;Port=5432;Database=hasura;Integrated Security=true;Pooling=true;",
    "IRONHASURA_IDENTITY_CONNECTION_STRING": "User ID =username;Password=password;Server=host;Port=5432;Database=hasura;Integrated Security=true;Pooling=true;"
  },
  "IRONHASURA_CONSOLE_URL": "http://localhost:8080",
  
  "IRONHASURA_FILE_ADAPTER": "filesystem",
  "IRONHASURA_BLOB_CS": "DefaultEndpointsProtocol=https;AccountName=account;AccountKey=mykey;EndpointSuffix=core.windows.net",
  "IRONHASURA_STORAGE_CONTAINER": "uploads",
  
  "IRONHASURA_AUTHORITY_ENDPOINT": "http://localhost:8081/auth/realms/master",
  "IRONHASURA_AUDIENCE": "ironhasura"
}
```
