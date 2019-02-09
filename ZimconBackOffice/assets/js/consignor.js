/**
 * Created by pamela on 2019/01/28.
 */

$(document).ready(function () {
    //Consignee 
    $('#consignorForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            name: {
                message: 'The name is not valid',
                validators: {
                    notEmpty: {
                        message: '* Name is required and cannot be empty'
                    }
                }
            }
        }
    });
    $(".editConsinor").click(function (event) {
        //$("#fakeloader").fakeLoader();
        var id = $(this).closest("tr").find('td:eq(0)').text();
        var name = $(this).closest("tr").find('td:eq(1)').text();

        $("#consignorId").val(id);
        $("#name").val(name);
    });
    $('#consignorForm').validator().on('submit', function (e) {
        if (!e.isDefaultPrevented()) {
            var postUrl = "../api/consignor";
            var postData = {
                'id': $("#consignorId").val(),
                'name': $("#name").val(),
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
                        "Result",
                        "Consignor successfully saved",
                        "success"
                    );
                    location.reload();

                },
                error: function (error) {
                    swal(
                        "Ooops",
                        "Failed to add consignor",
                        "error"
                    );
                    location.reload();

                }
            });
        }
    });
});
