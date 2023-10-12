$(document).ready(function () {
    ShowCompanyData();
    //$('.select2').select2();
});

function ShowCompanyData() {
    $.ajax({
        url: '/security/Company/LoadData',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            var object = '';
            $.each(result, function (index, item) {
                console.log(item);
                object += '<tr>';
                object += '<td>' + item.COMPANY_ID + '</td>';
                object += '<td>' + item.COMPANY_NAME + '</td>';
                object += '<td>' + item.COMPANY_SHORT_NAME + '</td>';
                object += '<td>' + item.COMPANY_ADDRESS1 + '</td>';
                object += '<td>' + item.COMPANY_ADDRESS2 + '</td>';
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.COMPANY_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + item.COMPANY_ID + ');">Delete</a></td>';
                object += '</tr>';
            });
            $('#table_data').html(object);
        },
        error: function () {
            toastr.error("Data can't get");
        }
    });
};
$('#btnAddCompany').click(function () {
    /*    alert('ok');*/
    ClearTextBox();
    $('#CompanyModal').modal('show');
    $('#compId').hide();
    //$('#EmployeeId').hide();
    $('#AddCompany').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#companyHeading').text('Add Employee');
});

function AddCompany() {
    debugger
    var object = {
        COMPANY_NAME: $('#COMPANY_NAME').val(),
        COMPANY_SHORT_NAME: $('#COMPANY_SHORT_NAME').val(),
        COMPANY_ADDRESS1: $('#COMPANY_ADDRESS1').val(),
        COMPANY_ADDRESS2: $('#COMPANY_ADDRESS2').val(),

    }
    $.ajax({
        url: '/security/Company/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data saved successfully');
            ClearTextBox();
            ShowCompanyData();
            HideModalPopUp();
        },
        error: function () {
            toastr.error("Data can't Saved!")
        }
    })
}

function HideModalPopUp() {
    $('#CompanyModal').modal('hide')
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
            url: '/security/Company/Delete?id=' + id,
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
        url: '/security/Company/Edit?id=' + id,
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
            $('#AddCompany').css('display', 'none');
            $('#btnUpdate').css('display', 'block');
            $('#companyHeading').text('Update Company');
            //$('#AddEmployee').hide();
            //$('#btnUpdate').show();


        },
        error: function () {
            toastr.error("Data can't be deleted");
        }
    })
}

function UpdateCompany() {
    debugger
    var object = {
        COMPANY_ID: $('#COMPANY_ID').val(),
        COMPANY_NAME: $('#COMPANY_NAME').val(),
        COMPANY_SHORT_NAME: $('#COMPANY_SHORT_NAME').val(),
        COMPANY_ADDRESS1: $('#COMPANY_ADDRESS1').val(),
        COMPANY_ADDRESS2: $('#COMPANY_ADDRESS2').val(),

    }

    $.ajax({
        url: '/security/Company/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data updated successfully');
            ClearTextBox();
            ShowCompanyData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}
