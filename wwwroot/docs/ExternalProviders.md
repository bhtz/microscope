Sign-in with External Identity Providers
========================================

microscope support the following authentication strategies : 

* Local
* OpenIdConnect (OIDC)
* Microsoft (azure AD)
* Google
* Github
* Twitter
* LinkedIn
* Dropbox

OIDC
----

1. Register an application in your OIDC provider
2. Add `http://localhost:5000/signin-oidc` as an authorized redirect URI.
3. Configure microscope : 

```json
    "ExternalProviders": {
        "OIDC" : {
            "ClientId": "",
            "ClientSecret": ""
        }
    }
```

Azure AD
------------------------

1. Navigate to : [azure portal - App registrations](https://go.microsoft.com/fwlink/?linkid=2083908)
2. Select New registration
3. Add `http://localhost:5000/signin-azuread` as an authorized redirect URI.
4. Configure microscope : 

```json
    "ExternalProviders": {
        "AzureAd": {
            "Authority":"https://login.microsoftonline.com/<tenantid>/v2.0/",
            "ClientId": "<clientid>",
            "ClientSecret": "<secret>",
            "CallbackPath": "/signin-azuread"  ,
            "ResponseType":"code id_token",
            "RequireHttpsMetadata":false,
            "SaveTokens":true,
            "GetClaimsFromUserInfoEndpoint":true,
            "Scope":["email"]
        }
    }
```

Google
------

1. Navigate to : [console.developers.google.com](https://console.developers.google.com/)
2. Select Web server for our application environment and add `http://localhost:5000/signin-google` as an authorized redirect URI.
3. Configure microscope : 

```json
    "ExternalProviders": {
        "Google" : {
            "ClientId": "",
            "ClientSecret": ""
        }
    }
```

Github
------

1. Navigate to : [Github OAuth Apps](https://github.com/settings/developers)
2. Add `http://localhost:5000/signin-github` as an authorized redirect URI.
3. Configure microscope :

```json
    "ExternalProviders": {
        "Github" : {
            "ClientId": "",
            "ClientSecret": ""
        }
    }
```

Twitter
-------

1. Navigate to : [developer.twitter.com/apps](https://developer.twitter.com/apps)
2. Add `http://localhost:5000/signin-twitter` as an authorized redirect URI.
3. Configure microscope :

```json
    "ExternalProviders": {
        "Twitter" : {
            "ClientId": "",
            "ClientSecret": "",
            "RetrieveUserDetails": true
        }
    }
```

LinkedIn
--------

1. Navigate to : [linkedin.com/developers](https://www.linkedin.com/developers/)
2. Add `http://localhost:5000/signin-linkedin` as an authorized redirect URI.
3. Configure microscope :

```json
    "ExternalProviders": {
        "LinkedIn" : {
            "ClientId": "",
            "ClientSecret": ""
        }
    }
```

Dropbox
-------

1. Navigate to : [dropbox.com/developers/apps](https://www.dropbox.com/developers/apps)
2. Add `http://localhost:5000/signin-dropbox` as an authorized redirect URI.
3. Configure microscope :

```json
    "ExternalProviders": {
        "Dropbox" : {
            "ClientId": "",
            "ClientSecret": ""
        }
    }
```