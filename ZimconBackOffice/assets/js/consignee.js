/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {

    $('#consigneeForm').bootstrapValidator({
        message: 'This value is not valid',
        //feedbackIcons: {
        //    valid: 'glyphicon glyphicon-ok',
        //    invalid: 'glyphicon glyphicon-remove',
        //    validating: 'glyphicon glyphicon-refresh'
        //},
        fields: {
            countrySelector: {
                validators: {
                    notEmpty: {
                        message: '* Please select consignee'
                    }
                }
            },
            name: {
                message: 'The name is not valid',
                validators: {
                    notEmpty: {
                        message: '* Name is required and cannot be empty'
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
            address2: {
                message: 'The address is not valid',
                validators: {
                    notEmpty: {
                        message: '* Address is required and cannot be empty'
                    }
                }
            },
            contactNumber: {
                validators: {
                    notEmpty: {
                        message: '* Contact number is required and cannot be empty'
                    },
                    stringLength: {
                        min: 10,
                        max: 10,
                        message: 'Contact number must be more than 10 digits long'
                    }
                }
            }
        }
    });

    $(".editConsinee").click(function (event) {
        $('#AjaxLoader').show();
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var name = $(this).closest("tr").find('td:eq(1)').text();
        var address = $(this).closest("tr").find('td:eq(2)').text();
        var address2 = $(this).closest("tr").find('td:eq(3)').text();
        var country = $(this).closest("tr").find('td:eq(4)').text();
        var contactNumber = $(this).closest("tr").find('td:eq(5)').text();

        $("#consigneeId").val(id);
        $("#name").val(name);
        $("#address").val(address);
        $("#address2").val(address2);
        $("#contactNumber").val(contactNumber);
        $("#countrySelector").val(country);
        $('#AjaxLoader').hide();
    });

    //$("#submitConsignee").click(function (event) {
    $('#consigneeForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            var postUrl = "../api/consignee";
            var postData = {
                'id': $("#consigneeId").val(),
                'name': $("#name").val(),
                'address': $("#address").val(),
                'address2': $("#address2").val(),
                'contactNumber': $("#contactNumber").val(),
                'country': $("#countrySelector").val()
            };

            submitConsignee(postUrl, postData);
        }
    });

    var submitConsignee = function (postUrl, postData) {
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
});
