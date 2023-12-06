
let tagController = {
    init: function () {
        tagController.getAll().done(data => { tagController.refreshTable(data); });
        //Sự kiện insert, edit
        $('.save-dialog').click(function (evnt) {
            var data = APIHelper.serializeObj($('#ModalCreate form'));
            if (data.ID != null && data.ID != '' && data.ID != '0') {
                tagController.edit(data).done(dt => {
                    if (dt.codeError != '00') {
                        alert(dt.strError);
                    }
                    else {
                        tagController.getAll().done(data => { tagController.refreshTable(data); });
                        $("#ModalCreate").modal('hide');
                    }
                });
            }
            else {
                tagController.insert(data).done(dt => {
                    if (dt.codeError != '00') {
                        alert(dt.strError);
                    }
                    else {
                        tagController.getAll().done(data => { tagController.refreshTable(data); });
                        $("#ModalCreate").modal('hide');
                    }
                });
            }
        });
    },
    getAll: function () {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/GetAll",
            type: 'GET',
        });
    },
    getByID: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/GetByID",
            data: { ID: id },
            contentType: 'application/json',
            timeout: 60000,
            type: 'GET',
        });
    },
    insert: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/Insert",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    edit: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/Edit",
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    active: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/Active?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    deActive: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/DeActive?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    delete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/Delete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    unDelete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/UnDelete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    //ShowHTML
    showDialogUpdate: function (dataKeyCurrent) {
        let $model = $('#ModalCreate');
        if (dataKeyCurrent == null || dataKeyCurrent == '') {
            $model.find('button.save-dialog').hide();
        }
        else {
            $model.find('button.save-dialog').show();
        }

        if (dataKeyCurrent != '') {
            tagController.getByID(dataKeyCurrent).done(data => {
                if (data != null && data.obj != null) {
                    $model.find('input[name="ID"]').val(data.obj.id);
                    $model.find('input[name="Name"]').val(data.obj.name);
                }
            }).catch(error => {
                $model.find('input[name="ID"]').val('0');
                $model.find('input[name="Name"]').val('');
            });
        }
        else {
            $model.find('input[name="ID"]').val("0");
            $model.find('input[name="Name"]').val('');
        }
        document.getElementById('ModalButton').setAttribute('data-target', '#ModalCreate');
        document.getElementById('ModalButton').click();
    },
    refreshTable: function (data) {
        $("#tbTable").empty();
        if (data != null && data.listObj != null && data.listObj.length > 0) {
            var bodyContent = "";
            var lock = "";
            for (let i = 0; i < data.listObj.length; i++) {
                lock = (data.listObj[i].isActive == 'N' ? 'class="btn btn-danger btn-xs active-dialog" title="Khóa" data-key="'
                    + data.listObj[i].id + '" data-isActive="' + data.listObj[i].isActive + '"><i class="fa fa-lock"></i>' :
                    'class="btn btn-dark btn-xs active-dialog"  title="Mở khóa" data-key="'
                    + data.listObj[i].id + '" data-isActive="' + data.listObj[i].isActive + '"><i class="fa fa-unlock"></i>');
                if (i % 0 == 0) {
                    bodyContent = '<tr class="even pointer" id="' + data.listObj[i].id + '">';
                }
                else {
                    bodyContent = '<tr class="odd pointer" id="' + data.listObj[i].id + '">';
                }

                bodyContent += '<td class="">' + (i + 1) + '</td>';
                bodyContent += '<td class="">' + data.listObj[i].name + '</td>';
                bodyContent += '<td class="">' + '<button ' + lock +  '</button></td>';
                bodyContent += '<td class="">' + '<button>' + (data.listObj[i].isDelete == 'Y' ? 'Đã xóa' : 'Đang sử dụng') + '</button></td>';
                bodyContent += '<td class="last">';
                bodyContent += '<button class="btn btn-info btn-xs view-dialog" title= "Xem"  data-key="' + data.listObj[i].id + '"><i class="fa fa-eye"></i></button>';
                bodyContent += '<button class="btn btn-warning btn-xs edit-dialog" title= "Sửa"  data-key="' + data.listObj[i].id + '"><i class="fa fa-pencil"></i></button>';
                bodyContent += '<button class="btn btn-dark btn-xs delete-dialog" title= "Xóa" data-key="' + data.listObj[i].id + '"><i class="fa fa-trash"></i></button>';
                bodyContent += '</td>';
                bodyContent += '</tr>';
                $("#tbTable").append(bodyContent);
            }
            tagController.refreshEvent();
        }

    },
    //Event
    evtUpdateDialog: function (evnt) {
        evnt.stopPropagation();
        let dataKey = $(this).attr('data-key');
        dataKey = dataKey == null ? '' : dataKey;
        tagController.showDialogUpdate(dataKey);
    },
    refreshEvent: function () {
        $('.view-dialog').unbind('click').click(tagController.evtUpdateDialog);
        $('.edit-dialog').unbind('click').click(tagController.evtUpdateDialog);
        $('.delete-dialog').unbind('click').click(function (evnt) {
            evnt.stopPropagation();
            let dataKey = $(this).attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            tagController.delete(dataKey).done(data => {
                if (data.codeError != '00') {
                    alert(data.strError);
                }
                else {
                    tagController.getAll().done(data => { tagController.refreshTable(data); });
                }
            });
        });
        $('.active-dialog').unbind('click').click(function (evnt) {
            evnt.stopPropagation();
            let dataKey = $(this).attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;

            let dataActive = $(this).attr('data-isActive');
            dataActive = dataActive == null ? '' : dataActive;

            if (dataActive == 'Y') {
                tagController.deActive(dataKey).done(data => {
                    if (data.codeError != '00') {
                        alert(data.strError);
                    }
                    else {
                        tagController.getAll().done(data => { tagController.refreshTable(data); });
                    }
                });
            }
            else {
                tagController.active(dataKey).done(data => {
                    if (data.codeError != '00') {
                        alert(data.strError);
                    }
                    else {
                        tagController.getAll().done(data => { tagController.refreshTable(data); });
                    }
                });
            }
        });
    },


};

tagController.init();