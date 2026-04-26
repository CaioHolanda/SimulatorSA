namespace SimulatorSA.Bacnet.Mapping;

public class BacnetPointMap
{
    public BacnetObjectKind ObjectType { get; }
    public uint Instance { get; }
    public string PointKey { get; }
    public string ObjectName { get; }
    public string Description { get; }
    public bool IsWritable { get; }
    public BacnetPointMap(
        BacnetObjectKind objectType,
        uint instance,
        string pointKey,
        string objectName,
        string description,
        bool isWritable = false)
    {
        ObjectType = objectType;
        Instance = instance;
        PointKey = pointKey;
        ObjectName = objectName;
        Description = description;
        IsWritable = isWritable;
    }
}