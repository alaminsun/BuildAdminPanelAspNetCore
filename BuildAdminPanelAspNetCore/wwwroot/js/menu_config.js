$(document).ready(function () {
    ShowMenuConfigData();

});


var COMPANY_ID, MENU_NAME, AREA;


$('#CreateMenuConfig').click(function () {

    if (CheckValidation() == false) {
        return false;
    }
    else {
        CreateMenu();
    }
    //CreateMenu();
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
    MENU_NAME = $('#MENU_NAME').val();
    AREA = $('#AREA').val();

    if (COMPANY_ID =="") {
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

//function ParentMenuDownData() {
//    debugger;
//    $.ajax({
//        url: '/security/MenuMaster/LoadData?COMPANY_ID =' + companyId,
//        type: 'Post',
//        dataType: 'json',
//        contentType: 'application/json;charset=utf-8;',
//        success: function (result) {
//            //console.log(result);
//            $('#PARENT_MENU_ID').empty();
//            $('#PARENT_MENU_ID').append('<Option value="">--Select Parent--</Option>');
//            $.each(result, function (i, data) {
//                $('#PARENT_MENU_ID').append('<Option value=' + data.PARENT_MENU_ID + '>' + data.PARENT_MENU_ID + '</Option>');
//            });
//        },
//        error: function () {
//            toastr.error("Data can't get");
//        }
//    });

//};

$('#COMPANY_ID').change(function () {
    //$('#PARENT_MENU_ID').attr('disabled', false);
    var companyId = $(this).val();
    $('#PARENT_MENU_ID').empty();
    $('#PARENT_MENU_ID').append('<Option>--Select Parent--</Option>');
    $.ajax({
        url: '/security/MenuMaster/LoadData?COMPANY_ID =' + companyId,
        type: 'Post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            //console.log(result);
            $.each(result, function (i, data) {
                $('#PARENT_MENU_ID').append('<Option value=' + data.parenT_MENU_ID + '>' + data.parenT_MENU_ID + '</Option>');
            });
        }
    });
});
$('#COMPANY_ID').change(function () {
    //$('#PARENT_MENU_ID').attr('disabled', false);
    var companyId = $(this).val();
    $('#MODULE_ID').empty();
    $('#MODULE_ID').append('<Option>--Select Module--</Option>');
    $.ajax({
        url: '/security/MenuCategory/LoadData?COMPANY_ID =' + companyId,
        type: 'Post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            console.log(result);
            $.each(result, function (i, data) {
                $('#MODULE_ID').append('<Option value=' + data.MODULE_ID + '>' + data.MODULE_NAME + '</Option>');
            });
        }
    });
});



//function ShowModuleData() {
//    $.ajax({
//        url: '/security/MenuMaster/LoadData',
//        type: 'Post',
//        dataType: 'json',
//        contentType: 'application/json;charset=utf-8;',
//        success: function (result, status, xhr) {
//            console.log(result);
//            var object = '';
//            $.each(result, function (index, item) {
//                object += '<tr>';
//                object += '<td>' + item.ROW_NO + '</td>';
//                object += '<td>' + item.MENU_ID + '</td>';
//                object += '<td>' + item.COMPANY_ID + '</td>';
//                object += '<td>' + item.MENU_NAME + '</td>';
//                object += '<td>' + item.ORDER_BY_SLNO + '</td>';
//                object += '<td>' + item.STATUS + '</td>';
//                object += '<td>' + item.HREF + '</td>';
//                object += '<td>' + item.AREA + '</td>';
//                object += '<td>' + item.CONTROLLER + '</td>';
//                object += '<td>' + item.ACTION + '</td>';
//                object += '<td>' + item.PARENT_MENU_ID + '</td>';
//                object += '<td>' + item.MODULE_ID + '</td>';
//                object += '<td>' + item.MENU_SHOW + '</td>';
//                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.MENU_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + item.MENU_ID + ');">Delete</a></td>';
//                object += '</tr>';
//            });
//            $('#menu_config_data').html(object);
//            //$('#menuConfigList').DataTable().column(1).visible(true);
//            //$("#menuConfigList td:nth-child(1,2), #menuConfigList th:nth-child(1,2)").hide();
//            //$("#menuConfigList td:nth-child(1), #menuConfigList th:nth-child(1)").hide();
//            $('table tr').each(function () {
//                $(this).find('td:nth-child(2), td:nth-child(3), td:nth-child(7)').hide();
//            });
//            $('table tr').each(function () {
//                $(this).find('th:nth-child(2), th:nth-child(3), th:nth-child(7)').hide();
//            });
//        },
//        error: function () {
//            toastr.error("Data can't get");
//        }
//    });
//};

function ShowMenuConfigData() {
    $.ajax({
        url: '/security/MenuMaster/LoadData',
        type: 'Post',
        dataType: 'json',
        success: OnSuccess
    })
}

function OnSuccess(response) {
    console.log(response);
    $('#menuConfigList').DataTable({
        bProcessing: true,
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
                data: 'MENU_ID',
                render: function (data, type, row, meta) {
                    return row.MENU_ID
                }
            },
            //{
            //    data: 'COMPANY_ID',
            //    render: function (data, type, row, meta) {
            //        return row.COMPANY_ID
            //    }
            //},
            {
                data: 'MENU_NAME',
                render: function (data, type, row, meta) {
                    return row.MENU_NAME
                }
            },
            {
                data: 'ORDER_BY_SLNO',
                render: function (data, type, row, meta) {
                    return row.ORDER_BY_SLNO
                }
            },
            {
                data: 'STATUS',
                render: function (data, type, row, meta) {
                    return row.STATUS
                }
            },
            {
                data: 'HREF',
                render: function (data, type, row, meta) {
                    return row.HREF
                }
            },
            {
                data: 'AREA',
                render: function (data, type, row, meta) {
                    return row.AREA
                }
            },
            {
                data: 'CONTROLLER',
                render: function (data, type, row, meta) {
                    return row.CONTROLLER
                }
            },
            {
                data: 'ACTION',
                render: function (data, type, row, meta) {
                    return row.ACTION
                }
            },
            {
                data: 'PARENT_MENU_ID',
                render: function (data, type, row, meta) {
                    return row.PARENT_MENU_ID
                }
            },
            {
                data: 'MODULE_ID',
                render: function (data, type, row, meta) {
                    return row.MODULE_ID
                }
            },
            {
                data: 'MENU_SHOW',
                render: function (data, type, row, meta) {
                    return row.MENU_SHOW
                }
            },
            {
                targets: -1, // -1 targets the last column
                data: null,
                render: function (data, type, row, meta) {
                    //return '<a href="#" class="btn btn-primary" onclick="Edit(' + row.MENU_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + row.MENU_ID + ');">Delete</a>'

                    return "<div class='btn-group'><button type='button' class='btn btn-primary dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Action</button>"
                        + "<div class='dropdown-menu shadow animated--grow-in'><a class='btn btn-primary dropdown-item' onclick='Edit(" + row.MENU_ID + ")'><i class='fas fa-pencil-alt'></i>Edit</a>"
                        + "<a href='#' id='delete' onclick = DeleteData(" + row.MENU_ID + "); class='dropdown-item'><i class='fas fa-trash-alt'></i>Delete</a>"
                        + "</div>"
                        + "</div>"
                }
                //defaultContent: '<a href="#" class="btn btn-primary" onclick="Edit(' + row.MENU_ID + ')">Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + row.MENU_ID + ');">Delete</a>',
            }

        ]

    }).columns([1,5]).visible(false);;
}

