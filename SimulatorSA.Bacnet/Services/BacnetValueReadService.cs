using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Models;

namespace SimulatorSA.Bacnet.Services;

public class BacnetValueReadService : IBacnetValueReadService
{
    private readonly ISimulationStateProvider _simulationStateProvider;
    private readonly IBacnetPointResolver _pointResolver;

    public BacnetValueReadService(
        ISimulationStateProvider simulationStateProvider,
        IBacnetPointResolver pointResolver)
    {
        _simulationStateProvider = simulationStateProvider;
        _pointResolver = pointResolver;
    }

    public BacnetOperationResult ReadPresentValue(BacnetObjectKind objectType, uint instance)
    {
        var point = _pointResolver.Resolve(objectType, instance);

        if (point is null)
        {
            return BacnetOperationResult.Fail(
                "point_not_found",
                $"No BACnet point was found for object type '{objectType}' and instance '{instance}'.");
        }

        var state = _simulationStateProvider.GetCurrentState();

        return ReadFromState(point.PointKey, state);
    }

    public BacnetOperationResult ReadPresentValue(string pointKey)
    {
        var point = _pointResolver.ResolveByPointKey(pointKey);

        if (point is null)
        {
            return BacnetOperationResult.Fail(
                "point_not_found",
                $"No BACnet point was found for point key '{pointKey}'.");
        }

        var state = _simulationStateProvider.GetCurrentState();

        return ReadFromState(point.PointKey, state);
    }

    private static BacnetOperationResult ReadFromState(string pointKey, SimulationStateDto state)
    {
        return pointKey switch
        {
            "room.temperature" => BacnetOperationResult.Ok(state.IndoorTemperature),
            "room.outdoor_temperature" => BacnetOperationResult.Ok(state.OutdoorTemperature),
            "room.setpoint" => BacnetOperationResult.Ok(state.Setpoint),
            "room.error" => BacnetOperationResult.Ok(state.Setpoint - state.IndoorTemperature),
            "heating.power" => BacnetOperationResult.Ok(state.HeaterOutput),

            // Compatibilidade temporária
            "controller.output" => BacnetOperationResult.Ok(state.HeaterOutput),

            _ => BacnetOperationResult.Fail(
                "unsupported_point",
                $"Point key '{pointKey}' is not supported by the read service.")
        };
    }
}