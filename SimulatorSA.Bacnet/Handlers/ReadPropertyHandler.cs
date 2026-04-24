using SimulatorSA.Bacnet.Configuration;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Responses;
using SimulatorSA.Bacnet.Services;
using System;
using System.Collections.Generic;
using System.IO.BACnet;

namespace SimulatorSA.Bacnet.Handlers
{
    public class ReadPropertyHandler
    {
        private readonly BacnetDeviceConfiguration _configuration;
        private readonly IBacnetValueReadService _readService;
        private readonly BacnetResponseService _responseService;

        public ReadPropertyHandler(
            BacnetDeviceConfiguration configuration,
            IBacnetValueReadService readService,
            BacnetResponseService responseService)
        {
            _configuration = configuration;
            _readService = readService;
            _responseService = responseService;
        }

        public void Handle(
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

            if (objectId.type == BacnetObjectTypes.OBJECT_DEVICE)
            {
                HandleDeviceProperty(sender, adr, invokeId, objectId, property, maxSegments);
                return;
            }

            HandleNonDeviceProperty(sender, adr, invokeId, objectId, property, maxSegments);
        }

        private void HandleNonDeviceProperty(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var propertyId = (BacnetPropertyIds)property.propertyIdentifier;

            switch (propertyId)
            {
                case BacnetPropertyIds.PROP_OBJECT_IDENTIFIER:
                    _responseService.SendReadPropertyResponse(
                        sender,
                        adr,
                        invokeId,
                        objectId,
                        property,
                        maxSegments,
                        new[] { new BacnetValue(objectId) });

                    Console.WriteLine($"[BACnet] Object_Identifier sent: {objectId.type}:{objectId.instance}");
                    return;

                case BacnetPropertyIds.PROP_OBJECT_TYPE:
                    _responseService.SendReadPropertyResponse(
                        sender,
                        adr,
                        invokeId,
                        objectId,
                        property,
                        maxSegments,
                        new[] { new BacnetValue(objectId.type) });

                    Console.WriteLine($"[BACnet] Object_Type sent: {objectId.type}");
                    return;

                case BacnetPropertyIds.PROP_OBJECT_NAME:
                    SendObjectName(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_DESCRIPTION:
                    SendDescription(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_PRESENT_VALUE:
                    SendPresentValue(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_PROPERTY_LIST:
                    SendPropertyList(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_UNITS:
                    SendUnits(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_STATUS_FLAGS:
                    SendStatusFlags(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_EVENT_STATE:
                    SendEventState(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                case BacnetPropertyIds.PROP_OUT_OF_SERVICE:
                    SendOutOfService(sender, adr, invokeId, objectId, property, maxSegments);
                    return;

                default:
                    Console.WriteLine($"[BACnet] Unsupported property for non-device object: {property.propertyIdentifier}");
                    return;

            }
        }
        private void SendDescription(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var objectKind = (BacnetObjectKind)objectId.type;

            var point = BacnetObjectCatalog.Find(
                objectKind,
                (uint)objectId.instance);

            var description = point?.Description
                ?? "SimulatorSA BACnet point";

            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(description) });

            Console.WriteLine($"[BACnet] Description sent: {description}");
        }
        private void SendPropertyList(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var values = new[]
            {
                new BacnetValue((uint)BacnetPropertyIds.PROP_OBJECT_IDENTIFIER),
                new BacnetValue((uint)BacnetPropertyIds.PROP_OBJECT_NAME),
                new BacnetValue((uint)BacnetPropertyIds.PROP_OBJECT_TYPE),
                new BacnetValue((uint)BacnetPropertyIds.PROP_PRESENT_VALUE),
                new BacnetValue((uint)BacnetPropertyIds.PROP_DESCRIPTION),
                new BacnetValue((uint)BacnetPropertyIds.PROP_UNITS),
                new BacnetValue((uint)BacnetPropertyIds.PROP_STATUS_FLAGS),
                new BacnetValue((uint)BacnetPropertyIds.PROP_EVENT_STATE),
                new BacnetValue((uint)BacnetPropertyIds.PROP_OUT_OF_SERVICE),
                new BacnetValue((uint)BacnetPropertyIds.PROP_PROPERTY_LIST)
            }; 
            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                values);

            Console.WriteLine($"[BACnet] Property_List sent for {objectId.type}:{objectId.instance}");
        }
        private void SendUnits(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var units = objectId.type switch
            {
                BacnetObjectTypes.OBJECT_ANALOG_INPUT when objectId.instance is 1 or 2
                    => BacnetUnitsId.UNITS_DEGREES_CELSIUS,

                BacnetObjectTypes.OBJECT_ANALOG_INPUT when objectId.instance == 3
                    => BacnetUnitsId.UNITS_KILOWATTS,

                BacnetObjectTypes.OBJECT_ANALOG_INPUT when objectId.instance == 4
                    => BacnetUnitsId.UNITS_DEGREES_CELSIUS,

                BacnetObjectTypes.OBJECT_ANALOG_OUTPUT when objectId.instance == 1
                    => BacnetUnitsId.UNITS_PERCENT,

                BacnetObjectTypes.OBJECT_ANALOG_VALUE when objectId.instance == 1
                    => BacnetUnitsId.UNITS_DEGREES_CELSIUS,

                _ => BacnetUnitsId.UNITS_NO_UNITS
            };

            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue((uint)units) });

            Console.WriteLine($"[BACnet] Units sent: {units}");
        }
        private void SendEventState(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(BacnetEventStates.EVENT_STATE_NORMAL) });

            Console.WriteLine("[BACnet] Event_State sent: NORMAL");
        }
        private void SendOutOfService(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(false) });