//$('#menuConfigList').DataTable().columns([1, 2]).visible(false);

$('#btnCreateMenuConfig').click(function () {
    /*    alert('ok');*/
    ClearTextBox();
    //$('#COMPANY_ID').val('');
    //$('#COMPANY_ID').prop('selectedIndex', );
    //$('#COMPANY_ID').empty();
    $('#MenuConfigModal').modal('show');
    $('#menuId').hide();
    $('#COMPANY_ID').empty();
    //$('#ModuleModal').load(window.location.href + ' #ModuleModal .modal-content');
    CompanyDropDownData();
    //$('#EmployeeId').hide();
    $('#CreateMenuConfig').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#menuConfigHeading').text('Menu Configuration');
});

function CreateMenu() {
    debugger

    var object = {
        //{ COMPANY_ID: parseInt(model.COMPANY_ID), MENU_ID: model.MENU_ID, MENU_NAME: model.MENU_NAME, ORDER_BY_SLNO: parseInt(model.ORDER_BY_SLNO), PARENT_MENU_ID: parseInt(model.PARENT_MENU_ID), MODULE_ID: parseInt(model.MODULE_ID), CONTROLLER: model.CONTROLLER, ACTION: model.ACTION, AREA: model.AREA, HREF: model.AREA + "/" + model.CONTROLLER + "/" + model.ACTION, MENU_SHOW: model.MENU_SHOW },
        //MODULE_ID: $('#MODULE_ID').val(),
        MENU_ID: $('#MENU_ID').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        MENU_NAME: $('#MENU_NAME').val(),
        ORDER_BY_SLNO: $('#ORDER_BY_SLNO').val(),
        MODULE_ID: $('#MODULE_ID').val(),
        CONTROLLER: $('#CONTROLLER').val(),
        ACTION: $('#ACTION').val(),
        AREA: $('#AREA').val(),
        HREF: $('#AREA').val() + "/" + $('#CONTROLLER').val() + "/" + $('#ACTION').val(),
        MENU_SHOW: $('#MENU_SHOW').val(),
        PARENT_MENU_ID: $('#PARENT_MENU_ID').val(),
        //EMAIL: $('#EMAIL').val(),

    }
    $.ajax({
        url: '/security/MenuMaster/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            //console.log(data);
            toastr.success('Data saved successfully');
            ClearTextBox();
            ShowMenuConfigData();
            HideModalPopUp();
        },
        error: function () {
            toastr.error("Data can't Saved!")
        }
    })
}

