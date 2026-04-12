/*
Papel do BacnetValueReadService

    receber um pedido de leitura;
    localizar o ponto BACnet correspondente;
    obter o SimulationSnapshot atual;
    extrair do snapshot o valor correto;
    devolver isso dentro de um BacnetOperationResult.
 */
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Models;
using SimulatorSA.Core.Models.SimulationData;

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

        return ReadPresentValue(point.PointKey);
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

        var snapshot = _simulationStateProvider.GetCurrentSnapshot();

        return point.PointKey switch
        {
            "room.temperature" => BacnetOperationResult.Ok(snapshot.RoomTemperature),
            "room.setpoint" => BacnetOperationResult.Ok(snapshot.Setpoint),
            "controller.output" => BacnetOperationResult.Ok(snapshot.ControllerOutput),
            "room.error" => BacnetOperationResult.Ok(snapshot.ControlError),
            "heating.power" => BacnetOperationResult.Ok(snapshot.HeatingPowerKW),

            _ => BacnetOperationResult.Fail(
                "unsupported_point",
                $"Point key '{point.PointKey}' is not supported by the read service.")
        };
    }
}