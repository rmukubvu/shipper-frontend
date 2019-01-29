/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {

    $("#submitUser").click(function (event) {
        var id = $("#consigneeId").val();

        var postUrl = "";
        var postData = {
            'consigneeId': $("#name").val(),
            'createdDate': $("#address").val(),
            'emailAddress': $("#address2").val(),
            'id': $("#contact").val(),
            'password': $("#countrySelector").val()
        };

        if (id === "") {//new vehicle
            postUrl = "../api/user";
        } else {//edit vehicle
            postUrl = "../api/user/" + id;
        }

        submitConsignee(postUrl, postData);

    });

    $(".editUser").click(function (event) {

        //get user by ID
        //getUser();
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var email = $(this).closest("tr").find('td:eq(1)').text();

        $("#userId").val(id);
        $("#name").val(email);
        $("#email").val(email);
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
                swal(
                    "Result",
                    "Consignee added",
                    "success"
                );
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add consignee",
                    "error"
                );
                location.reload();
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
                swal(
                    "Result",
                    "User added",
                    "success"
                );
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add user",
                    "error"
                );
                location.reload();
            }
        });


    };

    var clearDataFields = function () {
        $("#consigneeId").val("");
        $("#name").val("");
        $("#address").val("");
        $("#address2").val("");
        $("#contact").val("");
        $("#countrySelector").val("");
    };
});