function HideModalPopUp() {
    $('#MenuConfigModal').modal('hide')

};

function ClearTextBox() {
    //$('#COMPANY_ID').val('');
    //$('#MODULE_NAME').val('');
    //$('#MODULE_ID').val('');
    //$('#ORDER_BY_NO').val(0);
     $('#MENU_ID').val(0),
     $('#COMPANY_ID').val("0"),
     $('#MENU_NAME').val(''),
     $('#ORDER_BY_SLNO').val(0),
     $('#MODULE_ID').val("0"),
     $('#CONTROLLER').val(''),
     $('#ACTION').val(''),
     $('#AREA').val(''),
     $('#PARENT_MENU_ID').val("0")
     //$('#MENU_SHOW').val()
   //$scope.model.COMPANY_SEARCH_ID = 0;
    //$scope.model.COMPANY_ID = 0;

    //$scope.model.MENU_ID = 0;
    //$scope.model.MENU_NAME = "";
    //$scope.model.AREA = "";

    //$scope.model.ORDER_BY_SLNO = 0;
    //$scope.model.MODULE_ID = 0;
    //$scope.model.MODULE = '';
    //$scope.model.CONTROLLER = '';
    //$scope.model.ACTION = '';
    //$scope.model.HREF = '';
    //$scope.model.STATUS = '';
    //$scope.model.PARENT_MENU_ID = 0;
    //$scope.model.PARENT_MENU = '';
    //$scope.model.MENU_SHOW = 'Active';
};

function Delete(id) {
    if (confirm('Are you sure, You want to delete this record?')) {
        $.ajax({
            url: '/security/MenuCategory/Delete?id=' + id,
            success: function (data) {
                //toastr.success('Record deleted successfully!')
                toastr.success(data.message);
                ShowMenuConfigData();
            },
            error: function () {
                toastr.error("Data can't be deleted");
            }
        })
    }
}

