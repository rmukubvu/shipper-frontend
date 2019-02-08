/**
 * Created by robson on 2017/04/16.
 */
$(document).ready(function() {

    localStorage.clear();

    $("#shipper_login_button").click(function (event) { 
        window.FakeLoader.showOverlay();
        var userName = $('#shipper_username').val();
        var password = $('#shipper_password').val();

        var getUrl = "../api/login?userName=" + userName + "&password=" + password;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: getUrl,
            success: function (result) {                
                if (result.error === false) {
                    if (result.admin === true) {
                        var sessionTimeout = 1; //hours
                        var loginDuration = new Date();
                        localStorage.setItem('shipper.isAdmin', true);
                        localStorage.setItem('shipper.fullName', result.user.firstName + " " + result.user.lastName);
                        localStorage.setItem('shipper.userName', result.user.emailAddress);
                        document.cookie = "ShipperSession=Valid; " + loginDuration.toGMTString() + "; path=/";
                        window.location.href = "/Dashboard";
                    } else {
                        localStorage.setItem('shipper.isAdmin', false);
                        localStorage.setItem('shipper.consignee', result.user.consigneeId);
                        localStorage.setItem('shipper.fullName', result.user.firstName + " " + result.user.lastName);
                        localStorage.setItem('shipper.userName', result.user.emailAddress);
                        window.location.href = "/Client";
                    }                 
                } else {
                    swal(
                        'Ooops',
                        result.loginErrorMessage,
                        'error'
                    );                   
                }
            },
            error: function () {
                swal(
                    'Ooops',
                     'Something went terribly wrong!',
                    'error'
                );
            }
        });
    });
});
