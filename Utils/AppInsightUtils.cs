using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace Integrate2019Demo
{
    public class AppInsightUtils
    {
        
        private static string key = TelemetryConfiguration.Active.InstrumentationKey = AppConfiguration.InstrumentationKey;
        private static TelemetryClient telemetry = new TelemetryClient() { InstrumentationKey = key };

        ///// <summary>
        ///// Create a custom event with the supplied PNR metadata message in Application insights.
        ///// </summary>
        ///// <param name="message"></param> 
        ///// <param name="invocationId"></param> 
        ///// <param name="status"></param>
        ///// <param name="funcName"></param>
        ///// <returns></returns>
        public static void CreateTrackedEvent(DemoMessage message, string invocationId, string status, string funcName, TimeSpan elapsed)
        {
            try
            {
                //write custom events to application insights                     
                telemetry.Context.Operation.Id = invocationId;
                telemetry.Context.Operation.Name = funcName;
                telemetry.Context.Operation.ParentId = invocationId;

                // Set up properties and metrics:
                var properties = new Dictionary<string, string> { { "FileName", message.FileName.ToString() }, { "RetryCount", message.RetryCount.ToString() } };
                var metrics = new Dictionary<string, double> { { "Started", DateTime.Now.Millisecond }, { "Elapsed", elapsed.TotalMilliseconds } };

                // Send the event:
                telemetry.TrackEvent(funcName + status, properties, metrics);
                telemetry.Flush();
            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                //throw ex.InnerException;
            }
                  
        }

        public static void CreateCustomException(Exception appException, string invocationId, string funcName)
        {
            try
            {
                //write exception events to application insights  
                telemetry.Context.Operation.Id = invocationId;
                telemetry.Context.Operation.Name = funcName;
                telemetry.Context.Operation.ParentId = invocationId;

                // Set up some properties and metrics:
                //var properties = new Dictionary<string, string> { { "EdiIdentifier", message.EDIIdentifier.ToString() }, { "FileName", message.FileName.ToString() } };
                //var metrics = new Dictionary<string, double> { { "OperationElapsedTime", DateTime.Now.Millisecond }, { "RetryCount", message.RetryCount } };

                telemetry.TrackException(appException);
            }
            catch (Exception ex)
            {
                //log app insights exception with ex.Message
                //throw ex.InnerException;
            }
            

        }
    }
}
