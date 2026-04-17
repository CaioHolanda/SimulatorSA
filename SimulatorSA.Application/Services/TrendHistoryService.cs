namespace SimulatorSA.Application.Services;

using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

public class TrendHistoryService : ITrendHistoryService
{
    private readonly int _maxSamples;
    private readonly Queue<TrendSampleDto> _samples;

    public TrendHistoryService(int maxSamples = 720)
    {
        _maxSamples = maxSamples;
        _samples = new Queue<TrendSampleDto>(_maxSamples);
    }

    public void AddSample(TrendSampleDto sample)
    {
        if (_samples.Count >= _maxSamples)
            _samples.Dequeue();

        _samples.Enqueue(sample);
    }

    public IReadOnlyList<TrendSampleDto> GetSamples()
    {
        return _samples.ToList();
    }

    public void Clear()
    {
        _samples.Clear();
    }
}