$(document).ready(function () {
    ShowModuleData();
    //$('#AddModule').click(function () {
    //    $('#moduleForm').submit();
    //});

        $('#moduleForm').submit(function (event) {
            event.preventDefault();

            //$.ajax({
            //    url: $(this).attr('action'),
            //    type: 'POST',
            //    data: $(this).serialize(),
            //    success: function (result) {
            //        // Handle the success response
            //        console.log(result);
            //    },
            //    error: function (jqXHR, textStatus, errorThrown) {
            //        // Handle the error response
            //        console.log(errorThrown);

            //        // Display the validation errors
            //        $('#moduleForm').html(jqXHR.responseText);
            //    }
            //});
            var object = {
                MODULE_ID: $('#MODULE_ID').val(),
                COMPANY_ID: $('#COMPANY_ID').val(),
                MODULE_NAME: $('#MODULE_NAME').val(),
                ORDER_BY_NO: $('#ORDER_BY_NO').val(),
                //EMAIL: $('#EMAIL').val(),

            }
            $.ajax({
                url: '/security/MenuCategory/AddOrUpdate',
                type: 'Post',
                data: object,
                //contentType:'application/json; charset=utf-8',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
                dataType: 'json',
                success: function (data) {
                    if (data.result === "1") {
                        toastr.success(data.message);
                        ClearTextBox();
                        ShowModuleData();
                        HideModalPopUp();
                    }
                    if (data.result === "2") {
                        toastr.error(data.message);
                        ClearTextBox();
                        //ShowModuleData();
                        //HideModalPopUp();
                    }
                    if (data.result === "") {
                        toastr.warning(data.message);
                    }

                },
                //error: function () {
                //    toastr.error("Data can't Saved!")
                //},
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle the error response
                    console.log(errorThrown);

                    // Display the validation errors
                    $('#moduleForm').html(jqXHR.responseText);
                    //toastr.warning(data.message);
                    
                }
                //error: function () {
                //    toastr.error("Data can't Saved!")
                //}
            })
        });
});



//$('#myButton').click(function () {
//    alert('Button clicked!');
//});

function CompanyDropDownData() {
    debugger;
    $.ajax({
        url: '/security/Company/LoadData',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            //console.log(result);
            /* $('#COMPANY_ID').empty();*/
            /*$('#COMPANY_ID').append('<Option>--Select Company--</Option>');*/
            $.each(result, function (i, data) {
                $('#COMPANY_ID').append('<Option value=' + data.COMPANY_ID + '>' + data.COMPANY_NAME + '</Option>');
            });
        },
        error: function () {
            toastr.error("Data can't get");
        }
    });


};

function ShowModuleData() {
    $.ajax({
        url: '/security/MenuCategory/LoadData',
        type: 'Post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            //console.log(result);
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.ROW_NO + '</td>';
                object += '<td>' + item.MODULE_NAME + '</td>';
                object += '<td>' + item.ORDER_BY_NO + '</td>';
                object += '<td>' + item.STATUS + '</td>';
                object += '<td>' + item.CREATENAME + '</td>';
                object += '<td>' + item.ENTERED_DATE + '</td>';
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.MODULE_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + item.MODULE_ID + ');">Delete</a></td>';
                object += '</tr>';
            });
            $('#module_data').html(object);
        },
        error: function () {
            toastr.error("Data can't get");
        }
    });
};
$('#btnAddModule').click(function () {
    /*    alert('ok');*/
    ClearTextBox();
    $('#COMPANY_ID').val('');
    //$('#COMPANY_ID').prop('selectedIndex', );
    $('#ModuleModal').modal('show');
    $('#modId').hide();
    //CompanyDropDownData();
    //$('#EmployeeId').hide();
    $('#AddModule').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#moduleHeading').text('Add Module');
});

function AddModule() {
    var object = {
        MODULE_ID: $('#MODULE_ID').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        MODULE_NAME: $('#MODULE_NAME').val(),
        ORDER_BY_NO: $('#ORDER_BY_NO').val(),
        //EMAIL: $('#EMAIL').val(),

    }
    $.ajax({
        url: '/security/MenuCategory/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data saved successfully');
            ClearTextBox();
            ShowModuleData();
            HideModalPopUp();
        },
        error: function () {
            toastr.error("Data can't Saved!")
        }
    })
}

function HideModalPopUp() {
    $('#ModuleModal').modal('hide')
    
};

function ClearTextBox() {
    //$('#COMPANY_ID').val('');
    $('#MODULE_NAME').val('');
    $('#MODULE_ID').val('');
    $('#ORDER_BY_NO').val(0);
};

function Delete(id) {
    if (confirm('Are you sure, You want to delete this record?')) {
        $.ajax({
            url: '/security/MenuCategory/Delete?id=' + id,
            success: function (data) {
                //toastr.success('Record deleted successfully!')
                toastr.success(data.message);
                ShowModuleData();
            },
            error: function () {
                toastr.error("Data can't be deleted");
            }
        })
    }
}

function Edit(id) {
    $.ajax({
        url: '/security/MenuCategory/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {

            // Open the modal with default content
            $('#ModuleModal').modal('show');

            //$('#ModuleModal').modal('show');
            $('#modId').hide();
            //CompanyDropDownData();
            //$('#COMPANY_ID').val();
            //$('#COMPANY_ID :selected').text();
            //$('#COMPANY_ID').empty();
            //$('#dropDownId :selected').text();
            $('#COMPANY_ID').append('<Option>--Select Company--</Option>');
            $('#COMPANY_ID').empty();
            $('#COMPANY_ID').append('<option value="">--Please Select A Company--</option>');

            $.each(response.companyList, function (i, data) {
                $('#COMPANY_ID').append('<Option value=' + data.value + '>' + data.text + '</Option>');
            });
            //ClearTextBox();
            //$('#COMPANY_ID').append('<Option value=' + response.companY_ID + '>' + response.companY_NAME + '</Option>');
            $('#COMPANY_ID').val(response.companY_ID);
            $('#MODULE_NAME').val(response.modulE_NAME);
            $('#MODULE_ID').val(response.modulE_ID);
            $('#ORDER_BY_NO').val(response.ordeR_BY_NO);
            $('#AddModule').css('display', 'none');
            $('#btnUpdate').css('display', 'block');
            $('#moduleHeading').text('Update Module');
            //$('#AddEmployee').hide();
            //$('#btnUpdate').show();
        },
        error: function () {
            toastr.error("Data can't be retrive");
        }
    })
}

function UpdateModule() {
    debugger
    var object = {
        COMPANY_ID: $('#COMPANY_ID').val(),
        COMPANY_NAME: $('#COMPANY_NAME').val(),
        MODULE_NAME: $('#MODULE_NAME').val(),
        MODULE_ID: $('#MODULE_ID').val(),
        ORDER_BY_NO: $('#ORDER_BY_NO').val(),

    }

    $.ajax({
        url: '/security/MenuCategory/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data updated successfully');
            //ClearTextBox();
            ShowModuleData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}