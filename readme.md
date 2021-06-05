# Microscope

    WORK IN PROGRESS

Admin & API on top of awesomes OSS products in order to provide BaaS experience :

* Interactive programming & data exploration (jupyterLab)
* GraphQL & event engine (hasura)
* Identity and access management (keycloak)
* Storage engine (minio)

related to https://github.com/bhtz/microscope

## Requirements: 

* docker engine

## Getting started

    git clone https://github.com/bhtz/HKLM.git

    cd datastack

    docker-compose up

* [NAVIGATE TO PORTAL](http://localhost:8085)

Migration cmd : 

run to src/Microscope.Infrastructure : 

    dotnet ef --startup-project ../Microscope.Api/ migrations add InitialCreate -o Migrations

    dotnet ef --startup-project ../Microscope.Api/ database update

Generate controller : 

    dotnet aspnet-codegenerator controller -api -name RemoteConfigController -m RemoteConfig -dc MicroscopeDbContext