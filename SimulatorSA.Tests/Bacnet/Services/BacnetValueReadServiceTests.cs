using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Services;

namespace SimulatorSA.Tests.Bacnet.Services;

public class BacnetValueReadServiceTests
{
    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnRoomTemperature()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(21.5, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnSetpoint()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver();

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
        var pointResolver = new FakeBacnetPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("controller.output");

        // Assert
        Assert.True(result.Success);
        Assert.Equal(55.0, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnPointNotFound_WhenPointDoesNotExist()
    {
        // Arrange
        var state = CreateState();
        var stateProvider = new FakeSimulationStateProvider(state);
        var pointResolver = new FakeBacnetPointResolver();

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
        var pointResolver = new FakeBacnetPointResolver();

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
        var pointResolver = new FakeUnsupportedPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.unmapped");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("unsupported_point", result.ErrorCode);
        Assert.NotNull(result.ErrorMessage);
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
        public BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance)
        {
            return BacnetObjectCatalog.All.FirstOrDefault(point =>
                point.ObjectType == objectType &&
                point.Instance == instance);
        }

        public BacnetPointMap? ResolveByPointKey(string pointKey)
        {
            return BacnetObjectCatalog.All.FirstOrDefault(point =>
                string.Equals(point.PointKey, pointKey, StringComparison.OrdinalIgnoreCase));
        }
    }

    private sealed class FakeUnsupportedPointResolver : IBacnetPointResolver
    {
        public BacnetPointMap? Resolve(BacnetObjectKind objectType, uint instance)
        {
            if (objectType == BacnetObjectKind.AnalogInput && instance == 999)
            {
                return new BacnetPointMap
                {
                    PointKey = "room.unmapped",
                    ObjectType = BacnetObjectKind.AnalogInput,
                    Instance = 999,
                    ObjectName = "Unmapped Point",
                    IsWritable = false
                };
            }

            return null;
        }

        public BacnetPointMap? ResolveByPointKey(string pointKey)
        {
            if (string.Equals(pointKey, "room.unmapped", StringComparison.OrdinalIgnoreCase))
            {
                return new BacnetPointMap
                {
                    PointKey = "room.unmapped",
                    ObjectType = BacnetObjectKind.AnalogInput,
                    Instance = 999,
                    ObjectName = "Unmapped Point",
                    IsWritable = false
                };
            }

            return null;
        }
    }
}