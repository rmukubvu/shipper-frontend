console.log('i am in driver');
$('#driverAllocationForm').bootstrapValidator({
    message: 'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        driverSelector: {
            validators: {
                notEmpty: {
                    message: '* Please select driver'
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
$('#driverForm').bootstrapValidator({
    message: 'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        countrySelector: {
            validators: {
                notEmpty: {
                    message: '* Please select country'
                }
            }
        },
        firstName: {
            message: 'The first name is not valid',
            validators: {
                notEmpty: {
                    message: '* First name is required and cannot be empty'
                }
            }
        },
        lastName: {
            message: 'The last name is not valid',
            validators: {
                notEmpty: {
                    message: '* Last name is required and cannot be empty'
                }
            }
        },
        saId: {
            message: 'The last name is not valid',
            validators: {
                notEmpty: {
                    message: '* Identification is required and cannot be empty'
                },
                stringLength: {
                    min: 13,
                    max: 13,
                    message: 'South African ID must be more than 13 digits long'
                }
            }
        },
        telephone: {
            validators: {
                notEmpty: {
                    message: '* Telephone number is required and cannot be empty'
                },
                stringLength: {
                    min: 10,
                    max: 10,
                    message: 'Telephone number must be more than 10 digits long'
                }
            }
        }
    }
});
//allocate driver to vehicle
//for status update
//$("#submitDriverAllocation").click(function (event) {
$('#driverAllocationForm').validator().on('submit', function (e) {
    if (!e.isDefaultPrevented()) {
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
}
});


//$("#submitDriver").click(function (event) {
    $('#driverForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
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
        }
});
