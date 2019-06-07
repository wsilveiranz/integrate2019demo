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
    public static class KeyVaultUtils
    {    
        ///// <summary>
        ///// get the secret value from the azure key vault.
        ///// </summary>
        ///// <param name="SecretKey"></param>      
        ///// <returns>SecretValue</returns>
        public static async Task<string> GetKeyVaultSecret(string SecretKey)
        {
            SecretBundle secretValue;
            try
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var vaultUrl = SecretUri(SecretKey);

                secretValue = await keyVaultClient.GetSecretAsync(vaultUrl);
            }
            catch (KeyVaultErrorException kex)
            {
                //log app insights exception with kex.Message
                throw kex.InnerException;
            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                throw ex.InnerException;
            }
            //log.Info("Secret Value retrieved from KeyVault.");

            return secretValue.Value;
        }

        ///// <summary>
        ///// generate the secret uri in a format required for azure key vault client library from the given values.
        ///// </summary>
        ///// <param name="SecretKey"></param>      
        ///// <returns>string</returns>
        public static string SecretUri(string SecretKey)
        {
            return $"{System.Environment.GetEnvironmentVariable("VaultUrl")}/secrets/{SecretKey}";
        }

        ///// <summary>
        ///// Get access token for the Azure SQL, msi enabled.
        ///// </summary>            
        ///// <returns>accessToken</returns>
        public static async Task<string> GetMSITokenAsync(string resourceKeyName)
        {          
            string accessToken;
            try
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                string resourceUri = System.Environment.GetEnvironmentVariable(resourceKeyName);
                accessToken = await azureServiceTokenProvider.GetAccessTokenAsync(resourceUri);
            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                throw ex.InnerException;
            }
            //log.Info("Access Token retrieved from AAD.");

            return accessToken;
        }
    }
}
