/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {
    $(".editDeviceAllocation").click(function (event) {
        var allocationId = $(this).closest("tr").find('td:eq(0)').text();
        var vehicleId = $(this).closest("tr").find('td:eq(1)').text();
        var deviceId = $(this).closest("tr").find('td:eq(2)').text();
        var deviceMake = $(this).closest("tr").find('td:eq(3)').text();
        var deviceModel = $(this).closest("tr").find('td:eq(4)').text();
        var vehicleLicense = $(this).closest("tr").find('td:eq(5)').text();
        var vehicleMake = $(this).closest("tr").find('td:eq(6)').text();
        var vehicleModel = $(this).closest("tr").find('td:eq(7)').text();

        $('#allocationId').val(allocationId);
        $("#deviceSelector").val(deviceId);
        $("#vehicleSelector").val(vehicleId);

    });

    $("#submitDeviceAllocation").click(function (event) {
        //console.log($('#deviceSelector').val());vehicleSelector
        var postUrl = "../api/devicealloc";
        var postData = {
            'id': $("#allocationId").val(),
            'vehicleId': $("#vehicleSelector").val(),
            'deviceId': $("#deviceSelector").val(),
            'allocationDate': new Date()
        };

        $('#AjaxLoader').show(); 
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
                location.reload();
                $('#AjaxLoader').hide();  
            },
            error: function (error) {
                swal(
                    'Ooops',
                    'Failed to allocate device',
                    'error'
                );
                location.reload();
                $('#AjaxLoader').hide();  
            }
        });

    });

    

    //var clearDataFields = function () {
    //    $("#consigneeId").val("");
    //    $("#name").val("");
    //    $("#address").val("");
    //    $("#address2").val("");
    //    $("#contact").val("");
    //    $("#countrySelector").val("");
    //};
});
