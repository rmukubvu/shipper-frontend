var restEndPointProd = "http://23.100.43.140";
var restEndPoint = "http://localhost:8089";

//not authorised kickout
if (localStorage.getItem('shipper.userName') === "") {
    location.href = "/Login";    
}

var lineCoordinatesArray = [];
var lat = -26.195246;
var lng = 28.034088;
var map;
var markers = [];
var map_marker;

$(document).idle({
  onIdle: function(){
      location.href = "/Login/LockScreen";
  },
  idle: 600000
});

var isAdmin = localStorage.getItem('shipper.isAdmin');
if (isAdmin === "true") {
    $('#side_main_menu').append(
        `<li class="header">MENU</li>
         <li><a href="/Dashboard"><i class="fa fa-location-arrow"></i><span>Dashboard</span></a>
         <li><a href="/User"><i class="fa fa-user"></i><span>Users</span></a></li>
         <li><a href="/User/Admin"><i class="fa fa-user"></i><span>Admin User</span></a></li>
         <li><a href="/Vehicle"><i class="fa fa-automobile"></i> <span>Vehicles</span></a></li>
         <li><a href="/Driver"><i class="fa fa-user"></i> <span>Driver</span></a></li>
         <li><a href="/Consignee"><i class="fa fa-database"></i> <span>Consignee</span></a></li>
         <li><a href="/Device"><i class="fa fa-mobile"></i> <span>Device</span></a></li>
         <li><a href="/Shipment"><i class="fa fa-truck"></i> <span>Shipment</span></a></li>
         <li><a href="/Notification"><i class="fa fa-exchange"></i> <span>Notifications</span></a></li>
         <li><a href="/Login"><i class="fa fa-sign-out"></i> <span>Log out</span></a></li>
        `);
} else {
    $('#side_main_menu').append(
        `<li class="header">MENU</li>
         <li> <a href="/Client"><i class="fa fa-location-arrow"></i><span>Dashboard</span></a>         
         <li><a href="/Client/Shipment"><i class="fa fa-gears"></i> <span>Shipment</span></a></li>         
         <li><a href="/Login"><i class="fa fa-sign-out"></i> <span>Log out</span></a></li>
        `);
}


function connectToForNotificationsMqtt() {
    //Using the HiveMQ public Broker, with a random client Id
    var client = new Paho.MQTT.Client("localhost", 8083, "myclientid_" + parseInt(Math.random() * 100, 10));
    //Gets  called if the websocket/mqtt connection gets disconnected for any reason
    client.onConnectionLost = function (responseObject) {
        //Depending on your scenario you could implement a reconnect logic here
        alert("connection lost: " + responseObject.errorMessage);
    };
    
    //Gets called whenever you receive a message for your subscriptions
    var trHTML = '';
    client.onMessageArrived = function (message) {        
        if (message.destinationName === "location") {
            var res = message.payloadString.split("|");
            var vehicleId = res[0];
            lat = parseFloat(res[1]);
            lng = parseFloat(res[2]);
            console.log("coordinates for " + vehicleId);
            //custom method
            redraw(vehicleId);
        } else {
            var dateNow = new Date();
            trHTML = '<tr><td>' + dateNow.toLocaleString() + '</td><td>' + message.payloadString + '</td></tr>';
            $('#notifications_table').append(trHTML);
        }
    };
    
    // connect the client
    client.connect({ onSuccess: onConnect });
    // called when the client connects
    function onConnect() {
        //Once a connection has been made, make a subscription and send a message.
        console.log("onConnect");
        if (window.location.href.indexOf("Dashboard") > - 1) {
            pushMarkersAsTheyCome();
        }        
        client.subscribe("notifications");
        client.subscribe("location");
        var message = new Paho.MQTT.Message("Ready to receive ......");
        message.destinationName = "notifications";
        client.send(message);
    }
            
}

