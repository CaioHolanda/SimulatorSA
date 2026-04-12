namespace SimulatorSA.Bacnet.Mapping;

public interface IBacnetPointResolver
{
    BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance);
    BacnetPointMap? ResolveByPointKey(string pointKey);
}