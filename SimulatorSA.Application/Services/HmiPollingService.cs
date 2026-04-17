namespace SimulatorSA.Application.Services;

using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

public class HmiPollingService : IHmiPollingService
{
    private readonly ISupervisoryStateService _supervisoryStateService;

    public HmiPollingService(ISupervisoryStateService supervisoryStateService)
    {
        _supervisoryStateService = supervisoryStateService;
    }

    public TrendSampleDto CollectSample()
    {
        var state = _supervisoryStateService.GetCurrentState();

        return new TrendSampleDto
        {
            Timestamp = state.Timestamp,
            IndoorTemperature = state.IndoorTemperature,
            OutdoorTemperature = state.OutdoorTemperature,
            Setpoint = state.Setpoint,
            HeaterOutput = state.HeaterOutput,
            HeaterEnabled = state.HeaterEnabled
        };
    }
}