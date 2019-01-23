$(document).ready(function () {
    $("#submitDeviceAllocation").click(function (event) {
        //console.log($('#deviceSelector').val());vehicleSelector
        var postUrl = "../api/devicealloc";
        var postData = {
            'vehicleId': $('#vehicleSelector').val(),
            'deviceId': $('#deviceSelector').val(),
            'allocationDate': new Date()
        };

        var postJson = JSON.stringify(postData);
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: postUrl,
            data: postJson,
            success: function (result) {
                swal(
                    'Result',
                    'Device has been allocated',
                    'success'
                );
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
})