function pushMarkersAsTheyCome() {   
           
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 16,
        center: { lat: lat, lng: lng, alt: 0 }
    });

    var icon = { // car icon
        path: 'M29.395,0H17.636c-3.117,0-5.643,3.467-5.643,6.584v34.804c0,3.116,2.526,5.644,5.643,5.644h11.759   c3.116,0,5.644-2.527,5.644-5.644V6.584C35.037,3.467,32.511,0,29.395,0z M34.05,14.188v11.665l-2.729,0.351v-4.806L34.05,14.188z    M32.618,10.773c-1.016,3.9-2.219,8.51-2.219,8.51H16.631l-2.222-8.51C14.41,10.773,23.293,7.755,32.618,10.773z M15.741,21.713   v4.492l-2.73-0.349V14.502L15.741,21.713z M13.011,37.938V27.579l2.73,0.343v8.196L13.011,37.938z M14.568,40.882l2.218-3.336   h13.771l2.219,3.336H14.568z M31.321,35.805v-7.872l2.729-0.355v10.048L31.321,35.805',
        scale: 0.4,
        fillColor: "#427af4", //<-- Car Color, you can change it 
        fillOpacity: 1,
        strokeWeight: 1,
        anchor: new google.maps.Point(0, 5),
        rotation: 45 //<-- Car angle
    };

    var coordinates = { lat: lat, lng: lng };
    map_marker = new google.maps.Marker({
        position: coordinates,
        icon: icon,
        map: map
    });
    map_marker.setMap(map);
    
}

function redraw(vehicleId) {
    map.setCenter({ lat: lat, lng: lng });
    map_marker.setPosition({ lat: lat, lng: lng });
    google.maps.event.addListener(map_marker, 'click', function () {
        location.href = "/Shipment/ViewShipments?id=" + vehicleId;
    });
    pushCoordToArray(lat, lng);
}

function pushCoordToArray(latIn, lngIn) {
    lineCoordinatesArray.push(new google.maps.LatLng(latIn, lngIn));
}

/* change password */
$("#submitChangePassword").click(function (event) {   
    window.FakeLoader.showOverlay();
    var password1 = $("#actualPassword").val();
    var password2 = $("#actualPassword2").val();
    if (password1 !== password2) {
        document.getElementById("actualPassword").style.borderColor = "#E34234";
        document.getElementById("actualPassword2").style.borderColor = "#E34234";
        swal(
            "Ooops",
            "Passwords do not match!",
            "error"
        );
    } else {

        var getUrl = "../api/changePassword?email=" + localStorage.getItem("shipper.userName") + "&password=" + password1;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: getUrl,
            success: function (result) {
                window.FakeLoader.hideOverlay();
                swal(
                    "Ooops",
                    "Password updated successfully",
                    "success"
                );
            },
            error: function () {
                window.FakeLoader.hideOverlay();
                swal(
                    "Ooops",
                    "Something went terribly wrong!",
                    "error"
                );
            }
        });
    }
});

//only interested in dashboard and notifications page
if (window.location.href.indexOf("Dashboard") > - 1 || window.location.href.indexOf("Notification") > - 1) {
    connectToForNotificationsMqtt();
}

//for global search
$("#search-btn-waybill").click(function (event) {
    var waybillNumber = $('#globalSearchValue').val();
    location.href = "/Shipment/Search?id=" + waybillNumber;
});

//for status update
$("#submitUpdateStatus").click(function (event) {
    window.FakeLoader.showOverlay();
    var manifestReference = $("#manifestReference").val();
    var waybill = $("#wayBillNumber").val();
    var vehicleId = $("#vehicleId").val();
    var statusId = $("#statusSelector").val();

    var postModel = {
        'vehicleId': vehicleId,
        'manifestReference': manifestReference,
        'wayBillNumber': waybill,
        'statusId': statusId,
        'createdDate': new Date()
    };

    var postUrl = "../api/updateStatus";
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: postUrl,
        data: postModel,
        success: function (result) {
            window.FakeLoader.hideOverlay();
            swal(
                "Result",
                "Status Updated Successfully",
                "success"
            );
        },
        error: function () {
            window.FakeLoader.hideOverlay();
            swal(
                "Ooops",
                "Something went terribly wrong!",
                "error"
            );
        }
    });    
});

