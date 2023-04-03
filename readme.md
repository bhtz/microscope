# Microscope

    EXPERIMENTAL / WORK IN PROGRESS

Admin & API on top of awesomes OSS products in order to provide BaaS experience :

* GraphQL api engine (hasura)
* REST api engine (postgREST)
* Identity and access management (keycloak)
* Storage engine (minio)
* Interactive programming & data exploration (jupyterLab)

related to https://github.com/bhtz/microscope

## Requirements: 

* docker engine

## Getting started

    git clone https://github.com/bhtz/microscope

    cd microscope

    dotnet publish

    docker-compose up

* [NAVIGATE TO PORTAL](http://localhost:8085)

Migration cmd : 

run to src/Microscope.Infrastructure : 

    dotnet ef --startup-project ../Microscope.Api/ migrations add InitialCreate -o Migrations

    dotnet ef --startup-project ../Microscope.Api/ database update

Generate controller : 

    dotnet aspnet-codegenerator controller -api -name RemoteConfigController -m RemoteConfig -dc MicroscopeDbContext

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
        * hasura
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
