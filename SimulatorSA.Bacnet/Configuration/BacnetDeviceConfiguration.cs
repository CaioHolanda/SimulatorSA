using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Bacnet.Configuration
{
    public class BacnetDeviceConfiguration
    {
        public uint DeviceInstance { get; set; } = 1001;
        public string DeviceName { get; set; } = "SimulatorSA Controller";
        public string VendorName { get; set; } = "SimulatorSA";
        public uint VendorId { get; set; } = 999;
        public int UdpPort { get; set; } = 47808;
    }
}
