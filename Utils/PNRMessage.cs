using System;

namespace Integrate2019Demo
{

    ///// <summary>
    ///// Defines the PNR message with the relevant metadata of the received EDI files.
    ///// </summary>
    ///// <param name="EDIIdentifier"></param> 
    ///// <param name="EDIVersion"></param> 
    ///// <param name="FileName"></param> 
    ///// <param name="MessageIdentifier"></param> 
    ///// <param name="RetryCount"></param> 
    ///// <param name="pnrStage"></param> 
    ///// <returns>PNRMessage</returns>
        public enum MessageStage
        {
            Received,
            Processed,
            Persisted,
            Completed
        }

        [Serializable]
        public class DemoMessage
        {
            public string CorrelationId { get; set; }
            public string MessageType { get; set; }
            public string FileName { get; set; }
            public string MessageIdentifier { get; set; }
            public int RetryCount { get; set; }

            public MessageStage Stage;
            
        }
}
