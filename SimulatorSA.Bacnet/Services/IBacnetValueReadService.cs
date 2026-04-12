using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Models;

namespace SimulatorSA.Bacnet.Services;

public interface IBacnetValueReadService
{
    BacnetOperationResult ReadPresentValue(BacnetObjectKind objectType, uint instance);
    BacnetOperationResult ReadPresentValue(string pointKey);
}