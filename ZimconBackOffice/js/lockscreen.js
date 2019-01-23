$(document).ready(function() {

    $('#lockedUserName').text(localStorage.getItem('shipper.userName'));

    $('.btn').click(function (event){
        var userName = localStorage.getItem('shipper.userName');
        var password = $('#lockscreen_password').val();
        var getUrl = restEndPoint + "/login?email=" + userName + "&password=" + password;
        $.ajax({
            url:getUrl,
            type: "GET",           
            success: function (result) {
                //close                
                if ( result.error === true ){
                    swal(
                        'Ooops',
                        result.loginErrorMessage,
                        'error'
                    );
                }else{
                    //open new window pass consgineeid
                    localStorage.setItem('shipper.consignee',result.user.consigneeId);
                    localStorage.setItem('shipper.userName',result.user.emailAddress);
                    window.location.href = "shipper.html";
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