using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Services;
using Xunit;

namespace SimulatorSA.Tests.Application.Services;

public class TrendHistoryServiceTests
{
    [Fact]
    public void AddSample_ShouldStoreOneSample()
    {
        var service = new TrendHistoryService(10);

        var sample = new TrendSampleDto
        {
            Timestamp = new DateTime(2026, 4, 18, 10, 0, 0),
            IndoorTemperature = 20.5,
            OutdoorTemperature = 12.0,
            Setpoint = 21.0,
            HeaterOutput = 75.0,
            HeaterEnabled = true
        };

        service.AddSample(sample);

        var samples = service.GetSamples();

        Assert.Single(samples);
        Assert.Equal(20.5, samples[0].IndoorTemperature);
    }

    [Fact]
    public void GetSamples_ShouldPreserveInsertionOrder()
    {
        var service = new TrendHistoryService(10);

        service.AddSample(new TrendSampleDto { IndoorTemperature = 18.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 19.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 20.0 });

        var samples = service.GetSamples();

        Assert.Equal(3, samples.Count);
        Assert.Equal(18.0, samples[0].IndoorTemperature);
        Assert.Equal(19.0, samples[1].IndoorTemperature);
        Assert.Equal(20.0, samples[2].IndoorTemperature);
    }

    [Fact]
    public void AddSample_ShouldRespectMaxSamplesLimit()
    {
        var service = new TrendHistoryService(3);

        service.AddSample(new TrendSampleDto { IndoorTemperature = 18.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 19.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 20.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 21.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 22.0 });

        var samples = service.GetSamples();

        Assert.Equal(3, samples.Count);
    }

    [Fact]
    public void AddSample_ShouldDiscardOldestSamplesWhenLimitIsExceeded()
    {
        var service = new TrendHistoryService(3);

        service.AddSample(new TrendSampleDto { IndoorTemperature = 18.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 19.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 20.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 21.0 });

        var samples = service.GetSamples();

        Assert.Equal(3, samples.Count);
        Assert.Equal(19.0, samples[0].IndoorTemperature);
        Assert.Equal(20.0, samples[1].IndoorTemperature);
        Assert.Equal(21.0, samples[2].IndoorTemperature);
    }

    [Fact]
    public void Clear_ShouldRemoveAllSamples()
    {
        var service = new TrendHistoryService(10);

        service.AddSample(new TrendSampleDto { IndoorTemperature = 18.0 });
        service.AddSample(new TrendSampleDto { IndoorTemperature = 19.0 });

        service.Clear();

        var samples = service.GetSamples();

        Assert.Empty(samples);
    }
}