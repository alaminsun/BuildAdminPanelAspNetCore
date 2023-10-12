$(document).ready(function () {
    ShowRoleData();

});


var COMPANY_ID, ROLE_NAME, AREA;


$('#btnSave').click(function () {

    //if (CheckValidation() == false) {
    //    return false;
    //}
    //else {
    //    CreateMenu();
    //}
    CreateRole();
})

//$('#firstName, #lastName, #mobileNumber',).on('change', function () {
//    CheckValidation();
//})

$('#roleForm').on('change', function () {
    CheckValidation();
})

function CheckValidation() {
    var isValid = true;
    COMPANY_ID = $('#COMPANY_ID').val();
    MENU_NAME = $('#MENU_NAME').val();
    AREA = $('#AREA').val();

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
    if (MENU_NAME == "") {
        $('#errorMENU_NAME').text('menu name can not be blank.');
        $('#MENU_NAME').css('border-color', 'red');
        $('#MENU_NAME').focus();
        isValid = false
    }
    else if (MENU_NAME.length < 3) {
        $('#errorMENU_NAME').text('menu name must be greater than or equal to 3 characters');
    }
    else {
        $('#errorMENU_NAME').text('');
        $('#MENU_NAME').css('border-color', 'lightgray');
    }
    if (AREA == "") {
        $('#errorAREA').text('menu name can not be blank.');
        $('#AREA').css('border-color', 'red');
        $('#AREA').focus();
        isValid = false
    }
    else if (AREA.length < 3) {
        $('#errorAREA').text('menu name must be greater than or equal to 3 characters');
    }
    else {
        $('#errorAREA').text('');
        $('#AREA').css('border-color', 'lightgray');
    }
    return isValid;
}

//for First Name
$('#MENU_NAME').keyup(function (e) {
    var result = BlockSpecialCharacters(e)
    var menuName = $('#MENU_NAME').val();
    if (result == false) {
        $('#errorMENU_NAME').text('Special character not allowed!');
        $('#MENU_NAME').val('');
    }
    else {
        $('#errorMENU_NAME').text('');
        $('#MENU_NAME').css('border-color', 'lightgray');
    }
    if (menuName == "") {
        $('#errorMENU_NAME').text('menu name can not be blank.');
        $('#MENU_NAME').css('border-color', 'red');
        $('#MENU_NAME').focus();
        isValid = false
    }
    else if (menuName.length < 3) {
        $('#errorMENU_NAME').text('menu name must be greater than or equal to 3 characters');
        $('#MENU_NAME').css('border-color', 'red');
    }
    else {
        $('#errorMENU_NAME').text('');
        $('#MENU_NAME').css('border-color', 'lightgray');
    }
})