            Console.WriteLine("[BACnet] Out_Of_Service sent: false");
        }
        private void SendStatusFlags(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var flags = new BacnetBitString();

            flags.SetBit(0, false); // IN_ALARM
            flags.SetBit(1, false); // FAULT
            flags.SetBit(2, false); // OVERRIDDEN
            flags.SetBit(3, false); // OUT_OF_SERVICE

            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(flags) });

            Console.WriteLine("[BACnet] Status_Flags sent");
        }
        private void SendObjectName(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            var objectKind = (BacnetObjectKind)objectId.type;

            var point = BacnetObjectCatalog.Find(
                objectKind,
                (uint)objectId.instance);

            var objectName = point?.ObjectName
                ?? $"{objectId.type}:{objectId.instance}";

            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(objectName) });

            Console.WriteLine($"[BACnet] Object_Name sent: {objectName}");
        }
        private void SendPresentValue(
                    BacnetClient sender,
                    BacnetAddress adr,
                    byte invokeId,
                    BacnetObjectId objectId,
                    BacnetPropertyReference property,
                    BacnetMaxSegments maxSegments)
        {
            var result = _readService.ReadPresentValue(
                (BacnetObjectKind)objectId.type,
                (uint)objectId.instance);

            if (!result.Success)
            {
                Console.WriteLine("[BACnet] Present_Value read failed");
                return;
            }

            _responseService.SendReadPropertyResponse(
                sender,
                adr,
                invokeId,
                objectId,
                property,
                maxSegments,
                new[] { new BacnetValue(result.Value) });

            Console.WriteLine($"[BACnet] Present_Value sent: {result.Value}");
        }
        private void HandleDeviceProperty(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            try
            {
                Console.WriteLine(
                    $"[BACnet] Device property handler entered: " +
                    $"property={property.propertyIdentifier}, " +
                    $"arrayIndex={property.propertyArrayIndex}");

                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_NAME)
                {
                    _responseService.SendReadPropertyResponse(
                        sender,
                        adr,
                        invokeId,
                        objectId,
                        property,
                        maxSegments,
                        new[] { new BacnetValue(_configuration.DeviceName) });

                    Console.WriteLine($"[BACnet] Device Object_Name sent: {_configuration.DeviceName}");
                    return;
                }

                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_IDENTIFIER)
                {
                    _responseService.SendReadPropertyResponse(
                        sender,
                        adr,
                        invokeId,
                        objectId,
                        property,
                        maxSegments,
                        new[] { new BacnetValue(objectId) });

                    Console.WriteLine($"[BACnet] Device Object_Identifier sent: {objectId.type}:{objectId.instance}");
                    return;
                }

                if (property.propertyIdentifier == (uint)BacnetPropertyIds.PROP_OBJECT_LIST)
                {
                    HandleObjectList(sender, adr, invokeId, objectId, property, maxSegments);
                    return;
                }

                Console.WriteLine("[BACnet] Device property not handled");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BACnet] EXCEPTION while handling device property: {ex}");
            }
        }

        private void HandleObjectList(
            BacnetClient sender,
            BacnetAddress adr,
            byte invokeId,
            BacnetObjectId objectId,
            BacnetPropertyReference property,
            BacnetMaxSegments maxSegments)
        {
            Console.WriteLine("[BACnet] Handling Device.Object_List");

            if (property.propertyArrayIndex == 0)
            {
                uint count = (uint)(1 + BacnetObjectCatalog.All.Count);

                _responseService.SendReadPropertyResponse(
                    sender,
                    adr,
                    invokeId,
                    objectId,
                    property,
                    maxSegments,
                    new[] { new BacnetValue(count) });

                Console.WriteLine($"[BACnet] Device Object_List count sent: {count}");
                return;
            }

            if (property.propertyArrayIndex == uint.MaxValue)
            {
                var values = BuildObjectListValues();

                _responseService.SendReadPropertyResponse(
                    sender,
                    adr,
                    invokeId,
                    objectId,
                    property,
                    maxSegments,
                    values);

                Console.WriteLine($"[BACnet] Device Object_List full sent with {values.Count} entries");
                return;
            }

            if (property.propertyArrayIndex >= 1)
            {
                var values = BuildObjectListValues();
                uint itemIndex = property.propertyArrayIndex - 1;

                if (itemIndex >= values.Count)
                {
                    Console.WriteLine($"[BACnet] Object_List index out of range: {property.propertyArrayIndex}");
                    return;
                }

                _responseService.SendReadPropertyResponse(
                    sender,
                    adr,
                    invokeId,
                    objectId,
                    property,
                    maxSegments,
                    new[] { values[(int)itemIndex] });

                Console.WriteLine($"[BACnet] Device Object_List[{property.propertyArrayIndex}] sent");
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