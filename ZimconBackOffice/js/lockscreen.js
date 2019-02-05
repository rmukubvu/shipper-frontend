$(document).ready(function() {

    $('#lockedUserName').text(localStorage.getItem('shipper.fullName'));

    $('.btn').click(function (event) {
        window.FakeLoader.showOverlay();
        var userName = localStorage.getItem('shipper.userName');
        var password = $('#lockscreen_password').val();
        var getUrl = restEndPoint + "/login?email=" + userName + "&password=" + password;
        $.ajax({
            url:getUrl,
            type: "GET",           
            success: function (result) {
                //close 
                window.FakeLoader.hideOverlay();
                if ( result.error === true ){
                    swal(
                        'Ooops',
                        result.loginErrorMessage,
                        'error'
                    );
                }else{
                    //open new window pass consgineeid
                    localStorage.setItem('shipper.fullName', result.user.firstName + " " + result.user.lastName);
                    localStorage.setItem('shipper.userName',result.user.emailAddress);
                    window.location.href = "/Dashboard";
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