let menuCategory = {
    classTreeView: 'tree-view',
    idContextMenuTreeView: 'contextMenuTreeView',
    refreshEvent: function () {
        //Sự kiện collapse_expand treeview
        $('.tree-view li').unbind('click').click(menuCategory.isCollapseExpand);

        $('.section-treeview .tree-view ul li').unbind('dblclick').dblclick(function (evnt) {
            evnt.stopPropagation();
            $('.section-treeview .tree-view ul li').removeClass('active');
            $(this).addClass('active');
            var dataKey = this.getAttribute("data-key");
            if (dataKey != null && dataKey != '') {
                categoryRepository.getAllByIDParent(dataKey).done(data => { categoryController.refreshTable(data); });
            }
        });
    },
    refreshTreeView: function () {
        let $treeView = $('.' + menuCategory.classTreeView + '>ul');
        $treeView.empty();
        let indexFolder = 0;
        let temFolder = {};

        categoryRepository.getAll().done((data) => {
            if (data != null && data.listObj != null) {
                let categories = data.listObj;
                for (indexFolder = 0; indexFolder < categories.length; indexFolder++) {
                    if (categories[indexFolder].idParent == 0) {
                        temFolder = categories[indexFolder];
                        let $liTreeView = $('<li></li>');
                        $liTreeView.attr('data-key', temFolder.id);
                        $liTreeView.html(temFolder.name);
                        $treeView.append($liTreeView);                        
                        menuCategory.addChildTreeView(categories, temFolder, $liTreeView);
                    }
                }
                menuCategory.refreshEvent();
            }
        });
    },
    addChildTreeView: function (data, parent, $liParent) {
        let $ulChild = $("<ul></ul>");
        let isChild = false;
        let temChildFolder = {};

        for (let indexChildFolder = 0; indexChildFolder < data.length; indexChildFolder++) {
            if (data[indexChildFolder].idParent == parent.id) {
                isChild = true;
                temChildFolder = data[indexChildFolder];
                let $liChild = $('<li></li>');
                $liChild.attr('data-key', temChildFolder.id);
                $liChild.html(temChildFolder.name);
                $ulChild.append($liChild);                
                menuCategory.addChildTreeView(data, temChildFolder, $liChild);
            }
        }
        if (isChild) {
            if (parent.levelChild >= 2)
                $liParent.addClass("expand-node");//collapse
            else
                $liParent.addClass("expand-node");

            $liParent.append($ulChild);
        }
    },
    isCollapseExpand: function (evnt) {
        evnt.stopPropagation();
        let vclass = this.getAttribute('class') + '';
        if (vclass.indexOf('expand-node') >= 0)
            $(this).removeClass("expand-node").addClass("collapse-node");
        else if (vclass.indexOf('collapse-node') >= 0)
            $(this).removeClass("collapse-node").addClass("expand-node");
    },
    getKeyActiveTreeView: function () {
        let $obj = $('.section-treeview .tree-view ul li.active');
        let itemValue = "";
        let itemKey = $obj.attr('data-key');

        if ($obj[0] != null)
            itemValue = $obj[0].outerText.split('\n')[0];

        if (itemKey == null || itemKey == '')
            itemKey = '0';

        return {
            key: itemKey,
            value: itemValue
        };
    },
    init: function () {
        menuCategory.refreshTreeView();
    }
};
let categoryController = {
    init: function () {
        $('.edit-dialog').click(categoryController.evtUpdateDialog);
        $('.save-dialog').click(categoryController.evtSaveDialog);
    },
    showDialogUpdate: function (dataKeyCurrent) {
        var dataParent = menuCategory.getKeyActiveTreeView();
        var item = item = { id: 0, idParent: dataParent.key, name: '' };
        categoryController.refreshDetail(item);
        if (dataKeyCurrent != '0') {
            categoryRepository.getByID(dataKeyCurrent).done(data => {
                if (data != null && data.obj != null)
                    item = data.obj;
                categoryController.refreshDetail(item);
            });
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
                bodyContent += '<td class="">' + '<button ' + lock + '</button></td>';
                bodyContent += '<td class="">' + '<button>' + (data.listObj[i].isDelete == 'Y' ? 'Đã xóa' : 'Đang sử dụng') + '</button></td>';
                bodyContent += '<td class="last">';
                bodyContent += '<button class="btn btn-info btn-xs view-dialog" title= "Xem"  data-key="' + data.listObj[i].id + '"><i class="fa fa-eye"></i></button>';
                bodyContent += '<button class="btn btn-warning btn-xs edit-dialog" title= "Sửa"  data-key="' + data.listObj[i].id + '"><i class="fa fa-pencil"></i></button>';
                bodyContent += '<button class="btn btn-dark btn-xs delete-dialog" title= "Xóa" data-key="' + data.listObj[i].id + '"><i class="fa fa-trash"></i></button>';
                bodyContent += '</td>';
                bodyContent += '</tr>';
                $("#tbTable").append(bodyContent);
            }
            categoryController.refreshEvent();
        }

    },
    refreshDetail: function (item) {
        let $model = $('#ModalCreate');
        $model.find('input[name="ID"]').val(item.id);
        $model.find('input[name="IDParent"]').val(item.idParent);
        $model.find('input[name="Name"]').val(item.name);
    },
    evtUpdateDialog: function (evnt) {
        evnt.stopPropagation();
        let dataKey = $(this).attr('data-key');
        dataKey = dataKey == null ? '0' : dataKey;
        categoryController.showDialogUpdate(dataKey);
    },
    evtSaveDialog: function (event) {
        var data = APIHelper.serializeObj($('#ModalCreate form'));
        if (data.ID != null && data.ID != '' && data.ID != '0') {
            categoryRepository.edit(data).done(dt => {
                if (dt.codeError != '00') {
                    alert(dt.strError);
                    return;
                }
                menuCategory.refreshTreeView();
                if (data.idParent != 0) {
                    categoryRepository.getAllByIDParent(data.idParent).done(data => { categoryController.refreshTable(data); });
                }
                $("#ModalCreate").modal('hide');
            });
        }
        else {
            categoryRepository.insert(data).done(dt => {
                if (dt.codeError != '00') {
                    alert(dt.strError);
                }
                else {
                    menuCategory.refreshTreeView();
                    if (data.idParent != 0) {
                        categoryRepository.getAllByIDParent(data.idParent).done(data => { categoryController.refreshTable(data); });
                    }
                    $("#ModalCreate").modal('hide');
                }
            });
        }
    },
    refreshEvent: function () {
        $('.view-dialog').unbind('click').click(categoryController.evtUpdateDialog);
        $('.edit-dialog').unbind('click').click(categoryController.evtUpdateDialog);
        $('.delete-dialog').unbind('click').click(function (evnt) {
            evnt.stopPropagation();
            let dataKey = $(this).attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            categoryRepository.delete(dataKey).done(data => {
                if (data.codeError != '00') {
                    alert(data.strError);
                }
                else {
                    dataKey = menuCategory.getKeyActiveTreeView();
                    categoryRepository.getAllByIDParent(dataKey.key).done(data => { categoryController.refreshTable(data); });
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
                categoryRepository.deActive(dataKey).done(data => {
                    if (data.codeError != '00') {
                        alert(data.strError);
                    }
                    else {
                        dataKey = menuCategory.getKeyActiveTreeView();
                        if (dataKey != null && dataKey.key != '') {
                            categoryRepository.getAllByIDParent(dataKey.key).done(data => { categoryController.refreshTable(data); });
                        }
                    }
                });
            }
            else {
                categoryRepository.active(dataKey).done(data => {
                    if (data.codeError != '00') {
                        alert(data.strError);
                    }
                    else {
                        dataKey = menuCategory.getKeyActiveTreeView();
                        if (dataKey != null && dataKey.key != '') {
                            categoryRepository.getAllByIDParent(dataKey.key).done(data => { categoryController.refreshTable(data); });
                        }
                    }
                });
            }
        });
    },
};

$(document).ready(function () {
    categoryController.init();
    menuCategory.init();
});
