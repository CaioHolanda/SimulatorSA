using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Services;
using Xunit.Abstractions;

namespace SimulatorSA.Tests.Bacnet.Services;

public class BacnetValueReadServiceTests
{
    private readonly ITestOutputHelper _output;
    public BacnetValueReadServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnRoomTemperature()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(21.5, Assert.IsType<double>(result.Value));
    }
    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnOutdoorTemperature()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 2);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(12.0, Assert.IsType<double>(result.Value));
    }
    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnSetpoint()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.setpoint");

        // Assert
        Assert.True(result.Success);
        Assert.Equal(22.0, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnControllerOutput()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("controller.output");

        // Assert
        Assert.True(result.Success);
        // NOTE: Currently mapped to heating power (kW). May change if controller output % is introduced.
        Assert.Equal(55.0, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnPointNotFound_WhenPointDoesNotExist()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 99);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("point_not_found", result.ErrorCode);
        Assert.NotNull(result.ErrorMessage);
    }

    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnPointNotFound_WhenPointKeyDoesNotExist()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.invalid");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("point_not_found", result.ErrorCode);
        Assert.NotNull(result.ErrorMessage);
    }

    [Fact]
    public void ReadPresentValue_ShouldReturnUnsupportedPoint_WhenPointExistsInCatalogButIsNotHandledByReadService()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeUnsupportedPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.unmapped");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("unsupported_point", result.ErrorCode);
        Assert.NotNull(result.ErrorMessage);
    }
    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnOutdoorTemperature()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.outdoor_temperature");

        // Assert
        Assert.True(result.Success);
        Assert.Equal(12.0, Assert.IsType<double>(result.Value));
    }
    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnHeatingPower()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("heating.power");

        // Assert
        Assert.True(result.Success);
        Assert.Equal(55.0, Assert.IsType<double>(result.Value));
    }
    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnControlError()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver(_output);

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.error");

        // Assert
        Assert.True(result.Success);

        double expectedError = state.Setpoint - state.IndoorTemperature;

        Assert.Equal(expectedError, Assert.IsType<double>(result.Value));
    }
    private static SimulationStateDto CreateState()
    {
        return new SimulationStateDto
        {
            RoomName = "Office A",
            IndoorTemperature = 21.5,
            OutdoorTemperature = 12.0,
            Setpoint = 22.0,
            HeaterOutput = 55.0,
            HeaterEnabled = true,
            ControllerType = "PID",
            CurrentStep = 10,
            SimulatedMinutes = 10.0
        };
    }

    private sealed class FakeSimulationStateProvider : ISimulationStateProvider
    {
        private readonly SimulationStateDto _state;

        public FakeSimulationStateProvider(SimulationStateDto state)
        {
            _state = state;
        }

        public SimulationStateDto GetCurrentState()
        {
            return _state;
        }
    }

    private sealed class FakeBacnetPointResolver : IBacnetPointResolver
    {
        private readonly ITestOutputHelper _output;

        public FakeBacnetPointResolver(ITestOutputHelper output)
        {
            _output = output;
        }

        public BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance)
        {
            var result = BacnetObjectCatalog.All.FirstOrDefault(point =>
                point.ObjectType == objectType &&
                point.Instance == instance);

            _output.WriteLine($"[Resolve] Type={objectType}, Instance={instance} -> {(result?.PointKey ?? "null")}");

            return result;
        }

        public BacnetPointMap? ResolveByPointKey(string pointKey)
        {
            var result = BacnetObjectCatalog.All.FirstOrDefault(point =>
                string.Equals(point.PointKey, pointKey, StringComparison.OrdinalIgnoreCase));

            _output.WriteLine($"[ResolveByKey] Key={pointKey} -> {(result?.ObjectName ?? "null")}");

            return result;
        }
    }
    private sealed class FakeUnsupportedPointResolver : IBacnetPointResolver
    {
        private readonly ITestOutputHelper _output;

        public FakeUnsupportedPointResolver(ITestOutputHelper output)
        {
            _output = output;
        }

        public BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance)
        {
            if (objectType == BacnetObjectKind.AnalogInput && instance == 999)
            {
                _output.WriteLine($"[Resolve] Type={objectType}, Instance={instance} -> room.unmapped");

                return new BacnetPointMap(
                    BacnetObjectKind.AnalogInput,
                    999,
                    "room.unmapped",
                    "Unmapped Point",
                    "Unmapped BACnet test point",
                    isWritable: false);
            }

            _output.WriteLine($"[Resolve] Type={objectType}, Instance={instance} -> null");
            return null;
        }

        public BacnetPointMap? ResolveByPointKey(string pointKey)
        {
            if (string.Equals(pointKey, "room.unmapped", StringComparison.OrdinalIgnoreCase))
            {
                _output.WriteLine($"[ResolveByKey] Key={pointKey} -> Unmapped Point");

                return new BacnetPointMap(
                    BacnetObjectKind.AnalogInput,
                    999,
                    "room.unmapped",
                    "Unmapped Point",
                    "Unmapped BACnet test point",
                    isWritable: false);
            }

            _output.WriteLine($"[ResolveByKey] Key={pointKey} -> null");
            return null;
        }
    }
}