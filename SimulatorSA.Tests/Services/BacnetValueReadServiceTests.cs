using SimulatorSA.Application.Interfaces;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Services;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Tests.Services;

public class BacnetValueReadServiceTests
{
    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnRoomTemperature()
    {
        // Arrange
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
        var pointResolver = new FakeBacnetPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(21.5, Assert.IsType<double>(result.Value));
    }
    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnRoomError()
    {
        // Arrange
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
        var pointResolver = new FakeBacnetPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue(BacnetObjectKind.AnalogInput, 2);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(0.5, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByPointKey_ShouldReturnSetpoint()
    {
        // Arrange
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
        var pointResolver = new FakeBacnetPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.setpoint");

        // Assert
        Assert.True(result.Success);
        Assert.Equal(22.0, Assert.IsType<double>(result.Value));
    }

    [Fact]
    public void ReadPresentValue_ByObjectTypeAndInstance_ShouldReturnPointNotFound_WhenPointDoesNotExist()
    {
        // Arrange
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
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
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
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
        var snapshot = CreateSnapshot();
        var stateProvider = new FakeSimulationStateProvider(snapshot);
        var pointResolver = new FakeUnsupportedPointResolver();

        var service = new BacnetValueReadService(stateProvider, pointResolver);

        // Act
        var result = service.ReadPresentValue("room.unmapped");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("unsupported_point", result.ErrorCode);
        Assert.NotNull(result.ErrorMessage);
    }

    private static SimulationSnapshot CreateSnapshot()
    {
        return new SimulationSnapshot
        {
            Time = 10,
            RoomTemperature = 21.5,
            Setpoint = 22.0,
            ControllerOutput = 55.0,
            ControlError = 0.5,
            HeatingPowerKW = 3.2
        };
    }

    private sealed class FakeSimulationStateProvider : ISimulationStateProvider
    {
        private readonly SimulationSnapshot _snapshot;

        public FakeSimulationStateProvider(SimulationSnapshot snapshot)
        {
            _snapshot = snapshot;
        }

        public SimulationSnapshot GetCurrentSnapshot()
        {
            return _snapshot;
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