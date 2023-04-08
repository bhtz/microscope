# Microscope

MULTI CLOUD BACKEND AS A SERVICE

    EXPERIMENTAL / WORK IN PROGRESS

Admin & API on top of awesome OSS products in order to provide Backend As A Service (BaaS) experience :

* GraphQL api engine (Hasura)
* REST api engine (PostgREST)
* Identity and access management (Keycloak)
* Storage engine (Minio)
* Interactive programming & data exploration (JupyterLab)

### Inspired by :

* Hasura
* Parse server
* Directus
* Pocketbase
* Firebase
* Supabase
* Appwrite
* Strapi
* and many others

## Requirements: 

* docker engine

## Getting started

    git clone https://github.com/bhtz/microscope

    cd microscope

    dotnet publish -r linux-arm64

    docker-compose up

* [OPEN CONSOLE APPLICATION](http://localhost:8086)

### HELPERS :

Migration cmd : 

run to src/Microscope.Infrastructure : 

    dotnet ef --startup-project ../Microscope.Api/ migrations add InitialCreate -o Migrations

    dotnet ef --startup-project ../Microscope.Api/ database update

Generate controller : 

    dotnet aspnet-codegenerator controller -api -name RemoteConfigController -m RemoteConfig -dc MicroscopeDbContext

MIGRATIONS
----------

Update environment :

    export ASPNETCORE_ENVIRONMENT=Development
    export ASPNETCORE_ENVIRONMENT=Production

Publish :

    dotnet publish -c Release

GENERATE READ DBCONTEXT
-----------------------

dotnet ef dbcontext scaffold "Host=localhost;Database=mydatabase;Username=myuser;Password=mypassword" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -n MyReadDbContext

NUMBER OF CODE LIGNE
--------------------

git ls-files | xargs wc -l

ROADMAP
=======

* Services : 
    * Reverse proxy -- OK
        * Yarp -- OK
        * Envoy
    * Identity
        * Keycloak -- OK
        * OpenIddict -- TARGET
    * Storage -- OK
        * S3  
        * Blob storage -- OK
        * filesystem -- OK
        * minio -- OK
    * Remote configs -- OK
        * Feature flipping -- OK
        * JSON Remote configs -- OK
        * Analytics -- OK
    * API
        * postgrest -- OK
        * JSON Server -- OK
        * mcsp_api_engine -- TARGET
    * Schema
        * DSL schema
            * database
                * schema
                    * Table
                        * Fields
                            * Type
                            * Widget
    * Back office 
      * Introspect microscope schema
        * Aka directus
    * Functions
        * azure functions
        * serverless
    * Automation -- OK
        * Workflows
            * Elsa -- OK
            * Logic app
        * Webhooks -- OK
* Clients
    * Blazor
        * Storage admin
        * Schema admin
        * Function admin
        * Workflows admin
        * Identity admin
        * Automation admin
