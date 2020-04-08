IRON HASURA
===========

BACKEND AS A SERVICE EXTENSION FOR HASURA GRAPHQL ENGINE

GETTING STARTED
===============


STACK FEATURES
==============

HASURA
------

- [x] GRAPHQL API
- [x] LIVE QUERIES
- [x] BAAS ADMIN
- [x] DOMAIN EVENTS
- [x] REMOTE SCHEMAS
- [x] ACTIONS

IRON HASURA
-----------

- [ ] AUTHENTICATION
  - [x] WEBHOOK
  - [ ] IDENTITY SERVER ?
- [ ] FILES
    - [x] blobstorage
    - [x] file system
    - [ ] aws S3
- [x] REMOTE CONFIGURATION
- [x] BASIC ANALYTICS
- [ ] PUSH NOTIFICATIONS
- [ ] INTEGRATION EVENTS
    - [ ] RabbitMQ
    - [ ] azure service bus
- [ ] MAILS
- [ ] CSV EXPORTS
- [ ] CSV IMPORT
- [ ] LOGS
  - [ ] CONSOLE
  - [ ] FILE
  - [ ] BLOBSTORAGE
  - [ ] APP INSIGHT
- [ ] COGNITIVE

SERVERLESS & LOGIC APP
----------------------

- [x] BUSINESS LOGIC
- [x] SCHEDULED TASKS
- [x] WORKFLOW

SETTINGS
--------

```json
{
  "ConnectionStrings": {
    "IRONHASURA_SQL_CONNECTION_STRING": ""
  },  
  "IRONHASURA_FILE_ADAPTER": "filesystem",
  "IRONHASURA_BLOB_CS": "",
  "IRONHASURA_STORAGE_CONTAINER": "uploads",
  "IRONHASURA_AUTHORITY_ENDPOINT": "http://localhost:8081/auth/realms/master",
  "IRONHASURA_AUDIENCE": "ironhasura"
}
```
