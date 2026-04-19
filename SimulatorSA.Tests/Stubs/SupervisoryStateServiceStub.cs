using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

namespace SimulatorSA.Tests.Stubs;

public class SupervisoryStateServiceStub : ISupervisoryStateService
{
    private readonly SupervisoryStateDto _state;

    public SupervisoryStateServiceStub(SupervisoryStateDto state)
    {
        _state = state;
    }

    public SupervisoryStateDto GetCurrentState()
    {
        return _state;
    }
}