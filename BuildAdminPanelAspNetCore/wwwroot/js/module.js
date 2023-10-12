$(document).ready(function () {
    ShowModuleData();

});


var COMPANY_ID, MODULE_NAME;


$('#AddModule').click(function () {

    if (CheckValidation() == false) {
        return false;
    }
    else {
        AddModule();
    }
})

//$('#firstName, #lastName, #mobileNumber',).on('change', function () {
//    CheckValidation();
//})

$('#moduleForm').on('change', function () {
    CheckValidation();
})

function CheckValidation() {
    var isValid = true;
    COMPANY_ID = $('#COMPANY_ID').val();
    MODULE_NAME = $('#MODULE_NAME').val();
    //mobileNumber = $('#mobileNumber').val();

    if (COMPANY_ID == "") {
        $('#errorCOMPANY_ID').text('company name can not be blank.');
        $('#COMPANY_ID').css('border-color', 'red');
        $('#COMPANY_ID').focus();
        isValid = false
    }
    //else if (firstName.length < 3) {
    //    $('#errorFirstName').text('First name must be greater than or equal to 3 characters');
    //}
    else {
        $('#errorCOMPANY_ID').text('');
        $('#COMPANY_ID').css('border-color', 'lightgray');
    }
    if (MODULE_NAME == "") {
        $('#errorMODULE_NAME').text('module name can not be blank.');
        $('#MODULE_NAME').css('border-color', 'red');
        $('#MODULE_NAME').focus();
        isValid = false
    }
    else if (MODULE_NAME.length < 3) {
        $('#errorMODULE_NAME').text('module name must be greater than or equal to 3 characters');
    }
    else {
        $('#errorMODULE_NAME').text('');
        $('#MODULE_NAME').css('border-color', 'lightgray');
    }
    //if (mobileNumber == "") {
    //    $('#errorMobileNumber').text('Please enter mobile number.');
    //    $('#mobileNumber').css('border-color', 'red');
    //    $('#mobileNumber').focus();
    //    isValid = false
    //}
    //else if (mobileNumber.length < 11) {
    //    $('#errorMobileNumber').text('Mobile number should be 11 digits');
    //}
    //else {
    //    $('#errorMobileNumber').text('');
    //    $('#mobileNumber').css('border-color', 'lightgray');
    //}
    return isValid;
}

//for First Name
$('#firstName').keyup(function (e) {
    var result = BlockSpecialCharacters(e)

    if (result == false) {
        $('#errorFirstName').text('Special character not allowed!');
        $('#firstName').val('');
    }
    else {
        $('#errorFirstName').text('');
    }
})

//for Last Name
$('#lastName').keyup(function (e) {
    var result = BlockSpecialCharacters(e)

    if (result == false) {
        $('#errorLastName').text('Special character not allowed!');
        $('#lastName').val('');
    }
    else {
        $('#errorLastName').text('');
    }
})

//for Last Name
$('#mobileNumber').keyup(function (e) {
    $('#mobileNumber').attr('maxlength', '11');
    if (!OnlyAllowedNumeric(e)) {
        $('#errorMobileNumber').text('Only number allowed!');
        $('#mobileNumber').val('');
    }
    else {
        $('#errorMobileNumber').text('');
    }
})

function BlockSpecialCharacters(e) {
    var key = e.key;
    var keyCharCode = key.charCodeAt(0);
    //0-9
    if (keyCharCode >= 48 && keyCharCode <= 57) {
        return key;
    }
    //A-Z
    if (keyCharCode >= 65 && keyCharCode <= 90) {
        return key;
    }
    //a-z
    if (keyCharCode >= 97 && keyCharCode <= 122) {
        return key;
    }
    return false;
}
function OnlyAllowedNumeric(e) {
    var key = e.key;
    var keyCharCode = key.charCodeAt(0);
    //0-9
    if (keyCharCode >= 48 && keyCharCode <= 57) {
        return key;
    }
    return false;
}



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
             $('#COMPANY_ID').empty();
            $('#COMPANY_ID').append('<Option value="">--Select Company--</Option>');
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
    //$('#COMPANY_ID').val('');
    //$('#COMPANY_ID').prop('selectedIndex', );
    //$('#COMPANY_ID').empty();
    $('#ModuleModal').modal('show');
    $('#modId').hide();
    $('#COMPANY_ID').empty();
    //$('#ModuleModal').load(window.location.href + ' #ModuleModal .modal-content');
    CompanyDropDownData();
    //$('#EmployeeId').hide();
    $('#AddModule').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#moduleHeading').text('Add Module');
});

function AddModule() {
    debugger
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
            //console.log(data);
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
            //$('#modId').hide();
            //$('#ModuleModal').load(window.location.href + ' #ModuleModal .modal-content');
            //console.log(response);
            // Open the modal with default content
            $('#ModuleModal').modal('show');

            //$('#ModuleModal').modal('show');
            $('#modId').show();
            //CompanyDropDownData();
            //$('#COMPANY_ID').val();
            //$('#COMPANY_ID :selected').text();
            $('#COMPANY_ID').empty();
            $('#ModuleModal').modal('show');
            //$('#dropDownId :selected').text();
            //$('#COMPANY_ID').append('<option value="">--Please Select A Company--</option>');
            $.each(response.companyList, function (i, data) {
                $('#COMPANY_ID').append('<Option value=' + data.companY_ID + '>' + data.companY_NAME + '</Option>');
            });
            //$('#COMPANY_ID').empty();
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
            ClearTextBox();
            ShowModuleData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}