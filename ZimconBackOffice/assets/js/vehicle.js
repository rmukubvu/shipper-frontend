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

    $("#submitVehicle").click(function (event) {
        if ($.trim($("#licenseNumber").val()) === "") {
            $("#licenseNumberDiv").removeClass("form-group");
            $("#licenseNumberDiv").addClass("form-group has-error");
        } else {
            var postUrl = "../api/vehicle";
            var postData = {
                'licenseId': $("#licenseNumber").val().toUpperCase(),
                'make': $("#vehicleMake").val(),
                'model': $("#vehicleModelSelector").val(),
                'year': $("#vehicleYearOption").val(),
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
});
