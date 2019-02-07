/**
 * Created by robson on 2017/04/16.
 */
$(document).ready(function() {       
        
        var consigneeId = localStorage.getItem('shipper.consignee');
        if (consigneeId === ""){
           //do nothing ... come with a plan for dashboard for back office     
        }else{
        
            var getUrlVehicles = restEndPoint + "/vehicle";
            var getUrlShipments = restEndPoint + "/shipment/consignee?consigneeId=" + consigneeId;
            var getUrlShipmentsWithStatus = restEndPoint + "/shipment/status/consignee?consigneeId=" + consigneeId;
            var getUrlConsigneeDetails = restEndPoint + "/consignee/id?id=" + consigneeId;
            
            var cachedShipmentsResults = localStorage.getItem('shipper.Shipments');          
            if (cachedShipmentsResults === null || cachedShipmentsResults === ""){
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: getUrlShipmentsWithStatus,
                    success: function (result) {
                        localStorage.setItem('shipper.Shipments',JSON.stringify(result)); 
                        var trHTML = '';
                        $.each(result, function (i, item) {                    
                            var properIndex = i + 1;              
                            trHTML += '<tr><td>' + properIndex + '.</td><td><a href=shipments.html?id=' + item.shipment.wayBillNumber + '>' + item.shipment.wayBillNumber + '</a></td><td>' + item.dashboardStatus.currentStatus + `</td><td><div class="progress progress-xs progress-striped active"><div class="progress-bar progress-bar-success" style="width: ` + item.dashboardStatus.label + `"></div></div></td><td><span class="badge bg-green">` + item.dashboardStatus.label + '</span></td></tr>';
                        });  
                        $('#dashboard_table').append(trHTML);                          
                    },
                    error: function () {
                        swal(
                            'Ooops',
                            'Something went terribly wrong! getUrlShipmentsWithStatus',
                            'error'
                        );
                    }               
                });
            }else{
                var result = JSON.parse(localStorage.getItem('shipper.Shipments'));
                var trHTML = '';
                $.each(result, function (i, item) {                    
                    var properIndex = i + 1;              
                    trHTML += '<tr><td>' + properIndex + '.</td><td><a href=shipments.html?id=' + item.shipment.wayBillNumber + '>' + item.shipment.wayBillNumber + '</a></td><td>' + item.dashboardStatus.currentStatus + `</td><td><div class="progress progress-xs progress-striped active"><div class="progress-bar progress-bar-success" style="width: ` + item.dashboardStatus.label + `"></div></div></td><td><span class="badge bg-green">` + item.dashboardStatus.label + '</span></td></tr>';
                });  
                $('#dashboard_table').append(trHTML);                      
            }

            $.ajax({
                type: "GET",
                dataType: "json",
                url: getUrlVehicles,
                success: function (result) {                
                    $.each(result, function (i, car) {
                        localStorage.setItem(car.id, JSON.stringify(car));
                    });                
                },
                error: function () {
                    swal(
                        'Ooops',
                        'Something went wrong! getUrlVehicles',
                        'error'
                    );
                }
            });

            //get consignee details
            $.ajax({
                type: "GET",
                dataType: "json",
                url: getUrlConsigneeDetails,
                success: function (result) {
                $('#company_header_name').text(result.name);     
                var address = result.address + " " + result.address2 + " " + result.country;
                if ( address === null || address === ""){
                    address = "Unit 11 City Deep Production Park 83 Heidelburg Road  City Deep 2049";                   
                }
                var gmapSource = "https://maps.google.com/maps?q=" + address + "&t=&z=15&ie=UTF8&iwloc=&output=embed";
                /*$('#company_address_1').append(result.address);
                $('#company_address_2').append(result.address2);
                $('#company_address_country').append(result.country);
                $('#company_address_phone').append(result.contactNumber);*/
                $('#gmap_canvas').attr("src",gmapSource);
                },
                error: function () {
                    swal(
                        'Ooops',
                        'Something went wrong getUrlConsigneeDetails !' + result.message,
                        'error'
                    );
                }
            });
        }
        
});
