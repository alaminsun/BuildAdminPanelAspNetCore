var linkElement = document.createElement('link');
linkElement.rel = 'stylesheet';
linkElement.type = 'text/css';
linkElement.href = 'BuildAdminPanelAspNetCore/wwwroot/css/customcss/checkboxbutton.css';
document.head.appendChild(linkElement);

var dataTable;

$(document).ready(function () {
    loadDataTable();
});

debugger;
function loadDataTable() {
    //dataTable = new DataTable('#tblData');
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/SA/Form/GetForm"
        },
        "columns": [
 /*           { "data": "formID", "width": "15%" },*/
            { "data": "formName", "width": "15%" },
            { "data": "formURL", "width": "15%" },
            //{ "data": "isActive", "width": "15%" },
            {
                data: 'isActive',
                render: function (data, type, row) {
                    if (row.isActive === true) {
                        //return '<input type="checkbox" class="checkbox" value="' + data + '" checked>';
                        return '<div class="cat action"><label><input type="checkbox" class="checkbox" value="' + data + '" checked><span>Select</span></label> </div>'
                    } else {
                        //return '<input type="checkbox" class="checkbox" value="' + data + '">';
                        return '<div class="cat action"><label><input type="checkbox" class="checkbox" value="' + data + '"><span>Select</span></label> </div>'
                    }
                },
                "width": "15%"
            },
            //{ "data": "phoneNumber", "width": "15%" },
            {
                "data": "formID",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Company/Upsert?id=${data}"
                           class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a onClick=Delete('/Admin/Company/Delete/${data}')
                           class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                            `
                },
                "width": "5%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}