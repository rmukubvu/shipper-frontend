$(document).ready(function() {

    $('#lockedUserName').text(localStorage.getItem('shipper.fullName'));
    $('#lockScreenButton').click(function (event) {
        window.FakeLoader.showOverlay();
        var userName = localStorage.getItem('shipper.userName');
        var password = $('#lockscreen_password').val();
        var getUrl = "../api/login?userName=" + userName + "&password=" + password;
        $.ajax({
            url:getUrl,
            type: "GET",           
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
            error: function (xhr, status, error) {
                swal(
                    'Ooops',
                    'Something went terribly wrong!',
                    'error'
                );
            }
        });
    });

});