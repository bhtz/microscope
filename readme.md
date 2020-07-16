MICROSCOPE
==========

Backend as a service for ambitious digital factory (work in progress)

* Postgres
* GraphQL Engine (hasura)
* Identity federation gateway
* Cloud & local Storage
* Remote configurations
* Analytics
* Swagger & GraphQL playground

ROADMAP
=======

[roadmap](https://github.com/bhtz/microscope/blob/master/wwwroot/docs/roadmap.md)


REQUIREMENTS
============

* dotnet core SDK 3.1
* docker engine

GETTING STARTED
===============

Get the source code: 

    git clone https://github.com/bhtz/microscope.git

Run the following command :

    docker-compose up

Create the following Backend As A Service stack :

* Postgres 12 (Database)
* Hasura (Events & GraphQL Engine)
* Microscope (Identity, Storage, remote configs, analytics)

SETTINGS
--------

You can update microscope configuration using : appsettings.{environment}.yaml according to [asp net core configurations](https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1).


MCSP_DATA_CS (connection string)

    connection string for microscope BAAS data (remote configurations, analytics)

MCSP_IDENTITY_CS (connection string)

    connection string for microscope Identity Server data (users, roles, claims, ...)

MCSP_HASURA_CONSOLE_URL

    (string) URL for Hasura graphql engine

MCSP_FILE_ADAPTER

    (string) Key defining the file storage adapter, poossibilities are : filesystem, blobstorage

MCSP_STORAGE_CONTAINER

    (string) The folder name (blob container, fs directory) used as root for storage

MCSP_HOST

    (string) Host of microscope instance (used as authority endpoint for API JWT token validation scheme)

ExternalProviders

    (object) OIDC external auth providers with configurations (clientId, secret, authority)

```json
      "ExternalProviders": {
    "AAD" : {
      "Authority": "https://login.microsoftonline.com/<tenantid>/v2.0",
      "ClientId": "<clientid>",
      "ClientSecret": "<secret>"
    },
    "Google" : {
      "Authority": "",
      "ClientId": "",
      "ClientSecret": ""
    }
  },
```

Clients

    (array) Collection of application client for microscope stack, check [identity server docs](http://docs.identityserver.io/en/stable/reference/client.html) for more details

```json
  "Clients": [
    {
      "ClientId": "PWA",
      "ClientName": "Progressive Web App Client",
      "ClientUri": "https://mydomain.com",
      "AllowedGrantTypes": ["authorization_code"],
      "RedirectUris": ["https://mydomain.com/oidc-callback"],
      "PostLogoutRedirectUris": ["https://mydomain.com/logout"],
      "AllowedCorsOrigins": ["https://mydomain.com"],
      "AccessTokenLifetime": 3600,
      "ClientSecrets": [ { "Value": "" } ],
      "RequireConsent": false,
      "RequireClientSecret": false,
      "EnableLocalLogin": true,
      "AllowedScopes": [
        "openid",
        "profile",
        "email",
        "role",
        "ironhasura.api"
      ]
    }
  ]
```