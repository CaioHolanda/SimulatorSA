namespace SimulatorSA.Bacnet.Mapping;

public static class BacnetObjectCatalog
{
    public static IReadOnlyList<BacnetPointMap> All { get; } =
    [
        new BacnetPointMap
        {
            PointKey = "room.temperature",
            ObjectType = BacnetObjectKind.AnalogInput,
            Instance = 1,
            ObjectName = "Room Temperature",
            IsWritable = false
        },
        new BacnetPointMap
        {
            PointKey = "room.outdoor_temperature",
            ObjectType = BacnetObjectKind.AnalogInput,
            Instance = 2,
            ObjectName = "Outdoor Temperature",
            IsWritable = false
        },
        new BacnetPointMap
        {
            PointKey = "room.error",
            ObjectType = BacnetObjectKind.AnalogInput,
            Instance = 3,
            ObjectName = "Control Error",
            IsWritable = false
        },
        new BacnetPointMap
        {
            PointKey = "heating.power",
            ObjectType = BacnetObjectKind.AnalogInput,
            Instance = 4,
            ObjectName = "Heating Power",
            IsWritable = false
        },
        new BacnetPointMap
        {
            PointKey = "room.setpoint",
            ObjectType = BacnetObjectKind.AnalogValue,
            Instance = 1,
            ObjectName = "Room Setpoint",
            IsWritable = true
        },

        // Compatibilidade temporária; remover depois se não for expor percentual real
        new BacnetPointMap
        {
            PointKey = "controller.output",
            ObjectType = BacnetObjectKind.AnalogOutput,
            Instance = 1,
            ObjectName = "Controller Output",
            IsWritable = false
        }
    ];
}