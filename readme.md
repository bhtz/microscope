MICROSCOPE
==========

![MICROSCOPE](https://github.com/bhtz/microscope/blob/master/wwwroot/home.png)

Digital Platform Factory (alpha)

* Postgres
* GraphQL Engine (hasura)
* Identity federation gateway
* Cloud & local Storage
* Remote configurations
* Analytics
* Swagger & GraphQL playground
* Cloud functions

BETA ROADMAP
============

[roadmap](https://github.com/bhtz/microscope/blob/master/wwwroot/docs/roadmap.md)


DEV REQUIREMENTS
================

* dotnet core SDK 3.1
* docker engine

GETTING STARTED
===============

Get the source code: 

    git clone https://github.com/bhtz/microscope.git

Run the following command :

    docker-compose up

Launch the following Backend As A Service stack :
* Postgres 12 (Database)
* Hasura (Domain Events & GraphQL Engine)
* Microscope (Identity, Storage, remote configs, analytics, ...)


Navigate to [http://localhost:5000/](http://localhost:5000/) and enjoy your backend as a service experience !

SETTINGS
--------

* [Storage providers](https://github.com/bhtz/microscope/blob/master/wwwroot/docs/Storage.md)
* [External auth providers](https://github.com/bhtz/microscope/blob/master/wwwroot/docs/ExternalProviders.md)