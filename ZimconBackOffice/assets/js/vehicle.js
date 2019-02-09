/**
 * Created by robson on 2017/04/16.
 */

$(document).ready(function () {

    var years = [];
    var currentTime = new Date();
    var startYear = currentTime.getFullYear();
    var html = "";

    for (var i = 0; i < 20; i++) {
        years.push(startYear - i);
    }

    $.each(years, function (i) {
        html += '<option value="' + years[i] + '">'
            + years[i] + "</option>";
    });

    $("#vehicleYearOption").empty().append(html);

    $('#vehicleForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            vehicleMakeSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select vehicle make'
                    }
                }
            },
            vehicleModelSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select vehicle model'
                    }

                }
            },
            vehicleYearOption: {
                validators: {
                    notEmpty: {
                        message: '* Please select vehicle model'
                    }
                }
            },
            licenseNumber: {
                message: 'The License Number is not valid',
                validators: {
                    notEmpty: {
                        message: '* License Number is required and cannot be empty'
                    },
                    stringLength: {
                        min: 6,
                        max: 10,
                        message: 'Invalid License Number'
                    }
                }
            }
        }
    });

    $(".editVehicle").click(function (event) {
        //clearDataFields();
        //$("#fakeloader").fakeLoader();
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var license = $(this).closest("tr").find('td:eq(1)').text();
        var make = $(this).closest("tr").find('td:eq(2)').text();
        var model = $(this).closest("tr").find('td:eq(3)').text();
        var year = $(this).closest("tr").find('td:eq(4)').text();

        $("#vehicleId").val(id);
        $("#licenseNumber").val(license);
        $("#vehicleMakeSelector").val(make);
        $("#vehicleModelSelector").val(model);
        $("#vehicleYearOption").val(year);

    });


    //$("#submitVehicle").click(function (event) {
    $('#vehicleForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            if ($.trim($("#licenseNumber").val()) === "") {
                $("#licenseNumberDiv").removeClass("form-group");
                $("#licenseNumberDiv").addClass("form-group has-error");
            }
            else {
                var postUrl = "../api/vehicle";
                var postData = {
                    'id': $("#vehicleId").val(),
                    'licenseId': $("#licenseNumber").val().toUpperCase(),
                    'make': $("#vehicleMakeSelector").val(),
                    'model': $("#vehicleModelSelector").val(),
                    'year': $("#vehicleYearOption").val()
                };

                submitVehicle(postUrl, postData);
            }
        }
    });

    //submit vehicle
    var submitVehicle = function (postUrl, postData) {

        var postJson = JSON.stringify(postData);
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: postUrl,
            data: postJson,
            success: function (result) {
                swal(
                    "Result",
                    "Vehicle added",
                    "success"
                );
                clearDataFields();
                location.reload();

            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add vehicle",
                    "error"
                );

                location.reload();

            }
        });
    };
    
});
