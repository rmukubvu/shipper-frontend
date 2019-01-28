/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {
    var username = "";


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

    $(".editVehicle").click(function (event) {
        clearDataFields();

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

    $("#submitVehicle").click(function (event) {

        if ($.trim($("#licenseNumber").val()) === "") {
            $("#licenseNumberDiv").removeClass("form-group");
            $("#licenseNumberDiv").addClass("form-group has-error");
        } else {
            var vehicleId = $("#vehicleId").val();

            var postUrl = "";
            var postData = {
                'licenseId': $("#licenseNumber").val().toUpperCase(),
                'make': $("#vehicleMakeSelector").val(),
                'model': $("#vehicleModelSelector").val(),
                'year': $("#vehicleYearOption").val()
            };

            
            if (vehicleId === "") {
                //new vehicle
                postUrl = "../api/vehicle";
            } else {
                //edit vehicle
                postUrl = "../api/vehicle/" + vehicleId;
            }

            submitVehicle(postUrl, postData);
            //var postJson = JSON.stringify(postData);
            //$.ajax({
            //    type: "POST",
            //    dataType: "json",
            //    contentType: "application/json; charset=utf-8",
            //    url: postUrl,
            //    data: postJson,
            //    success: function (result) {
            //        swal(
            //            "Result",
            //            "Vehicle added",
            //            "success"
            //        );
            //    },
            //    error: function (error) {
            //        swal(
            //            "Ooops",
            //            "Failed to add vehicle",
            //            "error"
            //        );
            //        location.reload();
            //    }
            //});
        }
    });

    $("#submitDriver").click(function (event) {
        if ($.trim($("#idPassport").val()) === "") {
            $("#passportDiv").removeClass("form-group");
            $("#passportDiv").addClass("form-group has-error");
        } else {
            var postUrl = "../api/driver";
            var postData = {
                'firstName': $("#firstName").val().toUpperCase(),
                'lastName': $("#lastName").val().toUpperCase(),
                'nationalId': $("#idPassport").val().toUpperCase(),
                'passportNumber': $("#idPassport").val().toUpperCase(),
                'telephone': $("#driverTelephone").val().toUpperCase(),
                'country': $("#countryOptions").val(),
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
                        "Result",
                        "Driver added",
                        "success"
                    );
                },
                error: function (error) {
                    swal(
                        "Ooops",
                        "Failed to add driver",
                        "error"
                    );
                }
            });
        }
    });

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

        clearDataFields();
    };

    var clearDataFields = function () {
        $("#vehicleId").val("");
        $("#licenseNumber").val("");
        $("#vehicleMakeSelector").val("");
        $("#vehicleModelSelector").val("");
        $("#vehicleYearOption").val("");
    }
});
