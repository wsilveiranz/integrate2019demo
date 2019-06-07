using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.ServiceBus;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Integrate2019Demo.Utils
{
    public static class BlobStorageUtils
    {

        ///// <summary>
        ///// Upload the blob in to an azure blob container.
        ///// </summary>
        ///// <param name="containerName"></param>  
        ///// <param name="fileName"></param>
        ///// <param name="data"></param>
        ///// <param name="storageAccountName"></param>
        ///// <returns>bool</returns>
        public static async Task UploadBlob(string containerName, string fileName, string data, string storageAccountName,string storageAccountKey)
        {
            try
            {
                //get the MSI access token for the blob storage
                //string accessToken = await GetTokensFromAppConfiguration("AzureStorageResource");
                //string accessToken = await GetMSITokenAsync("AzureStorageResource");
                //var tokenCredential = new Microsoft.WindowsAzure.Storage.Auth.TokenCredential(accessToken);
                //var storageCredentials = new StorageCredentials(tokenCredential);
                var storageCredentials = new StorageCredentials(storageAccountName, storageAccountKey);
                // Define the blob to read
                CloudBlockBlob blob = new CloudBlockBlob(new Uri($"https://{storageAccountName}.blob.core.windows.net/{containerName}/{fileName}"), storageCredentials);

                blob.Properties.ContentType = "text/plain";

                //upload the blob
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    await blob.UploadFromStreamAsync(stream);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }

        ///// <summary>
        ///// Delete the blob in to an azure blob container.
        ///// </summary>
        ///// <param name="containerName"></param>  
        ///// <param name="fileName"></param>
        ///// <param name="storageAccountName"></param>
        ///// <returns>bool</returns>
        public static async Task<bool> DeleteBlob(string containerName, string fileName, string storageAccountName, string storageAccountKey)
        {
            bool retVal = false;
            try
            {
                //get the MSI access token for the blob storage
                //string accessToken = await GetTokensFromAppConfiguration("AzureStorageResource");
                //string accessToken = await GetMSITokenAsync("AzureStorageResource");
                //var tokenCredential = new Microsoft.WindowsAzure.Storage.Auth.TokenCredential(accessToken);
                var storageCredentials = new StorageCredentials(storageAccountName, storageAccountKey);
                // Define the blob to read
                CloudBlockBlob blob = new CloudBlockBlob(new Uri($"https://{storageAccountName}.blob.core.windows.net/{containerName}/{fileName}"), storageCredentials);

                // Delete the blob.
                await blob.DeleteAsync();
                retVal = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
            return retVal;
        }

         ///// <summary>
        ///// Read the stream of a given file from the given azure blob container sync.
        ///// </summary>
        ///// <param name="containerKeyName"></param> 
        ///// <param name="fileName"></param>
        ///// <returns>Stream</returns>
        public static async Task<string> GetBlob(string containerName, string fileName, string storageAccountName, string storageAccountKey)
        {
            string blobContent;
            try
            {
                //get the MSI access token for the blob storage
                //string accessToken = await GetTokensFromAppConfiguration("AzureStorageResource");
                //string accessToken = await GetMSITokenAsync("AzureStorageResource");
                //var tokenCredential = new Microsoft.WindowsAzure.Storage.Auth.TokenCredential(accessToken);
                //var storageCredentials = new StorageCredentials(tokenCredential);
                var storageCredentials = new StorageCredentials(storageAccountName, storageAccountKey);
                // Define the blob to read
                CloudBlockBlob blob = new CloudBlockBlob(new Uri($"https://{storageAccountName}.blob.core.windows.net/{containerName}/{fileName}"), storageCredentials);
                
                using (StreamReader reader = new StreamReader(await blob.OpenReadAsync()))
                {
                    blobContent = await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
            return blobContent;
        }
    }
}
