using System.Collections.Generic;
using System.IO.BACnet;
using System.IO.BACnet.Serialize;

namespace SimulatorSA.Bacnet.Responses
{
    public class BacnetResponseService
    {
        public void SendReadPropertyResponse(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments,
            IList<BacnetValue> values)
        {
            sender.ReadPropertyResponse(
                adr,
                invokeId,
                CreateSegmentation(maxSegments),
                objectId,
                property,
                values);
        }

        private static BacnetClient.Segmentation CreateSegmentation(
            BacnetMaxSegments maxSegments)
        {
            return new BacnetClient.Segmentation
            {
                buffer = new EncodeBuffer(),
                sequence_number = 0,
                window_size = 1,
                max_segments = (byte)maxSegments
            };
        }
    }
}