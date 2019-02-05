using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Country> GetCountries()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Country>>("countries");
        }
        public List<SmartDevice> GetSmartDevices()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<SmartDevice>>("smartdevice");
        }

        public SmartDevice GetDeviceById(string id)
        {
            List<SmartDevice> devices = new List<SmartDevice>();
            devices= GetSmartDevices();
            return devices.FirstOrDefault(e => e.deviceId == id);
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
        //vehicleLocations
        public List<VehicleLocation> GetVehicleCurrentLocations()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<VehicleLocation>>("vehicleLocations");
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

        public RestResponse RecoverPassword(string userName)
        {
            return GetResponse<RestResponse>($"recoverPassword?email={userName}");
        }

        public RestResponse RemoveUserAsAdmin(string userName)
        {
            return GetResponse<RestResponse>($"revokeAdmin?email={userName}");
        }

        public User ChangePassword(string userName,string password){
            return GetResponse<User>($"changePassword?email={userName}&password={password}");
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

        public Vehicle GetVehicleByDeviceId(string deviceId)
        {
            var client = new Services(_cache, _serializer);

            return client.GetResult<Vehicle>($"vehicle/device/id?deviceId={deviceId}");
        }

        public Vehicle GetVehicleById(string vehicleId)
        {
            var client = new Services(_cache, _serializer);

            return client.GetResult<Vehicle>($"vehicle/id?vehicleId={vehicleId}");
        }

        public DashboardStatus GetDashboardStatusByWaybill(long waybill)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<DashboardStatus>($"shipment/dashboard/waybill?waybillNumber={waybill}");
        }

        public FullShipment GetShipmentByWayBill(long waybill)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<FullShipment>($"shipment/waybill?waybill={waybill}");
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
        public CurrentStatusByWaybill GetCurrentStatusByWaybill(long waybill)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<CurrentStatusByWaybill>($"shipment/status/waybill?waybillNumber={waybill}");
        }               
        public List<Consignor> GetConsignor()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Consignor>>("consignor");
        }
        public List<Consignee> GetConsignee()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<Consignee>>("consignee");
        }
        public Consignee GetConsigneeById(string id)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<Consignee>($"consignee/id?id={id}");
        }

        public Consignee GetConsigneeByTelegramId(long telegramId)
        {
            return GetResponse<Consignee>($"consigneeByTelegram?telegramId={telegramId}");          
        }
        public RestResponse MapClientToTelegram(long telegramId,string mobileNumber)
        {
            return GetResponse<RestResponse>($"mapClient?telegramId={telegramId}&telephone={mobileNumber}");
        }
        public List<ShipmentWithStatus> GetShipmentWithConsigneeId(string consigneeId)
        {
            return GetResponse<List<ShipmentWithStatus>>($"shipment/status/consignee?consigneeId={consigneeId}");
        }
        public List<User> GetUsers()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<User>>("users");
        }
        public List<StatusByWaybill> GetStatusByWaybill(long waybill)
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<StatusByWaybill>>($"shipment/status/history?waybillNumber={waybill}");
        }
        public List<Package> GetPackagesByConsignee(string consigneeId)
        {
            //var client = new Services(_cache, _serializer);
            //return client.GetResult<List<Package>>($"shipment/consignee?consigneeId={consigneeId}");
            return GetResponse<List<Package>>($"shipment/consignee?consigneeId={consigneeId}");
        }
        public List<DeviceAllocation> GetSmartDeviceAllocation()
        {
            var client = new Services(_cache, _serializer);
            return client.GetResult<List<DeviceAllocation>>("smartdevice/allocation");
        }        
        private T GetResponse<T>(string requestQuery) where T : new() {
            var client = new Services(_cache, _serializer);
            return client.GetResult<T>(requestQuery);
        }
        //If you want to create models use https://jsonutils.com/

        #endregion

        #region Posts

        public string SaveSmartDevice(int telegramId)
        {
            var smartDevice = new SmartDevice()
            {
                deviceId = telegramId.ToString(),
                serialNumber = telegramId.ToString(),
                make = "Huawei",
                model = "P20 Lite"
            };
            var service= new Services(_cache, _serializer);
            return service.Post(smartDevice, "smartdevice");
        }
        public string SaveVehicleLocation(int telegramId,string vehicleId,double latitude,double longitude)
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
            return service.Post(model, "location");
        }
        public void SaveShipmentStatus(string vehicleId, int statusId)
        {
            var shipments = GetShipmentOnVehicle(vehicleId);
            foreach (var shipment in shipments)
            {
                SaveShipmentStatus(vehicleId, shipment.manifestReference, shipment.wayBill, statusId);
            }
        }
        public string SaveShipmentStatus(string vehicleId,string manifestReference,long waybill,int statusId)
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
            return service.Post(model, "notification");
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
        public string SavePackageOntoVehicle(Package model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/shipment");
            return result;
        }
        ///notification
        public string UpdateShipmentStatus(StatusNotification model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/notification");
            return result;
        }
        public string SaveUser(User model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/user");
            return result;
        }
        public string SaveConsignor(Consignor model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/consignor");
            return result;
        }
        public string SaveConsignee(Consignee model)
        {
            var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/consignee");
            return result;
        }
        public string UpdateUser(User model)
        {
            return PostWithResponse(model, "/user/update");
        }
        public string AddUserAsAdmin(SystemAdmin model)
        {
            return PostWithResponse(model, "/userAdmin");
        }
        public string SaveConsigneeContacts(ConsigneeContactDetails model){
            /*var service = new Services(_cache, _serializer);
            var result = service.Post(model, "/consignee/contacts");
            return result;*/
            return PostWithResponse(model, "/consignee/contacts");
        }
        private string PostWithResponse<T>(T model,string path) where T : new()
        {
            var service = new Services(_cache, _serializer);
            return service.Post(model, path);
        }

        #endregion
    }
}
