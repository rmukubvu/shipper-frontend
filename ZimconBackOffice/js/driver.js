console.log('i am in driver');
//allocate driver to vehicle
//for status update
$("#submitDriverAllocation").click(function (event) {
    window.FakeLoader.showOverlay();   
    var vehicleId = $("#vehicleSelector").val();
    var driverId = $("#driverSelector").val();

    var postModel = {
        'vehicleId': vehicleId,           
        'driverId': driverId,
        'allocatedStartDate': new Date(),
        'allocatedEndDate': null
    };
    var postJson = JSON.stringify(postModel);
    var postUrl = "../api/driveralloc";
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: postUrl,
        data: postJson,
        success: function (result) {
            console.log(result);
            window.FakeLoader.hideOverlay();
            swal("Result", "Driver has been allocated", "success").then(function () {
                location.reload();
            });
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

$("#submitDriver").click(function (event) {
    window.FakeLoader.showOverlay();
    var postUrl = "../api/driver";
    var postData = {       
        'firstName': $("#firstName").val(),
        'lastName': $("#lastName").val(),
        'nationalId': $("#saId").val(),
        'passportNumber': $("#passport").val(),
        'telephone': $("#telephone").val(),
        'country': $("#countrySelector").val()
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
            swal("Result", "Driver has been added", "success").then(function () {
                location.reload();
            });
        },
        error: function (error) {
            swal(
                'Ooops',
                'Failed to create driver',
                'error'
            );
        }
    });
});
