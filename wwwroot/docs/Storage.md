Storage adapter
===============

microscope support the following storage providers : 

* Host file system
* Azure blob storage

File system
----------

1. Configure microscope (appsettings OR env variable):

```json
    {
        "MCSP_FILE_ADAPTER": "filesystem",
        "MCSP_STORAGE_CONTAINER": "uploads"
    }
```

Azure Blob storage
------------------

1. Navigate to [portal azure - storage account](https://portal.azure.com/#create/Microsoft.StorageAccount-ARM)
3. Then create container
4. Configure microscope (appsettings OR env variable):

```json
    {
        "MCSP_FILE_ADAPTER": "blobstorage",
        "MCSP_STORAGE_CS": "<my-blob-storage-connection-string>",
        "MCSP_STORAGE_CONTAINER": "<container-name>"
    }
```