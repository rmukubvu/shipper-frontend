/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {

    $('#userForm').bootstrapValidator({
        message: 'This value is not valid',        
        fields: {
            consigneeSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select consignee'
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
            email: {

            },
            password: {
                validators: {
                    notEmpty: {
                        message: '* Password is required and cannot be empty'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Password must be more than 6 characters long'
                    }
                }
            },
            password2: {
                validators: {
                    notEmpty: {
                        message: '* Password is required and cannot be empty'
                    },
                    identical: {
                        field: 'password',
                        message: 'Passwords do not match'
                    }
                }
            }
        }
    });

    $(".editUser").click(function (event) {
        //get user by ID
        //getUser();
        var userId = $(this).closest("tr").find('td:eq(0)').text();
        var consigneeId = $(this).closest("tr").find('td:eq(1)').text();
        var password = $(this).closest("tr").find('td:eq(2)').text();
        var createdDate = $(this).closest("tr").find('td:eq(3)').text();
        var consigneeName = $(this).closest("tr").find('td:eq(4)').text();
        var name = $(this).closest("tr").find('td:eq(5)').text();
        var firstName = $(this).closest("tr").find('td:eq(6)').text();
        var lastName = $(this).closest("tr").find('td:eq(7)').text();
        var email = $(this).closest("tr").find('td:eq(8)').text();
        var contact = $(this).closest("tr").find('td:eq(9)').text();
        var country = $(this).closest("tr").find('td:eq(10)').text();

        $("#userId").val(userId);
        $("#consigneeId").val(consigneeId);
        $("#consigneeSelector").val(consigneeId); 
        $("#firstName").val(firstName);
        $("#lastName").val(lastName);
        $("#email").val(email);
        $("#password").val(password);
        $("#password2").val(password);
        $("#createdDate").val(createdDate);
    });

    $("#submitUser").click(function (event) {
        window.FakeLoader.showOverlay();
        //for updates for user 
        var postUrl = "../api/updateUser";
        if ($("#userId").val() === "" || $("#userId").val() === "undefined") {
            postUrl = "../api/user";
        }
        
        var postData = {
            'id': $("#userId").val(),
            'consigneeId': $('#consigneeSelector').val(),
            'createdDate': new Date($.now()),
            'firstName': $("#firstName").val(),
            'lastName': $("#lastName").val(),
            'emailAddress': $("#email").val(),
            'password': $("#password").val()
        };
        submitUser(postUrl, postData);
    });

    var submitUser = function (postUrl, postData) {        
        var postJson = JSON.stringify(postData);
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: postUrl,
            data: postJson,
            success: function (result) {
                window.FakeLoader.hideOverlay();
                swal("Result", "Added succesfully", "success").then(function () {
                    location.reload();
                });             
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add consignee",
                    "error"
                );                          
            }
        });


    };

    var getUser = function (postUrl, postData) {
        var postJson = JSON.stringify(postData);
        $.ajax({
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: postUrl,
            data: postJson,
            success: function (result) {
                swal("Result", "Added succesfully", "success").then(function () {
                    location.reload();
                });               
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add user",
                    "error"
                );                               
            }
        });


    };

    //var clearDataFields = function () {
    //    $("#consigneeId").val("");
    //    $("#name").val("");
    //    $("#address").val("");
    //    $("#address2").val("");
    //    $("#contact").val("");
    //    $("#countrySelector").val("");
    //};
});
