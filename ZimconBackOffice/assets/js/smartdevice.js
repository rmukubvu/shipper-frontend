$(document).ready(function () {

    $('#deviceForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            deviceSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select smart device'
                    }
                }
            },
            vehicleSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select vehicle'
                    }
                }
            }
        }
    });

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


    $('#deviceForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            var postUrl = "../api/devicealloc";
            var postData = {
                'id': $("#allocationId").val(),
                'vehicleId': $("#vehicleSelector").val(),
                'deviceId': $("#deviceSelector").val(),
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
                        'Device successfully allocated',
                        'success'
                    );
                    location.reload();

                },
                error: function (error) {
                    swal(
                        'Ooops',
                        'Failed to allocate device',
                        'error'
                    );
                    location.reload();

                }
            });
        }
    });
});
