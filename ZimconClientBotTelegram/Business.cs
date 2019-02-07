using System;
using System.Collections.Generic;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconClientBotTelegram
{
    public class Business
    {
        private readonly RestCalls _restCalls;
        private readonly IDictionary<string, SmartDevice> _smartDeviceCache;
        private readonly IDictionary<string, SmartDeviceAllocation> _smartDeviceAllocationCache;
        private readonly IDictionary<string, Vehicle> _vehicleCache;
        private readonly IDictionary<string, List<ClearingStatus>> _clearingStatus;

        public Business()
        {
            _restCalls = new RestCalls();
            _smartDeviceCache = new Dictionary<string, SmartDevice>();
            _smartDeviceAllocationCache = new Dictionary<string, SmartDeviceAllocation>();
            _vehicleCache = new Dictionary<string, Vehicle>();
            _clearingStatus = new Dictionary<string, List<ClearingStatus>>();
        }

        public List<ClearingStatus> GetClearingStatuses()
        {
            if (_clearingStatus.ContainsKey("statuses"))
                return _clearingStatus["statuses"];

            var result = _restCalls.GetClearingStatuses();
            _clearingStatus["statuses"] = result;
            return GetClearingStatuses();
        }

        public List<SmartDeviceAllocation> GetSmartDeviceVehicleMapping()
        {
            return _restCalls.GetSmartDeviceVehicleMapping();
        }

        public List<Shipment> GetShipmentOnVehicle(int telegramId)
        {
            var vehicleId = GetVehicleId(telegramId);
            return _restCalls.GetShipmentOnVehicle(vehicleId);
        }

        public List<ShipmentWithStatus> GetShipmentWithTelegramId(int telegramId)
        {
            Consignee consignee = _restCalls.GetConsigneeByTelegramId(telegramId);
            if (!string.IsNullOrEmpty(consignee.name))
            {
                return _restCalls.GetShipmentWithConsigneeId(consignee.id);
            }
            return new List<ShipmentWithStatus>();
        }

        public string GetDashboardInfor(int waybill)
        {
            var status = _restCalls.GetDashboardStatusByWaybill(waybill);
            return $"Status: {status.currentStatus}\nProgress : {status.label}";
        }
        
        public void CacheDevices()
        {
            var collection = _restCalls.GetSmartDeviceVehicleMapping();
            foreach (var item in collection)
            {
                _smartDeviceAllocationCache[item.deviceId] = item;
            }

            var collection2 = _restCalls.GetSmartDevices();
            foreach (var item in collection2)
            {
                _smartDeviceCache[item.deviceId] = item;
            }
        }
        public DashboardStatus GetDashboardStatusByWaybill(long waybill)
        {
            return _restCalls.GetDashboardStatusByWaybill(waybill);
        }
        public string GetVehicleId(int telegramId)
        {
            var key = telegramId.ToString();
            if (_smartDeviceAllocationCache.ContainsKey(key))
            {
                return _smartDeviceAllocationCache[key].vehicleId;
            }
            var result = _restCalls.GetSmartDeviceMappingByDeviceId(key);
            if (result == null) return string.Empty;
            RefreshCache();
            return result.vehicleId;
            //throw new Exception("This device is not linked to a vehicle. Please contact admin");
        }

        public string GetVehicleInfor(int telegramId)
        {
            var key = telegramId.ToString();
            if (!_vehicleCache.ContainsKey(key))
            {             
                var vehicle = _restCalls.GetVehicle(telegramId);
                if (vehicle == null) return "No vehicle details available";
                _vehicleCache[key] = vehicle;
                return $"Your vehicle details:\n{ vehicle.make}\nLicense: {vehicle.licenseId}";
            }
            else
            {
                var vehicle = _vehicleCache[key];
                return $"Your vehicle details:\n{ vehicle.make}\nLicense: {vehicle.licenseId}";
            }
        }

        public string Link(long telegramId, string mobileNumber)
        {
            RestResponse response = _restCalls.MapClientToTelegram(telegramId, mobileNumber);
            return response.message;
        }

        public string GetConsigneeRegisteredTo(long telegramId)
        {
            Consignee consignee = _restCalls.GetConsigneeByTelegramId(telegramId);
            if (string.IsNullOrEmpty(consignee.name))
            {
                return "You are not linked , please contact Zimcon Admin";
            }
            return "You are linked to:\n" + consignee.name;
        }

        public void SaveDevice(int telegramId)
        {
            if (_smartDeviceCache.ContainsKey(telegramId.ToString())) return;
            _restCalls.SaveSmartDevice(telegramId);
            RefreshCache();
        }

        public bool IsDeviceLinked(int telegramId){
            Consignee consignee = _restCalls.GetConsigneeByTelegramId(telegramId);
            return !string.IsNullOrEmpty(consignee.name);
        }

        public void SaveLocation(int telegramId,  double latitude, double longitude){
            var vehicleId = GetVehicleId(telegramId);
            _restCalls.SaveVehicleLocation(telegramId, vehicleId, latitude, longitude);
        }

        public void SaveShipmentStatus(int telegramId, int statusId)
        {
            var vehicleId = GetVehicleId(telegramId);
            _restCalls.SaveShipmentStatus(vehicleId,statusId);
        }

        public void SaveShipmentStatus(int telegramId,string manifestReference, int waybill, int statusId){
            var vehicleId = GetVehicleId(telegramId);
            //_restCalls.SaveShipmentStatus(vehicleId,manifestReference,waybill,statusId);
        }

        private void RefreshCache()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                DestroySmartDeviceCache();
                CacheDevices();
            });
        }
                
        private void DestroySmartDeviceCache()
        {
            _smartDeviceAllocationCache.Clear();
            _smartDeviceCache.Clear();
        }
    }
}
