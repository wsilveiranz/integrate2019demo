# integrate2019demo

Claim Check Pattern and Application Insight utility code used during Integrate 2019 demo

you will need the following details in you local.setting.json

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "APPINSIGHTS_INSTRUMENTATIONKEY": "<app insights instrumentation key",
    "AzureWebJobsStorage": "<storage account connection string>",
    "servicebusconn": "<service bus connection string>",
    "storageAccountName": "<name of the storage account for claim check pattern>",
    "storageAccountKey": "<storage account key for the claim check pattern>",
    "blobContainerName": "<blob container name for the claim check patterns>",
    "serviceBusEndPoint": "sb://<yournamespace>.servicebus.windows.net/",
    "serviceBusEntityPath": "<the queue used for claim check patterns>",
    "serviceBusKeyName": "<service bus Access Key Name>",
    "serviceBusSASKey": "<service bus Access Key Value>"
  }
}
```