//for admin user
$(".editUserForUpdate").click(function (event) {
    //get user by ID
    //getUser();
    var userId = $(this).closest("tr").find('td:eq(0)').text();   
    var firstName = $(this).closest("tr").find('td:eq(2)').text();
    var lastName = $(this).closest("tr").find('td:eq(3)').text();
    var email = $(this).closest("tr").find('td:eq(4)').text();

    $("#userId").val(userId);   
    $("#firstName").val(firstName);
    $("#lastName").val(lastName);
    $("#email").val(email);
    
});

//for admin services ....
$("#submitAdminUser").click(function (event) {
    window.FakeLoader.showOverlay();
    var userId = $("#userId").val();

    var postUrl = "../api/userAdmin";
    var postData = {       
        'isAdmin': true,
        'userId': userId
    };

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: postUrl,
        data: postData,
        success: function (result) {
            window.FakeLoader.hideOverlay();
            swal(
                "Result",
                "User is now an Admin",
                "success"
            );
        },
        error: function () {
            window.FakeLoader.hideOverlay();
            swal(
                "Ooops",
                "Something went terribly wrong!",
                "error"
            );
        }
    });
});

//save device
$("#submitDeviceAllocation").click(function (event) {
    window.FakeLoader.showOverlay();
    var postUrl = "../api/devicealloc";
    var postData = {
        'id': $("#allocationId").val(),
        'vehicleId': $("#vehicleSelector").val(),
        'deviceId': $("#deviceSelector").val(),
        'allocationDate': new Date()
    };

    //  
    var postJson = JSON.stringify(postData);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: postUrl,
        data: postJson,
        success: function (result) {
            window.FakeLoader.hideOverlay();
            swal("Result", "Device has been allocated", "success").then(function () {
                location.reload();
            });
        },
        error: function (error) {
            swal(
                'Ooops',
                'Failed to allocate device',
                'error'
            );
        }
    });
});

//save the shipment
$("#submitShipment").click(function (event) {
    window.FakeLoader.showOverlay();
    var postUrl = "../api/package";
    var vehicleId = $("#vehicleSelector").val();
    var consigneeId = $("#consigneeSelector").val();
    var destinationAddress = $("#search_location").val();
    var sourceAddress = $("#address").val();
    var contents = $("#shipmentContents").val();
    /*var destinationLatitude = $("#vehicleSelector").val();
    var destinationLongitude = $("#vehicleSelector").val();
    var sourceLatitude = $("#vehicleSelector").val();
    var sourceLongitude = $("#vehicleSelector").val();*/
    //https://stackoverflow.com/questions/1042885/using-google-maps-api-to-get-travel-time-data to get estimated time of arrivval
    var postData = {
        'consigneeId': consigneeId,        
        'destinationAddress': destinationAddress,
        'loadedDate': new Date(),
        'manifestReference': "",
        'sourceAddress': sourceAddress,
        'wayBillNumber': 0,
        'vehicleId': vehicleId,
        'contents' : contents
    };
    var postJson = JSON.stringify(postData);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: postUrl,
        data: postJson,
        success: function (result) {
            window.FakeLoader.hideOverlay();
            swal("Result", "Shipment loaded", "success").then(function () {
                reloadCargo(vehicleId);
            }); 
        },
        error: function () {
            swal(
                'Ooops',
                'Something went terribly wrong!',
                'error'
            );
        }
    });    
});

//load cargo on truck once saved
function reloadCargo(vehicleId) {
    var cargoUrl = "../api/cargoOnVehicle?vehicleId=" + vehicleId;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: cargoUrl,
        success: function (result) {
            //$('#loadedShipmentTable').text(""); clear table or body contents and overwrite with loaded ones
            var trHTML = '';
            $.each(result, function (i, item) {
                trHTML += '<tr><td>' + item.consignee + '</td><td>' + item.manifestReference + '</td><td>' + item.status + '</td><td>' + item.wayBill + '</td></tr>';
            });
            $('#loadedShipmentTable').append(trHTML);
        },
        error: function () {
            swal(
                'Ooops',
                'Something went terribly wrong!',
                'error'
            );
        }
    });  
}