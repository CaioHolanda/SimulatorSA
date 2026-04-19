using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Services;
using SimulatorSA.Tests.Stubs;
using Xunit;

namespace SimulatorSA.Tests.Application.Services;

public class SupervisoryStateServiceTests
{
    [Fact]
    public void GetCurrentState_ShouldMapSimulationStateToSupervisoryState()
    {
        var simulationState = new SimulationStateDto
        {
            RoomName = "Office A",
            IndoorTemperature = 20.3,
            OutdoorTemperature = 9.5,
            Setpoint = 21.0,
            HeaterOutput = 80.0,
            HeaterEnabled = true,
            ControllerType = "PID",
            CurrentStep = 7,
            SimulatedMinutes = 7.0
        };

        var provider = new SimulationStateProviderStub(simulationState);
        var service = new SupervisoryStateService(provider);

        var before = DateTime.Now;
        var result = service.GetCurrentState();
        var after = DateTime.Now;

        Assert.Equal(simulationState.RoomName, result.RoomName);
        Assert.Equal(simulationState.IndoorTemperature, result.IndoorTemperature);
        Assert.Equal(simulationState.OutdoorTemperature, result.OutdoorTemperature);
        Assert.Equal(simulationState.Setpoint, result.Setpoint);
        Assert.Equal(simulationState.HeaterOutput, result.HeaterOutput);
        Assert.Equal(simulationState.HeaterEnabled, result.HeaterEnabled);
        Assert.Equal(simulationState.ControllerType, result.ControllerType);
        Assert.Equal(simulationState.CurrentStep, result.CurrentStep);
        Assert.Equal(simulationState.SimulatedMinutes, result.SimulatedMinutes);

        Assert.True(result.Timestamp >= before);
        Assert.True(result.Timestamp <= after);
    }
}