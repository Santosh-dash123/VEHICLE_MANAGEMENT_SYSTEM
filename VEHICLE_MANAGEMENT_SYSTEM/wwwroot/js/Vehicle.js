$(document).ready(function () {
    $.noConflict();
    $("#NAME").focus();

    $("#D_ID").change(function () {
        debugger;
        var Id = $("#D_ID").val();
        if (Id == null || Id == "") {
            Swal.fire({
                title: "Please select a district!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "OK",
                cancelButtonText: "Cancel",
            });
        }
        else {
            $.ajax({
                url: "/Home/GetBlock/" + Id,
                type: "GET",
                success: function (response) {
                    debugger;
                    $("#B_ID").empty().append('<option value="">---Select Block---</option>');
                    $.each(response, function (index, block) {
                        debugger;
                        $("#B_ID").append($(`<option value=${block.value}>${block.text}</option>`));
                    })
                },
                error: function (error) {

                }
            })
        }
    })

    $("#B_ID").change(function () {
        debugger;
        var Id = $("#B_ID").val();
        if (Id == null || Id == "") {
            Swal.fire({
                title: "Please select a block!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "OK",
                cancelButtonText: "Cancel",
            });
        }
        else {
            $.ajax({
                url: "/Home/GetTypeOfVehicle/" + Id,
                type: "GET",
                success: function (response) {
                    debugger;
                    $("#TOFVC").empty().append('<option value="">---Select Types Of Vehicle---</option>');
                    $.each(response, function (index, tofvc) {
                        debugger;
                        $("#TOFVC").append($(`<option value=${tofvc.value}>${tofvc.text}</option>`));
                    })
                },
                error: function (error) {

                }
            })
        }
    })

    $("#TOFVC").change(function () {
        debugger;
        var Id = $("#TOFVC").val();
        if (Id == null || Id == "") {
            Swal.fire({
                title: "Please select type of vehicle!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "OK",
                cancelButtonText: "Cancel",
            });
        }
        else {
            $.ajax({
                url: "/Home/GetVehicleName/" + Id,
                type: "GET",
                success: function (response) {
                    debugger;
                    $("#VCNAME").empty().append('<option value="">---Select Types Of Vehicle---</option>');
                    $.each(response, function (index, VCNAME) {
                        debugger;
                        $("#VCNAME").append($(`<option value=${VCNAME.value}>${VCNAME.text}</option>`));
                    })
                },
                error: function (error) {

                }
            })
        }
    })

    $("#VCNAME").change(function () {
        debugger;
        var Id = $("#VCNAME").val();
        if (Id == null || Id == "") {
            Swal.fire({
                title: "Please select a vehicle name!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "OK",
                cancelButtonText: "Cancel",
            });
        }
        else {
            $.ajax({
                url: "/Home/GetNoOfVehicle/" + Id,
                type: "GET",
                success: function (response) {
                    debugger;
                    console.log(response);
                    $("#NOOFVC").val(response);
                },
                error: function (error) {

                }
            })
        }
    })
})

function ValidationCallForInfo(message) {
    Swal.fire({
        title: message,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
    });
}

function chcekAllField() {
    debugger;
    if ($('#NAME').val() === "" || $("#NAME").val() == null) {
        $('#NAME').focus();
        //$('#NAME').after('<label class="error mt-2 text-danger">Please enter your name.</label>');
        ValidationCallForInfo('Name cannot be  blank!!');
        return false;
    }
    if (!$('#D_ID').val()) {
        debugger;
        $('#D_ID').focus();
        ValidationCallForInfo('District cannot be  blank!!');
        return false;
    }
    if (!$('#B_ID').val() || $('#B_ID').val() === "") {
        debugger;
        $('#B_ID').focus();
        ValidationCallForInfo('Block cannot be  blank!!');
        return false;
    }
    if (!$('#TOFVC').val() || $('#TOFVC').val() === "") {
        debugger;
        $('#TOFVC').focus();
        ValidationCallForInfo('Type of vehicle cannot be  blank!!');
        return false;
    }
    if (!$('#VCNAME').val() || $('#VCNAME').val() === "") {
        debugger;
        $('#VCNAME').focus();
        ValidationCallForInfo('Vehicle name cannot be  blank!!');
        return false;
    }
    if (!$('#NOOFVC').val() || $('#NOOFVC').val() === "") {
        debugger;
        $('#NOOFVC').focus();
        ValidationCallForInfo('No of vehicle cannot be  blank!!');
        return false;
    }
    debugger;
    if ($("#FILE_").val() != "" || $("#FILE_").val() != null) {
        debugger;
        var imagepath = $("#FILE_").prop('files')[0]; // Get the file selected
        // Validate file format
        if (imagepath) {
            var allowedFormats = ['image/jpeg', 'image/png', 'application/pdf'];
            if (!allowedFormats.includes(imagepath.type)) {
                $("#FILE_").focus();
                Swal.fire({
                    title: "Please select a file with only jpg,png or pdf format!",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonText: "OK",
                    cancelButtonText: "Cancel",
                });
                return false; // Prevent form submission if file format is not allowed
            }
        }
        // else {
        //     $("#FILE_").focus();
        //     Swal.fire({
        //         title: "Please select file!",
        //         icon: "warning",
        //         showCancelButton: true,
        //         confirmButtonText: "OK",
        //         cancelButtonText: "Cancel",
        //     });
        //     return false; // Prevent form submission if no file is selected
        // }
    }
    return true;
}

function saveData() {
    debugger;
    if ($("#FILE_").val() == "" || $("#FILE_").val() == null) {
        debugger;
        imagepath = null;
    }
    else {
        debugger;
        var imagepath = $("#FILE_").prop('files')[0]; // Get the file selected
    }


    var studentData = new FormData(); //This FormData is a built in javascript object that provides a way to easily
    //construct a set of key/value pairs representing form fields and their values, which can then be sent with an AJAX request.

    studentData.append('ID', $("#ID").val());
    studentData.append('NAME', $("#NAME").val());
    studentData.append('D_ID', $("#D_ID").val());
    studentData.append('B_ID', $("#B_ID").val());
    studentData.append('TOFVC', $("#TOFVC").val());
    studentData.append('VCNAME', $("#VCNAME").val());
    studentData.append('NOOFVC', $("#NOOFVC").val());
    studentData.append('allinputpath', imagepath);

    if (chcekAllField() == true) {
        debugger;
        $.ajax({
            url: "/Home/Vehicle",
            type: "POST",
            data: studentData,
            success: function (response) {
                console.log(response.result);
                Swal.fire({
                    title: response.result,
                    icon: "success",
                    confirmButtonText: "Ok",
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                })
            },
            error: function (error) {
                alert("Some error is present please check console!");
            }
        });
    }
}