//for Last Name
$('#AREA').keyup(function (e) {
    var result = BlockSpecialCharacters(e)
    var area = $('#AREA').val();
    if (result == false) {
        $('#errorAREA').text('Special character not allowed!');
        $('#AREA').val('');
    }
    else {
        $('#errorAREA').text('');
        $('#AREA').css('border-color', 'lightgray');
    }
    if (area == "") {
        $('#errorAREA').text('area name can not be blank.');
        $('#AREA').css('border-color', 'red');
        $('#AREA').focus();
        isValid = false
    }
    else if (area.length < 3) {
        $('#errorAREA').text('area name must be greater than or equal to 3 characters');
        $('#AREA').css('border-color', 'red');
    }
    else {
        $('#errorAREA').text('');
        $('#AREA').css('border-color', 'lightgray');
    }
})
//for Last Name
// Detect the change event
$("#COMPANY_ID").change(function () {
    // Get the selected value
    var selectedValue = $(this).val();
    alert(selectedValue);
    if (selectedValue == "") {
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
    // Use the selected value
    console.log(selectedValue);
});

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


function ShowRoleData() {
    $.ajax({
        url: '/security/Role/LoadData',
        type: 'Post',
        dataType: 'json',
        success: OnSuccess
    })
}

function OnSuccess(response) {
    console.log(response);
    $('#roleList').DataTable({
        bProcessing: false,
        lengthChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        bfilter: true,
        bSort: true,
        destroy: true,
        bPaginate: true,
        data: response,
        responsive: true,
        columns: [
            //{ 'visible': false, 'targets': [1,2,6] },
            {
                data: 'ROW_NO',
                render: function (data, type, row, meta) {
                    return row.ROW_NO
                }
            },
            {
                data: 'ROLE_ID',
                render: function (data, type, row, meta) {
                    return row.ROLE_ID
                }
            },
            {
                data: 'ROLE_NAME',
                render: function (data, type, row, meta) {
                    return row.ROLE_NAME
                }
            },
            //{
            //    data: 'UNIT_NAME',
            //    render: function (data, type, row, meta) {
            //        return row.UNIT_NAME
            //    }
            //},
            {
                data: 'STATUS',
                render: function (data, type, row, meta) {
                    return row.STATUS
                }
            },
            {
                data: 'ENTERED_DATE',
                render: function (data, type, row, meta) {
                    return row.ENTERED_DATE
                    //var date = new Date(data);
                    //return date.toLocaleDateString();
                }
            },
            {
                targets: -1, // -1 targets the last column
                data: null,
                render: function (data, type, row, meta) {
                    //return '<a href="#" class="btn btn-primary" onclick="Edit(' + row.MENU_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + row.MENU_ID + ');">Delete</a>'

                    return "<div class='btn-group'><button type='button' class='btn btn-primary dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Action</button>"
                        + "<div class='dropdown-menu shadow animated--grow-in'><a class='btn btn-primary dropdown-item' onclick='Edit(" + row.ROLE_ID + ")'><i class='fas fa-pencil-alt'></i>Edit</a>"
                        + "<a href='#' id='delete' onclick = DeleteData(" + row.ROLE_ID + "); class='dropdown-item'><i class='fas fa-trash-alt'></i>Delete</a>"
                        //+ "<a href='#' id='delete' onclick = ActivateRole(" + row.ROLE_ID + "); class='dropdown-item'><i class='fas fa-toggle-on'></i>Activate</a>"
                        + "</div>"
                        + "</div>"
                }
                //defaultContent: '<a href="#" class="btn btn-primary" onclick="Edit(' + row.MENU_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + row.MENU_ID + ');">Delete</a>',
            }

        ]

    }).columns([1]).visible(false);;
}

//$('#menuConfigList').DataTable().columns([1, 2]).visible(false);

$('#btnCreateRole').click(function () {
    /*    alert('ok');*/
    ClearTextBox();
    //$('#COMPANY_ID').val('');
    //$('#COMPANY_ID').prop('selectedIndex', );
    //$('#COMPANY_ID').empty();
    $('#RoleModal').modal('show');
    $('#roleId').hide();
    $('#COMPANY_ID').empty();
    //$('#ModuleModal').load(window.location.href + ' #ModuleModal .modal-content');
    CompanyDropDownData();
    //$('#EmployeeId').hide();
    $('#btnSave').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#roleHeading').text('Create Role');
});

function CreateRole() {
    debugger

    var object = {
        ROLE_ID: $('#ROLE_ID').val(),
        ROLE_NAME: $('#ROLE_NAME').val(),
        STATUS: $('#STATUS').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        UNIT_ID: $('#UNIT_ID').val()

    }
    $.ajax({
        url: '/security/Role/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            //console.log(data);
            toastr.success('Data saved successfully');
            ClearTextBox();
            ShowRoleData();
            HideModalPopUp();
        },
        error: function () {
            toastr.error("Data can't Saved!")
        }
    })
}

function HideModalPopUp() {
    $('#RoleModal').modal('hide')

};

function ClearTextBox() {
    $('#MENU_ID').val(0),
        $('#COMPANY_ID').val(''),
        $('#MENU_NAME').val(''),
        $('#ORDER_BY_SLNO').val(0),
        $('#MODULE_ID').val(''),
        $('#CONTROLLER').val(''),
        $('#ACTION').val(''),
        $('#AREA').val(''),
        $('#PARENT_MENU_ID').val('')
};

function Delete(id) {
    if (confirm('Are you sure, You want to delete this record?')) {
        $.ajax({
            url: '/security/MenuCategory/Delete?id=' + id,
            success: function (data) {
                //toastr.success('Record deleted successfully!')
                toastr.success(data.message);
                ShowRoleData();
            },
            error: function () {
                toastr.error("Data can't be deleted");
            }
        })
    }
}

function Edit(id) {
    $.ajax({
        url: '/security/Role/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $('#RoleModal').modal('show');
            $('#roleId').hide();
            $('#COMPANY_ID').empty();
            $('#COMPANY_ID').append('<option value="">----Select Company----</option>');
            $.each(response.companyList, function (i, data) {
                $('#COMPANY_ID').append('<Option value=' + data.companY_ID + '>' + data.companY_NAME + '</Option>');
            });
            $('#COMPANY_ID').val(response.companY_ID);
            $('#UNIT_ID').val(response.unitT_ID);
            $('#ROLE_NAME').val(response.rolE_NAME);
            $('#ROLE_ID').val(response.rolE_ID);
            $('#STATUS').val(response.status);
            //$('#MODULE_ID').val(response.modulE_ID);

            $('#btnSave').css('display', 'none');
            $('#btnUpdate').css('display', 'block');
            $('#roleHeading').text('Update Role');
            //$('#AddEmployee').hide();
            //$('#btnUpdate').show();
        },
        error: function () {
            toastr.error("Data can't be retrive");
        }
    })
}

function UpdateRole() {
    debugger
    var object = {
        ROLE_ID: $('#ROLE_ID').val(),
        ROLE_NAME: $('#ROLE_NAME').val(),
        STATUS: $('#STATUS').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        UNIT_ID: $('#UNIT_ID').val()

    }

    $.ajax({
        url: '/security/Role/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data updated successfully');
            ClearTextBox();
            ShowRoleData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}