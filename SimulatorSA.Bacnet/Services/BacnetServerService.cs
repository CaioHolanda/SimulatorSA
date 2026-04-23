using System;
using System.IO.BACnet;
using System.IO.BACnet.Serialize;
using SimulatorSA.Bacnet.Configuration;
using SimulatorSA.Bacnet.Mapping;

namespace SimulatorSA.Bacnet.Services
{
    public class BacnetServerService
    {
        private BacnetClient? _client;
        private readonly BacnetDeviceConfiguration _configuration;
        private readonly IBacnetValueReadService _readService;

        public BacnetServerService(
            BacnetDeviceConfiguration configuration,
            IBacnetValueReadService readService)
        {
            _configuration = configuration;
            _readService = readService;
        }

        public void Start()
        {
            var transport = new BacnetIpUdpProtocolTransport(_configuration.UdpPort, false);
            _client = new BacnetClient(transport);

            _client.OnWhoIs += OnWhoIs;
            _client.OnReadPropertyRequest += OnReadPropertyRequest;

            _client.Start();

            Console.WriteLine($"[BACnet] Server started on UDP {_configuration.UdpPort}");
        }

        public void Stop()
        {
            _client?.Dispose();
            Console.WriteLine("[BACnet] Server stopped");
        }

        private void OnWhoIs(
            BacnetClient sender,
            BacnetAddress adr,
            int lowLimit,
            int highLimit)
        {
            Console.WriteLine("[BACnet] WhoIs received");

            sender.Iam(
                _configuration.DeviceInstance,
                BacnetSegmentations.SEGMENTATION_NONE,
                adr);

            Console.WriteLine($"[BACnet] I-Am sent (Device {_configuration.DeviceInstance})");
        }
        private static BacnetClient.Segmentation CreateSegmentation(BacnetMaxSegments maxSegments)
        {
            return new BacnetClient.Segmentation
            {
                buffer = new System.IO.BACnet.Serialize.EncodeBuffer(),
                sequence_number = 0,
                window_size = 1,
                max_segments = (byte)maxSegments
            };
        }
        private void OnReadPropertyRequest(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            Console.WriteLine(
                $"[BACnet] Read request: " +
                $"type={objectId.type}, instance={objectId.instance}, " +
                $"property={property.propertyIdentifier}, " +
                $"arrayIndex={property.propertyArrayIndex}");

            // 1) Tratar propriedades do objeto Device
            if (objectId.type == BacnetObjectTypes.OBJECT_DEVICE)
            {
                Console.WriteLine("[BACnet] Routing request to Device handler");
                if (TryHandleDeviceProperty(sender, adr, invokeId, objectId, property, maxSegments))
                    return;
                Console.WriteLine("[BACnet] Device handler returned false");
                return;
            }

            // 2) Para objetos AI/AV/AO, por enquanto só suportamos Present_Value
            if (property.propertyIdentifier != (uint)BacnetPropertyIds.PROP_PRESENT_VALUE)
                return;

            var result = _readService.ReadPresentValue(
                (BacnetObjectKind)objectId.type,
                (uint)objectId.instance);

            if (!result.Success)
                return;

            var value = new BacnetValue(result.Value);

            sender.ReadPropertyResponse(
                adr,
                invokeId,
                CreateSegmentation(maxSegments),
                objectId,
                property,
                new[] { value });

            Console.WriteLine($"[BACnet] Response sent: {result.Value}");
        }

        private bool TryHandleDeviceProperty(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
             BacnetMaxSegments maxSegments)
        {
            Console.WriteLine(
                $"[BACnet] Device property handler entered: " +
                $"property={property.propertyIdentifier}, arrayIndex={property.propertyArrayIndex}");

            try
            {
                // Object_Name
                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_NAME)
                {
                    Console.WriteLine("[BACnet] Handling Device.Object_Name");

                    sender.ReadPropertyResponse(
                        adr,
                        invokeId,
                        CreateSegmentation(maxSegments),
                        objectId,
                        property,
                        new[] { new BacnetValue(_configuration.DeviceName) });

                    Console.WriteLine($"[BACnet] Device Object_Name sent: {_configuration.DeviceName}");
                    return true;
                }

                // Object_Identifier
                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_IDENTIFIER)
                {
                    Console.WriteLine("[BACnet] Handling Device.Object_Identifier");

                    sender.ReadPropertyResponse(
                        adr,
                        invokeId,
                        CreateSegmentation(maxSegments),
                        objectId,
                        property,
                        new[] { new BacnetValue(objectId) });

                    Console.WriteLine($"[BACnet] Device Object_Identifier sent: {objectId.type}:{objectId.instance}");
                    return true;
                }

                // Object_List
                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_LIST)
                {
                    Console.WriteLine("[BACnet] Handling Device.Object_List");

                    // arrayIndex = 0 -> count
                    if (property.propertyArrayIndex == 0)
                    {
                        uint count = (uint)(1 + BacnetObjectCatalog.All.Count);

                        Console.WriteLine($"[BACnet] Object_List count requested, count={count}");

                        sender.ReadPropertyResponse(
                            adr,
                            invokeId,
                            CreateSegmentation(maxSegments),
                            objectId,
                            property,
                            new[] { new BacnetValue(count) });

                        Console.WriteLine($"[BACnet] Device Object_List count sent: {count}");
                        return true;
                    }

                    // arrayIndex = ALL
                    if (property.propertyArrayIndex == uint.MaxValue)
                    {
                        var values = BuildObjectListValues();

                        Console.WriteLine($"[BACnet] Object_List ALL requested, entries={values.Count}");

                        sender.ReadPropertyResponse(
                            adr,
                            invokeId,
                            CreateSegmentation(maxSegments),
                            objectId,
                            property,
                            values);

                        Console.WriteLine($"[BACnet] Device Object_List full sent with {values.Count} entries");
                        return true;
                    }

                    // arrayIndex = n
                    if (property.propertyArrayIndex >= 1)
                    {
                        var values = BuildObjectListValues();
                        uint itemIndex = property.propertyArrayIndex - 1;

                        Console.WriteLine($"[BACnet] Object_List item requested: index={property.propertyArrayIndex}");

                        if (itemIndex < values.Count)
                        {
                            sender.ReadPropertyResponse(
                                adr,
                                invokeId,
                                CreateSegmentation(maxSegments),
                                objectId,
                                property,
                                new[] { values[(int)itemIndex] });

                            Console.WriteLine($"[BACnet] Device Object_List[{property.propertyArrayIndex}] sent");
                            return true;
                        }

                        Console.WriteLine($"[BACnet] Object_List index out of range: {property.propertyArrayIndex}");
                        return false;
                    }
                }

                Console.WriteLine("[BACnet] Device property not handled");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BACnet] EXCEPTION while handling device property: {ex}");
                return false;
            }

        }

        private List<BacnetValue> BuildObjectListValues()
        {
            var values = new List<BacnetValue>
            {
                new BacnetValue(
                    new BacnetObjectId(
                        BacnetObjectTypes.OBJECT_DEVICE,
                        _configuration.DeviceInstance))
            };

            foreach (var point in BacnetObjectCatalog.All)
            {
                values.Add(
                    new BacnetValue(
                        new BacnetObjectId(
                            (BacnetObjectTypes)point.ObjectType,
                            point.Instance)));
            }

            return values;
        }

    }
}