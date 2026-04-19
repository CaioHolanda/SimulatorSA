using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Services;
using SimulatorSA.Tests.Stubs;
using Xunit;

namespace SimulatorSA.Tests.Application.Services;

public class HmiPollingServiceTests
{
    [Fact]
    public void CollectSample_ShouldMapSupervisoryStateToTrendSample()
    {
        var supervisoryState = new SupervisoryStateDto
        {
            Timestamp = new DateTime(2026, 4, 18, 10, 15, 0),
            RoomName = "Office A",
            IndoorTemperature = 20.8,
            OutdoorTemperature = 11.2,
            Setpoint = 21.0,
            HeaterOutput = 65.0,
            HeaterEnabled = true,
            ControllerType = "PID",
            CurrentStep = 12,
            SimulatedMinutes = 12.0
        };

        var supervisoryService = new SupervisoryStateServiceStub(supervisoryState);
        var pollingService = new HmiPollingService(supervisoryService);

        var sample = pollingService.CollectSample();

        Assert.Equal(supervisoryState.Timestamp, sample.Timestamp);
        Assert.Equal(supervisoryState.IndoorTemperature, sample.IndoorTemperature);
        Assert.Equal(supervisoryState.OutdoorTemperature, sample.OutdoorTemperature);
        Assert.Equal(supervisoryState.Setpoint, sample.Setpoint);
        Assert.Equal(supervisoryState.HeaterOutput, sample.HeaterOutput);
        Assert.Equal(supervisoryState.HeaterEnabled, sample.HeaterEnabled);
    }
}