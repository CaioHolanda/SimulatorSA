using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

namespace SimulatorSA.Tests.Stubs;

public class SimulationStateProviderStub : ISimulationStateProvider
{
    private readonly SimulationStateDto _state;

    public SimulationStateProviderStub(SimulationStateDto state)
    {
        _state = state;
    }

    public SimulationStateDto GetCurrentState()
    {
        return _state;
    }
}