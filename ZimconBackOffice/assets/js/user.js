/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {

    $(".editUser").click(function (event) {

        //get user by ID
        //getUser();
        var userId = $(this).closest("tr").find('td:eq(0)').text();
        var consigneeId = $(this).closest("tr").find('td:eq(1)').text();
        var password = $(this).closest("tr").find('td:eq(2)').text();
        var createdDate = $(this).closest("tr").find('td:eq(3)').text();
        var consigneeName = $(this).closest("tr").find('td:eq(4)').text();
        var email = $(this).closest("tr").find('td:eq(5)').text();
        var contact = $(this).closest("tr").find('td:eq(6)').text();
        var country = $(this).closest("tr").find('td:eq(7)').text();

        $("#userId").val(userId);
        $("#consigneeId").val(consigneeId);
        $("#consigneeName").val(consigneeName);
        $("#email").val(email);
        $("#password").val(password);
        $("#password2").val(password);
        $("#createdDate").val(createdDate);
    });

    $("#submitUser").click(function (event) {
        var id = $("#userId").val();

        var postUrl = "../api/user";
        var postData = {
            'id': $("#userId").val(),
            'consigneeId': $("#consigneeId").val(),
            'createdDate': new Date($.now()),//$("#createdDate").val(),
            'emailAddress': $("#email").val(),
            'password': $("#password").val()
        };

        submitUser(postUrl, postData);

    });

    var submitUser = function (postUrl, postData) {
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
                    "Result",
                    "Consignee added",
                    "success"
                );
                location.reload();
                $('#AjaxLoader').hide();  
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add consignee",
                    "error"
                );
                location.reload();
                $('#AjaxLoader').hide();  
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
                location.reload();
                $('#AjaxLoader').show();  
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add user",
                    "error"
                );
                location.reload();
                $('#AjaxLoader').hide();  
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
