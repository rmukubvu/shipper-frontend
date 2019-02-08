/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {
    //Consignee 
    $('#consigneeForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
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
        //$("#fakeloader").fakeLoader();
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
         
    });
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
                    "Consignee successfully saved",
                    "success"
                );
                location.reload();
                 
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add consignee",
                    "error"
                );
                location.reload();
                 
            }
        });
    };
   

    //Consignee contact person 
    $('#consigneeContactForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            countryCode: {
                validators: {
                    notEmpty: {
                        message: '* Please select country code'
                    }
                }
            },
            contactPerson: {
                message: 'The name is not valid',
                validators: {
                    notEmpty: {
                        message: '* Name is required and cannot be empty'
                    }
                }
            },
            contactEmailAddress: {
                message: 'The email address is not valid',
                validators: {
                    notEmpty: {
                        message: '* Email address is required and cannot be empty'
                    }
                }
            },
            telephone: {
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

    //click add contact person link
    $('#addNewContact').click(function (event) {
        $('#contactName').text(localStorage.getItem('shipper.consigneeName')); 
        $('#addNewContact').text(localStorage.getItem('shipper.consigneeName')); 
        $('#contactsName').text(localStorage.consigneeName);
        $('#contactsName').text(localStorage.consigneeNamess);

        $('#addNewConsigneeContactForm').removeClass('hidden');
    });
    //click on view list of contact persons
    $(".viewContacts").click(function (event) {
        var consigneeId = $(this).closest("tr").find('td:eq(0)').text();
        var consigneeName = $(this).closest("tr").find('td:eq(1)').text();
        localStorage.setItem('shipper.consigneeName', consigneeName);
        localStorage.setItem('shipper.consigneeId', consigneeId);

        location.pathname = "/Consignee/" + consigneeId;
        $('#contactName').text(localStorage.getItem('shipper.consigneeName'));
    });

    function maskedPhonedNumber(countryCode, telephone) {
        var countryCodeLength = countryCode.length;
        return "0" + telephone.substring(countryCodeLength);
    }


    $(".editcontact").click(function (event) {

        var id = $(this).closest("tr").find('td:eq(0)').text();
        var consigneeId = $(this).closest("tr").find('td:eq(1)').text();
        var name = $(this).closest("tr").find('td:eq(2)').text();
        var emailAddress = $(this).closest("tr").find('td:eq(3)').text();
        var countryCode = $(this).closest("tr").find('td:eq(4)').text();
        var telephone = $(this).closest("tr").find('td:eq(5)').text();

        $("#contactId").val(id);
        $("#consigneeId").val(consigneeId);
        $("#contactPerson").val(name);
        $("#emailAddress").val(emailAddress);
        $("#countryCode").val(countryCode);
        $("#telephone").val(maskedPhonedNumber(countryCode,telephone));
        $("#addNewConsigneeContactForm").removeClass('hidden');
    });

    $('#consigneeContactForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            var consigneeId = localStorage.getItem('shipper.consigneeId');
            var postUrl = "../api/consigneeContact";

            var postData = {
                'id': $("#contactId").val(),
                'consigneeId': consigneeId,
                'name': $("#contactPerson").val(),
                'emailAddress': $("#emailAddress").val(),
                'countryCode': $("#countryCode").val(),
                'telephone': $("#telephone").val()
            };
            
            submitConsigneeContact(postUrl, postData);          
        }
    });
    var submitConsigneeContact = function (postUrl, postData) {
        var postJson = JSON.stringify(postData);
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: postUrl,
            data: postJson,
            success: function (result) {
                window.FakeLoader.hideOverlay();
                swal(
                    "Result",
                    "Consignee contact successfully saved",
                    "success"
                );
                location.reload();
                // 
            },
            error: function (error) {
                swal(
                    "Ooops",
                    "Failed to add consignee",
                    "error"
                );
                location.reload();
                // 
            }
        });
    };
});
