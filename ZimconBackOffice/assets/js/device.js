/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {
    $("#submitDeviceAllocation").click(function (event) {
        //console.log($('#deviceSelector').val());vehicleSelector
        var postUrl = "../api/devicealloc";
        var postData = {
            'vehicleId': $('#vehicleSelector').val(),
            'deviceId': $('#deviceSelector').val(),
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

    $(".editDeviceAllocation").click(function (event) {
        var allocationId = $(this).closest("tr").find('td:eq(0)').text();
        var vehicleId = $(this).closest("tr").find('td:eq(1)').text();
        var deviceId = $(this).closest("tr").find('td:eq(2)').text();
        var deviceMake = $(this).closest("tr").find('td:eq(3)').text();
        var deviceModel = $(this).closest("tr").find('td:eq(4)').text();
        var vehicleLicense = $(this).closest("tr").find('td:eq(5)').text();
        var vehicleMake = $(this).closest("tr").find('td:eq(6)').text();
        var vehicleModel= $(this).closest("tr").find('td:eq(7)').text();

        $('#deviceSelector > option:selected').text("" + deviceMake + ' - ' + deviceModel + ' - ' + deviceId + "");
        $('#vehicleSelector > option:selected').text("" + vehicleModel + ' - ' + vehicleLicense + "");
        //$('#deviceSelector option[value='+deviceId+']').html("" + deviceMake + ' - ' + deviceModel + ' - ' + deviceId + "");
        ////$('#select option[value="someValue"]').text("newText")
        //$('#vehicleSelector option').html("" + vehicleModel + ' - ' + vehicleLicense + "");

        //var selectedOption = '<option>' + deviceMake + ' - ' + deviceModel + ' - '+deviceId+'</option>';
        //$("#deviceSelector").append('<option>' + deviceMake + ' - ' + deviceModel + ' - ' + deviceId + '</option>');

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
