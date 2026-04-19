/*
Papel do BacnetValueReadService

    receber um pedido de leitura;
    localizar o ponto BACnet correspondente;
    obter o estado atual da simulacao;
    extrair do estado o valor correto;
    devolver isso dentro de um BacnetOperationResult.
 */
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

        var state = _simulationStateProvider.GetCurrentState();

        return ReadFromState(point.PointKey, state);
    }
    private static BacnetOperationResult ReadFromState(string pointKey, SimulationStateDto state)
    {
        return pointKey switch
        {
            "room.temperature" => BacnetOperationResult.Ok(state.IndoorTemperature),
            "room.setpoint" => BacnetOperationResult.Ok(state.Setpoint),
            "controller.output" => BacnetOperationResult.Ok(state.HeaterOutput),
            _ => BacnetOperationResult.Fail(
                "unsupported_point",
                $"Point key '{pointKey}' is not supported by the read service.")
        };
    }
}