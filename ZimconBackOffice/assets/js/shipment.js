/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {

    $('#shipmentForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            consigneeSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select smart device'
                    }
                }
            },
            vehicleSelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select vehicle'
                    }
                }
            },
            address: {
                message: 'The address is not valid',
                validators: {
                    notEmpty: {
                        message: '* Address is required and cannot be empty'
                    }
                }
            },
            search_location: {
                message: 'The address is not valid',
                validators: {
                    notEmpty: {
                        message: '* Address is required and cannot be empty'
                    }
                }
            },
        }
    });

    //edit waybill
    $('#sizing-addon1').click(function (event) {
        $('#AjaxLoader').show();

        var waybillNumber = $('#searchShipment').val();

        if (waybillNumber !== null && waybillNumber !== "") {

            var getUrl = "api/getShipment?waybill=" + waybillNumber;

            $.ajax({
                url: getUrl,
                type: "GET",
                success: function (result) {
                    //close                
                    if (result.error === true) {
                        swal(
                            'Ooops',
                            result.loginErrorMessage,
                            'error'
                        );
                        $('#AjaxLoader').hide();
                    } else {
                        $('#shipmentId').val(result.id);
                        $('#wayBillNumber').val(result.wayBillNumber);
                        $('#manifestReference').val(result.manifestReference);
                        $('#loadedDate').val(result.loadedDate);
                        $('#consigneeSelector').val(result.consigneeId);
                        $('#vehicleSelector').val(result.vehicleId);
                        $('#address').val(result.sourceLatitude);
                        $('#search_location').val(result.destinationLatitude);
                    }
                    $('#AjaxLoader').hide();
                },
                error: function (xhr, status, error) {
                    swal(
                        'Ooops',
                        'Something went terribly wrong!',
                        'error'
                    );
                    $('#AjaxLoader').hide();
                }
            });
        }
    });

    //add shipment
    $('#shipmentForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            var postUrl = "../api/shipment";
            var postData = {
                'id': $("#shipmentId").val(),
                'consigneeId': $("#consigneeSelector").val(),
                'destinationLatitude': $("#search_location").val(),
                'destinationLongitude': $("#search_location").val(),
                'loadedDate': $("#loadedDate").val(),
                'manifestReference': $("#manifestReference").val(),
                'sourceLatitude': $("#address").val(),
                'vehicleId': $("#vehicleSelector").val(),
                'wayBillNumber': $("#wayBillNumber").val()
            };

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
                        'Result',
                        'Shipment successfully saved',
                        'success'
                    );
                    location.reload();
                    $('#AjaxLoader').hide();
                },
                error: function (error) {
                    swal(
                        'Ooops',
                        'Failed to allocate device',
                        'error'
                    );
                    location.reload();
                    $('#AjaxLoader').hide();
                }
            });
        }
    });
});
