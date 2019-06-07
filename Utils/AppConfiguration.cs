using System;
using System.Collections.Generic;
using System.Text;

namespace Integrate2019Demo
{
    public static class AppConfiguration
    {
        public static string StorageAccountKey {
            get
            {
                return System.Environment.GetEnvironmentVariable("storageAccountKey", EnvironmentVariableTarget.Process);
            }
        }

        public static string StorageAccountName
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("storageAccountName", EnvironmentVariableTarget.Process);
            }
        }

        public static string BlobContainer
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("blobContainerName", EnvironmentVariableTarget.Process);
            }
        }

        public static string InstrumentationKey
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process);
            }
        }

        public static string ServiceBusEndPoint
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("serviceBusEndPoint", EnvironmentVariableTarget.Process);
            }

        }

        public static string ServiceBusEntityPath
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("serviceBusEntityPath", EnvironmentVariableTarget.Process);
            }
        }

        public static string ServiceBusKeyName
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("serviceBusKeyName", EnvironmentVariableTarget.Process);
            }
        }

        public static string ServiceBusSASKey
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("serviceBusSASKey", EnvironmentVariableTarget.Process);
            }
        }
    }
}
