using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

namespace SimulatorSA.Application.Services;

public class TrendCollectionService : ITrendCollectionService
{
    private readonly IPollingCoordinator _pollingCoordinator;
    private readonly ITrendHistoryService _trendHistoryService;

    public TrendCollectionService(
        IPollingCoordinator pollingCoordinator,
        ITrendHistoryService trendHistoryService)
    {
        ArgumentNullException.ThrowIfNull(pollingCoordinator);
        ArgumentNullException.ThrowIfNull(trendHistoryService);

        _pollingCoordinator = pollingCoordinator;
        _trendHistoryService = trendHistoryService;
    }

    public PollingResult CollectAndStore()
    {
        var result = _pollingCoordinator.TryCollect();

        if (result.HasSample && result.Sample is not null)
        {
            _trendHistoryService.AddSample(result.Sample);
        }

        return result;
    }
}