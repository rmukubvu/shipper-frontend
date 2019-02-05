/**
 * Created by robson on 2017/04/16.
 */
$(document).ready(function () {   
    $("#shipper_recover_button").click(function (event) {     
        window.FakeLoader.showOverlay();
        var userName = $('#shipper_emailAddress').val();
        var getUrl = "../api/recoverPassword?userName=" + userName;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: getUrl,
            success: function (result) {
                window.FakeLoader.hideOverlay();
                if (result.error === false) {                   
                    swal("Good Job", result.message, "success").then(function () {
                        window.location.href = "/Login";
                    });
                } else {
                    swal(
                        'Ooops',
                        result.message,
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
