using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using Integrate2019Demo.Utils;

namespace Integrate2019Demo
{
    public static class FunctionReceiveMessage
    {
        [FunctionName("ReceiveMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            log.LogInformation("C# HTTP trigger function processed a request.");
            

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var fileName = $"{Guid.NewGuid().ToString()}.json";

            await BlobStorageUtils.UploadBlob(AppConfiguration.BlobContainer, fileName, requestBody, AppConfiguration.StorageAccountName, AppConfiguration.StorageAccountKey);

            DemoMessage msg = new DemoMessage()
            {
                FileName = fileName,
                Stage = MessageStage.Received,
                RetryCount = 0,
                MessageIdentifier = Guid.NewGuid().ToString()
            };

            var result = ServiceBusUtils.PublishMessage(ServiceBusUtils.CreateSBMessage(msg), AppConfiguration.ServiceBusEndPoint, AppConfiguration.ServiceBusEntityPath, AppConfiguration.ServiceBusKeyName, AppConfiguration.ServiceBusSASKey);

            stopWatch.Stop();

            if (result)
            {
                AppInsightUtils.CreateTrackedEvent(msg, msg.MessageIdentifier, "Completed", "ReceiveMessage", stopWatch.Elapsed);
                return (ActionResult)new OkObjectResult(msg);
            }
            else
            {
                AppInsightUtils.CreateTrackedEvent(msg, msg.MessageIdentifier, "Failed", "ReceiveMessage", stopWatch.Elapsed);
                return (ActionResult)new BadRequestObjectResult("Failed to Receive Message");
            }
        }
    }
}
