V1 architecture
===============

* Microscope.Admin
    * add storage
* Microscope.Api
    * add storage
* Microscope.GraphQL
    * users
    * roles
    * storage
    * clients
    * ...
* Microscope.EntityFramework.MySql
* Microscope.EntityFramework.PostgreSQL
* Microscope.EntityFramework.Shared
* Microscope.EntityFramework.SqlServer
* Microscope.STS.Identity
    * external providers
* Microscope.Storage
    * Storage interface
    * Storage implementation


UX DESIGN
=========

ADMIN

* Identity
    * Users
    * Roles
    * External Providers (readonly)
* Applications
    * Clients
* STS
    * Identity ressources
    * Persisted grants
    * Api ressources
* Storage
* Database
* Api
    * REST
    * GraphQL
* Logs
* Account
    * manage
    * logout