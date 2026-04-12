using System;
using System.Linq;

namespace SimulatorSA.Bacnet.Mapping;

public class BacnetPointResolver : IBacnetPointResolver
{
    public BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance)
    {
        return BacnetObjectCatalog.All.FirstOrDefault(point =>
            point.ObjectType == objectType &&
            point.Instance == instance);
    }

    public BacnetPointMap? ResolveByPointKey(string pointKey)
    {
        if (string.IsNullOrWhiteSpace(pointKey))
            return null;

        return BacnetObjectCatalog.All.FirstOrDefault(point =>
            string.Equals(point.PointKey, pointKey, StringComparison.OrdinalIgnoreCase));
    }
}