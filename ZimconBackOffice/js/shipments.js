/**
 * Created by robson on 2017/04/17.
 */
$(document).ready(function () {

    if (localStorage.getItem('shipper.userName') === "") {
        location.href = "/Login";
        return;
    }
    var restEndPoint = "http://154.0.174.76:8089"; 
    var map;
    var map_marker;
    var lat = -26.195246;
    var lng = 28.034088;
    var serialNumber;
    var lineCoordinatesArray = [];
    var vehicles = [];

    //var url = new URL(window.location.href);
    //var waybillFromQueryString = url.searchParams.get("waybill");
    // user has directly clicked to this page without going through
    // the home page
    var cachedShipmentsResults = localStorage.getItem('shipper.Shipments');
    if (cachedShipmentsResults === null || cachedShipmentsResults === "") {
        //fetch from ajax call
        var consigneeId = localStorage.getItem('shipper.consignee');
        var getUrlShipmentsWithStatus = restEndPoint + "/shipment/status/consignee?consigneeId=" + consigneeId;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: getUrlShipmentsWithStatus,
            success: function (result) {
                localStorage.setItem('shipper.Shipments', JSON.stringify(result));
                initialize();
            },
            error: function () {
                swal(
                    'Ooops',
                    'Something went terribly wrong! /shipment/status/consignee?consigneeId= ',
                    'error'
                );
            }
        });
    }

    var result = JSON.parse(localStorage.getItem('shipper.Shipments'));
    var trHTML = '';
    $.each(result, function (i, item) {
        trHTML += '<tr><td>' + displayVehicleDetails(item.shipment.vehicleId) + '</td><td>' + item.shipment.manifestReference + '</td><td>' + item.shipment.wayBillNumber + '</td><td><a href=javascript:void(0); onclick="displayStatusChanges(' + item.shipment.wayBillNumber + ');">View</a></td></tr>';
        if (vehicles.indexOf(item.shipment.vehicleId) === -1) {
            vehicles.push(item.shipment.vehicleId);
            initialize();
            displayMetrics(item.shipment.wayBillNumber);
        }
    });
    $('#shipmentsDataTable').append(trHTML);

    /*if (waybillFromQueryString != null || waybillFromQueryString != 'undefined'){
        //query status using waybill
        displayStatusChanges(Number(waybillFromQueryString));
    }*/


    function displayVehicleDetails(vehicleId) {
        var json = JSON.parse(localStorage.getItem(vehicleId));
        return json.make + " - " + json.licenseId;
    }

    /*function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] === variable) { return pair[1]; }
        }
        return ("");
    }*/

    function initialize() {
        console.log("Google Maps Initialized");
        // calls PubNub
        // pubs();

        map = new google.maps.Map(document.getElementById('map-canvas'), {
            zoom: 13,
            center: { lat: lat, lng: lng, alt: 0 }
        });

        map_marker = new google.maps.Marker({
            position: { lat: lat, lng: lng },
            map: map,
            title: serialNumber
        });
        map_marker.setMap(map);
    }

    // moves the marker and center of map
    function redraw() {
        map.setCenter({ lat: lat, lng: lng, alt: 0 });
        map_marker.setPosition({ lat: lat, lng: lng, alt: 0 });
        pushCoordToArray(lat, lng);
    }

    function pushCoordToArray(latIn, lngIn) {
        lineCoordinatesArray.push(new google.maps.LatLng(latIn, lngIn));
    }

    function displayMetrics(waybill) {
        var urlAnalytics = restEndPoint + "/analytics?waybill="+ waybill;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: urlAnalytics,
            success: function (result) {
                if (result !== null) {
                    var message = "Distance from Origin : " + result.distanceFromOrigin + "\n"
                        + "Distance to Destination : " + result.distanceToDestination + "\n"
                        + "Total distance : " + result.completeDistanceToDestination + "\n"
                        + "Estimated time of arrival : " + result.estimatedTimeOfArrival;
                    document.getElementById("analytics_details").value = message;
                    lat = parseFloat(result.latitude);
                    lng = parseFloat(result.longitude);
                    redraw();
                }else {
                    document.getElementById("analytics_details").value = "";
                }
            },
            error: function () {
                swal(
                    'Result',
                    'Truck at the depot',
                    'warning'
                );
            }
        });
    }

});
