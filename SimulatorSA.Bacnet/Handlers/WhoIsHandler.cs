using System;
using System.IO.BACnet;
using SimulatorSA.Bacnet.Configuration;

namespace SimulatorSA.Bacnet.Handlers
{
    public class WhoIsHandler
    {
        private readonly BacnetDeviceConfiguration _configuration;

        public WhoIsHandler(BacnetDeviceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Handle(
            BacnetClient sender,
            BacnetAddress adr,
            int lowLimit,
            int highLimit)
        {
            Console.WriteLine("[BACnet] WhoIs received");

            sender.Iam(
                _configuration.DeviceInstance,
                BacnetSegmentations.SEGMENTATION_NONE,
                adr);

            Console.WriteLine($"[BACnet] I-Am sent (Device {_configuration.DeviceInstance})");
        }
    }
}