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

namespace Integrate2019Demo
{
    public static class ServiceBusUtils
    {
        ///// <summary>
        ///// Create abrokered message from PNR metadata message to be sent to ASB Topic.
        ///// </summary>
        ///// <param name="PNREnvelope"></param>      
        ///// <returns></returns>
        public static Message CreateSBMessage(DemoMessage msg)
        {
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(msg);
                Stream streamMessage = new MemoryStream();
                //instanitate a brokered message using the pnrMessage
                Message message = new Message(Encoding.UTF8.GetBytes(jsonMessage));
                //set the content type
                message.ContentType = "application/json";
                //Add the message properties
                message.UserProperties.Add("Stage", msg.Stage.ToString());
                message.UserProperties.Add("FileName", msg.FileName);
                message.UserProperties.Add("RetryCount", msg.RetryCount);

                return message;

            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                throw ex.InnerException;
            }
        }

        ///// <summary>
        ///// publish a brokered message from to ASB Topic.
        ///// </summary>
        ///// <param name="sbMsg"></param>  
        ///// <param name="sbConn"></param>
        ///// <param name="topicName"></param>
        ///// <param name="subscription"></param>
        ///// <returns>bool</returns>
        public static bool PublishMessage(Message sbMsg, string sbEndPoint, string entityPath, string keyName, string sasKey)
        {
            bool retVal = false;
            try
            {
                //endpoint, entitypath, 
                ServiceBusConnectionStringBuilder connStr = new ServiceBusConnectionStringBuilder(sbEndPoint, entityPath, keyName, sasKey);
                QueueClient client = new QueueClient(connStr);
                client.SendAsync(sbMsg);
                retVal = true;
                return retVal;
            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                throw ex.InnerException;
            }
        }

    }
}
