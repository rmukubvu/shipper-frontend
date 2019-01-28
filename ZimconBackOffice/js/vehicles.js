/**
 * Created by robson on 2017/04/16.
 */

$(document).ready(function () {
    alert("JS");
    if (localStorage.getItem('shipper.userName') === "" ){
        location.href = "/index.html";
        return;
    }

    /*$(document).idle({
        onIdle: function(){
          alert('Since you waited so long, the answer to the Ultimate Question of Life, the Universe, and Everything is 42');
        },
        idle: 10000
      });*/

    var getUrl = restEndPoint + "";
    //var getUrl = "http://127.0.0.1:4567/all/Cargo";
    /*$.ajax({
        type: "GET",
        dataType: "json",
        url: getUrl,
        success: function (result) {
            var html = '' ;
            $.each(result , function(i){
                html += '<option value="' + result[i].schoolId + '">'
                    + result[i].schoolName + '</option>' ;
            });
            $('#schoolsSelector').empty().append(html);
        },
        error: function () {
            swal(
                'Ooops',
                'Something went terribly wrong!',
                'error'
            );
        }
    });*/

    var years = [];
    var currentTime = new Date();
    var startYear = currentTime.getFullYear();
    var html = '';

    for (i = 0 ; i < 20 ; i++){
        console.log(startYear - i);
        years.push(startYear - i);
    }

    $.each(years , function(i){
        html += '<option value="' + years[i] + '">'
            + years[i] + '</option>' ;
    });
    $('#vehicleYearOption').empty().append(html);

    $("#submitVehicle").click(function (event) {
       
        if($.trim($('#licenseNumber').val()) === "") {      
            $('#licenseNumberDiv').removeClass("form-group");     
            $('#licenseNumberDiv').addClass("form-group has-error");
        }else {
            var postUrl = restEndPoint + "/vehicle";            
            /*var postData = {
                'schoolId': $('#schoolsSelector').val(),
                'serialNumber': $('#serialNumber').val().toUpperCase(),
                'deviceType': $("#deviceTypeOptions option:selected").text(),
                'operatingSystem': $("#operatingSystemOptions option:selected").text(),
                'createdDate': moment().format()
            };*/
            var postData = {
                'licenseId' : $('#licenseNumber').val().toUpperCase(),
                'make': $('#vehicleMake').val(), 
                'model': $('#vehicleModelSelector').val(),   
                'year': $('#vehicleYearOption').val(),     
            };

            var postJson = JSON.stringify(postData);
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: postUrl,
                data: postJson,
                success: function (result) {                    
                    swal(
                        'Result',
                        'Vehicle added',
                        'success'
                    );
                },
                error: function (error) {
                    swal(
                        'Ooops',
                        'Failed to add vehicle',
                        'error'
                    );
                }
            });
        }
    });

    $("#submitDriver").click(function (event) {
        if($.trim($('#idPassport').val()) === "") {      
            $('#passportDiv').removeClass("form-group");     
            $('#passportDiv').addClass("form-group has-error");
        }else {
            var postUrl = restEndPoint + "/driver";            
            /*var postData = {
                'schoolId': $('#schoolsSelector').val(),
                'serialNumber': $('#serialNumber').val().toUpperCase(),
                'deviceType': $("#deviceTypeOptions option:selected").text(),
                'operatingSystem': $("#operatingSystemOptions option:selected").text(),
                'createdDate': moment().format()
            };*/
            var postData = {
                'firstName' : $('#firstName').val().toUpperCase(),
                'lastName': $('#lastName').val().toUpperCase(), 
                'nationalId': $('#idPassport').val().toUpperCase(),
                'passportNumber':$('#idPassport').val().toUpperCase(),
                'telephone':$('#driverTelephone').val().toUpperCase(),
                'country': $('#countryOptions').val(),     
            };

            var postJson = JSON.stringify(postData);
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: postUrl,
                data: postJson,
                success: function (result) {                    
                    swal(
                        'Result',
                        'Driver added',
                        'success'
                    );
                },
                error: function (error) {
                    swal(
                        'Ooops',
                        'Failed to add driver',
                        'error'
                    );
                }
            });
        }
    });
});
