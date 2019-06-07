using Integrate2019Demo.Utils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Integrate2019Demo
{
    public static class SBQueueTrigger
    {
        [FunctionName("SBQueueTrigger")]
        public static void Run([ServiceBusTrigger("integrate2019", Connection = "servicebusconn")]Message msg, ILogger log)
        {
            object filename;
            msg.UserProperties.TryGetValue("FileName", out filename);
            Task<string> content = BlobStorageUtils.GetBlob(AppConfiguration.BlobContainer, filename.ToString(), AppConfiguration.StorageAccountName,AppConfiguration.StorageAccountKey);          
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {msg.MessageId}, with filename {filename.ToString()}");
            log.LogInformation($"FileContent: {content.Result}");
        }
    }
}
