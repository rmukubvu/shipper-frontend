﻿using System;
using System.Collections.Generic;
using Shipper.RestGateway.BaseClient;
using Shipper.RestGateway.Cache;
using Shipper.RestGateway.Model;

namespace Shipper.RestGateway.RestClients
{
    public class RestCalls
    {
        private readonly ICacheService _cache = null;
        private readonly JsonSerializer _serializer = null;

        public RestCalls()
        {
            _cache = new InMemoryCache();
            _serializer = new JsonSerializer();
        }
        
        #region Get calls

        public UserLogin Login(string userName, string password)
        {
            var client = new Services(_cache,_serializer);
            return client.GetResult<UserLogin>($"login?email={userName}&password={password}");
        }

        public List<ClearingStatus> GetClearingStatuses()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<ClearingStatus>>("clearingstatus");
        }

        public List<SmartDevice> GetSmartDevices()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<SmartDevice>>("smartdevice");
        }

        public List<Vehicle> GetVehicles()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Vehicle>>("vehicle");
        }

        public List<VehicleLocationHistory> GetVehicleLocationHistory(string vehicleId)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<VehicleLocationHistory>>($"replaytrip?vehicleId={vehicleId}");
        }

        public List<AllocatedDevice> GetAllocatedDevices()
        {
            var smartDeviceAllocations = GetSmartDeviceVehicleMapping();
            var result = new List<AllocatedDevice>();
            foreach (var smartDeviceAllocation in smartDeviceAllocations)
            {
                result.Add(new AllocatedDevice()
                {
                    smartDeviceAlloc = smartDeviceAllocation,
                    allocatedVehicle = GetVehicle(int.Parse(smartDeviceAllocation.deviceId))
                });
            }
            return result;
        }


        public List<SmartDeviceAllocation> GetSmartDeviceVehicleMapping()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<SmartDeviceAllocation>>("smartdevice/allocation");
        }

        public SmartDeviceAllocation GetSmartDeviceMappingByDeviceId(string deviceId){
            var client = new Services(_cache, _serializer);
            return client.GetResult<SmartDeviceAllocation>($"smartdevice/device/?deviceId={deviceId}");
        }

        public Vehicle GetVehicle(int telegramId)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<Vehicle>($"vehicle/device/id?deviceId={telegramId}");
        }

        public DashboardStatus GetDashboardStatusByWaybill(long waybill)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<DashboardStatus>($"shipment/dashboard/waybill?waybillNumber={waybill}");
        }

        public List<Shipment> GetShipmentOnVehicle(string vehicleId)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Shipment>>($"shipment/vehicle?vehicleId={vehicleId}");
        }

        public List<Driver> GetDrivers()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Driver>>("driver");
        }
        
        public DriverAllocation GetDriverAllocationByVehicleId(string vehicleId)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<DriverAllocation>($"vehicle/driver/allocation?vehicleId={vehicleId}");
        }

        #endregion

        #region Posts

        public void SaveSmartDevice(int telegramId)
        {
            var smartDevice = new SmartDevice()
            {
                deviceId = telegramId.ToString(),
                serialNumber = telegramId.ToString(),
                make = "Huawei",
                model = "P20 Lite"
            };

            var service= new Services(_cache, _serializer);
            var result = service.Post(smartDevice, "smartdevice");
            Console.WriteLine(result);
        }


        public void SaveVehicleLocation(int telegramId,string vehicleId,double latitude,double longitude)
        {
            var model = new VehicleLocation()
            {
                deviceId = telegramId.ToString(),
                latitude = latitude,
                longitude = longitude,
                locationDateTime = DateTime.Now,
                vehicleId = vehicleId
            };
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "location");
            Console.WriteLine(result);
        }

        public void SaveShipmentStatus(string vehicleId, int statusId)
        {
            var shipments = GetShipmentOnVehicle(vehicleId);
            foreach (var shipment in shipments)
            {
                SaveShipmentStatus(vehicleId, shipment.manifestReference, shipment.wayBill, statusId);
            }
        }

        public void SaveShipmentStatus(string vehicleId,string manifestReference,long waybill,int statusId)
        {
            var model = new ShipmentStatus()
            {
                vehicleId = vehicleId,
                manifestReference = manifestReference,
                wayBillNumber = waybill,
                statusId = statusId,
                createdDate = DateTime.Now
            };

            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "notification");
            Console.WriteLine(result);
        }

        public string SaveVehicle(Vehicle model)
        {
            var service  = new Services(_cache,_serializer);
            var result = service.Post(model, "vehicle");
            return result;
        }

        public string SaveSmartDeviceAllocation(SmartDeviceAllocation model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "smartdevice/allocation");
            return result;
        }

        public string SaveDriver(Driver model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "driver");
            return result;
        }

        public string AllocateDriverToVehicle(DriverAllocation model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/vehicle/driver/allocation");
            return result;
        }

        #endregion
    }
}
