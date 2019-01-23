/**
 * Created by robson on 2017/04/16.
 */
$(document).ready(function() {

    localStorage.clear();

    $("#shipper_login_button").click(function (event) {
       
        var userName = $('#shipper_username').val();
        var password = $('#shipper_password').val();

        var getUrl = "../api/login?userName=" + userName + "&password=" + password;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: getUrl,
            success: function (result) {       
                //var result = JSON.parse(json);
                if (result.error === false) {
                    var sessionTimeout = 1; //hours
                    var loginDuration = new Date();
                    loginDuration.setTime(loginDuration.getTime()+(sessionTimeout*60*60*1000));
                    //localStorage.setItem('shipper.consignee',result.user.consigneeId);
                    localStorage.setItem('shipper.userName',result.user.emailAddress);
                    document.cookie = "ShipperSession=Valid; "+ loginDuration.toGMTString()+ "; path=/";
                    window.location.href = "/Home";
                } else {
                    swal(
                        'Ooops',
                        result.loginErrorMessage,
                        'error'
                    );
                    //window.location.href = "/Home";
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