function Edit(id) {
    $.ajax({
        url: '/security/MenuMaster/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            console.log(response.menuList);
            //$('#modId').hide();
            //$('#ModuleModal').load(window.location.href + ' #ModuleModal .modal-content');
            //console.log(response);
            // Open the modal with default content
            $('#MenuConfigModal').modal('show');

            //$('#ModuleModal').modal('show');
            $('#menuId').hide();
            //CompanyDropDownData();
            //$('#COMPANY_ID').val();
            //$('#COMPANY_ID :selected').text();
            $('#COMPANY_ID').empty();
            //$('#ModuleModal').modal('show');
            //$('#dropDownId :selected').text();
            $('#COMPANY_ID').append('<option value="0">----Select Company----</option>');
            $.each(response.companyList, function (i, data) {
                $('#COMPANY_ID').append('<Option value=' + data.companY_ID + '>' + data.companY_NAME + '</Option>');
            });
            $('#PARENT_MENU_ID').empty();
            $('#PARENT_MENU_ID').append('<option value="0">---Select Parent Menu- --</option>');
            $.each(response.menuList, function (i, data) {
                $('#PARENT_MENU_ID').append('<Option value=' + data.parenT_MENU_ID + '>' + data.parenT_MENU + '</Option>');
            });
            $('#MODULE_ID').empty();
            $('#MODULE_ID').append('<option value="0">---Select Menu Module---</option>');
            $.each(response.moduleList, function (i, data) {
                $('#MODULE_ID').append('<Option value=' + data.modulE_ID + '>' + data.modulE_NAME + '</Option>');
            });
            //$('#COMPANY_ID').empty();
            //ClearTextBox();
            //$('#COMPANY_ID').append('<Option value=' + response.companY_ID + '>' + response.companY_NAME + '</Option>');
            $('#COMPANY_ID').val(response.companY_ID);
            $('#MENU_ID').val(response.menU_ID);
            $('#MENU_NAME').val(response.menU_NAME);
            $('#PARENT_MENU_ID').val(response.parenT_MENU_ID);
            $('#MENU_NAME').val(response.menU_NAME);
            $('#MODULE_ID').val(response.modulE_ID);
            $('#ORDER_BY_SLNO').val(response.ordeR_BY_SLNO);
            $('#AREA').val(response.area);
            $('#CONTROLLER').val(response.controller);
            $('#ACTION').val(response.action);
            $('#CreateMenuConfig').css('display', 'none');
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

function UpdateMenuConfig() {
    debugger
    var object = {
        //COMPANY_ID: $('#COMPANY_ID').val(),
        //COMPANY_NAME: $('#COMPANY_NAME').val(),
        //MODULE_NAME: $('#MODULE_NAME').val(),
        //MODULE_ID: $('#MODULE_ID').val(),
        //ORDER_BY_NO: $('#ORDER_BY_NO').val(),
        MENU_ID: $('#MENU_ID').val(),
        COMPANY_ID: $('#COMPANY_ID').val(),
        MENU_NAME: $('#MENU_NAME').val(),
        ORDER_BY_SLNO: $('#ORDER_BY_SLNO').val(),
        MODULE_ID: $('#MODULE_ID').val(),
        CONTROLLER: $('#CONTROLLER').val(),
        ACTION: $('#ACTION').val(),
        AREA: $('#AREA').val(),
        HREF: $('#AREA').val() + "/" + $('#CONTROLLER').val() + "/" + $('#ACTION').val(),
        MENU_SHOW: $('#MENU_SHOW').val(),
        PARENT_MENU_ID: $('#PARENT_MENU_ID').val(),
    }

    $.ajax({
        url: '/security/MenuMaster/AddOrUpdate',
        type: 'Post',
        data: object,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8;',
        dataType: 'json',
        success: function () {
            toastr.success('Data updated successfully');
            ClearTextBox();
            ShowMenuConfigData();
            HideModalPopUp();
        },
        error: function () {

            toastr.error("Data can't update!")
        }
    })
}










