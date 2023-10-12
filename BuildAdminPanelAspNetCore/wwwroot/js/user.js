$(document).ready(function () {
    ShowUSerData();
    $(".table-responsive").on("shown.bs.collapse", function () {
        $(this).css("overflow", "inherit");
    }).on("hidden.bs.collapse", function () {
        $(this).css("overflow", "auto");
    });
    $('#COMPANY_ID').change(function () {
        $('#EMPLOYEE_ID').attr('disabled', false);
        var comp_id = $(this).val();
        $('#EMPLOYEE_ID').empty();
        $('#EMPLOYEE_ID').append('<Option>--Select Employee--</Option>');
        $.ajax({
            url: '/security/User/GetEmployeeWithoutAccount?COMPANY_ID=' + comp_id,
            type: 'Post',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8;',
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#EMPLOYEE_ID').append('<Option value=' + data.EMPLOYEE_ID + '>' + data.EMPLOYEE_NAME + '</Option>');
                });
            }
        });
    });
})

function CompanyDropDownData() {
    debugger;
    $.ajax({
        url: '/security/Company/LoadData',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            console.log(result);
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

function ShowUSerData() {
    $.ajax({
        url: '/security/User/LoadData',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            console.log(result);
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.ROW_NO + '</td>';
                object += '<td>' + item.USER_NAME + '</td>';
                object += '<td>' + item.EMPLOYEE_ID + '</td>';
                object += '<td>' + item.COMPANY_NAME + '</td>';
                object += '<td>' + item.USER_PASSWORD + '</td>';
                object += '<td>' + item.EMAIL + '</td>';
                //object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.COMPANY_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + item.COMPANY_ID + ');">Delete</a></td>';
                object += '</tr>';
            });
            $('#user_data').html(object);
        },
        error: function () {
            toastr.error("Data can't get");
        }
    });
};
$('#btnAddUser').click(function () {
    /*    alert('ok');*/
    ClearTextBox();
    $('#UserModal').modal('show');
    $('#compId').hide();
    CompanyDropDownData();
    //$('#EmployeeId').hide();
    $('#AddUser').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#userHeading').text('Add User');
});

function AddUser() {
    debugger
    var object = {
        USER_ID: $('#USER_ID').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        EMPLOYEE_ID: $('#EMPLOYEE_ID').val(),
        USER_TYPE: $('#USER_TYPE').val(),
        EMAIL: $('#EMAIL').val(),

    }
    $.ajax({
        url: '/security/User/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data saved successfully');
            ClearTextBox();
            ShowUserData();
            HideModalPopUp();
        },
        error: function () {
            toastr.error("Data can't Saved!")
        }
    })
}

function HideModalPopUp() {
    $('#UserModal').modal('hide')
};

function ClearTextBox() {
    $('#COMPANY_ID').val('');
    $('#COMPANY_NAME').val('');
    $('#COMPANY_SHORT_NAME').val('');
    $('#COMPANY_ADDRESS1').val('');
    $('#COMPANY_ADDRESS2').val('');
};

function Delete(id) {
    if (confirm('Are you sure, You want to delete this record?')) {
        $.ajax({
            url: '/security/User/Delete?id=' + id,
            success: function () {
                toastr.success('Record deleted successfully!')
                ShowEmployeeData();
            },
            error: function () {
                toastr.error("Data can't be deleted");
            }
        })
    }
}

function Edit(id) {
    $.ajax({
        url: '/security/User/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $('#CompanyModal').modal('show');
            $('#compId').hide();
            $('#COMPANY_ID').val(response.companY_ID);
            $('#COMPANY_NAME').val(response.companY_NAME);
            $('#COMPANY_SHORT_NAME').val(response.companY_SHORT_NAME);
            $('#COMPANY_ADDRESS1').val(response.companY_ADDRESS1);
            $('#COMPANY_ADDRESS2').val(response.companY_ADDRESS2);
            $('#AddUser').css('display', 'none');
            $('#btnUpdate').css('display', 'block');
            $('#userHeading').text('Update User');
            //$('#AddEmployee').hide();
            //$('#btnUpdate').show();


        },
        error: function () {
            toastr.error("Data can't be deleted");
        }
    })
}

function UpdateUser() {
    debugger
    var object = {
        COMPANY_ID: $('#COMPANY_ID').val(),
        COMPANY_NAME: $('#COMPANY_NAME').val(),
        COMPANY_SHORT_NAME: $('#COMPANY_SHORT_NAME').val(),
        COMPANY_ADDRESS1: $('#COMPANY_ADDRESS1').val(),
        COMPANY_ADDRESS2: $('#COMPANY_ADDRESS2').val(),

    }

    $.ajax({
        url: '/security/User/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data updated successfully');
            ClearTextBox();
            ShowUserData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}