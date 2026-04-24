using System;
using System.IO.BACnet;
using SimulatorSA.Bacnet.Configuration;
using SimulatorSA.Bacnet.Handlers;

namespace SimulatorSA.Bacnet.Services
{
    public class BacnetServerService
    {
        private BacnetClient? _client;

        private readonly BacnetDeviceConfiguration _configuration;
        private readonly WhoIsHandler _whoIsHandler;
        private readonly ReadPropertyHandler _readPropertyHandler;

        public BacnetServerService(
            BacnetDeviceConfiguration configuration,
            WhoIsHandler whoIsHandler,
            ReadPropertyHandler readPropertyHandler)
        {
            _configuration = configuration;
            _whoIsHandler = whoIsHandler;
            _readPropertyHandler = readPropertyHandler;
        }

        public void Start()
        {
            var transport = new BacnetIpUdpProtocolTransport(_configuration.UdpPort, false);
            _client = new BacnetClient(transport);

            _client.OnWhoIs += _whoIsHandler.Handle;
            _client.OnReadPropertyRequest += _readPropertyHandler.Handle;

            _client.Start();

            Console.WriteLine($"[BACnet] Server started on UDP {_configuration.UdpPort}");
        }

        public void Stop()
        {
            if (_client is null)
                return;

            _client.OnWhoIs -= _whoIsHandler.Handle;
            _client.OnReadPropertyRequest -= _readPropertyHandler.Handle;

            _client.Dispose();
            _client = null;

            Console.WriteLine("[BACnet] Server stopped");
        }
    }
}