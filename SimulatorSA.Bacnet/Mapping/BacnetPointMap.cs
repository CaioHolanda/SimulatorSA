using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Bacnet.Mapping
{
    public class BacnetPointMap
    {
        public string PointKey { get; init; } = string.Empty;
        public BacnetObjectKind ObjectType { get; init; }
        public uint Instance { get; init; }
        public string ObjectName { get; init; } = string.Empty;
        public bool IsWritable { get; init; }
    }
}
