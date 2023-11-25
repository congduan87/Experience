$(document).ready(function () {
    menuFolder.init();
    uploadFile.init();
    document.oncontextmenu = (e) => {
        e.preventDefault();
        document.getElementById(menuFolder.idContextMenuTreeView).style.display = "none";
        document.getElementById(uploadFile.idContextMenuFileUpload).style.display = "none";

        let $item = $(e.target);
        if ($item.parents('.' + menuFolder.classTreeView).length >= 1) {
            let menu = document.getElementById(menuFolder.idContextMenuTreeView);
            let keytemp = $item.attr('data-key');
            keytemp = keytemp != null ? keytemp : '';

            menu.style.display = 'block';
            menu.style.left = e.pageX + "px";
            menu.style.top = e.pageY + "px";
            $(menu).find('a').each((index, obj) => {
                obj.setAttribute('data-key', keytemp);
            });
        }
        else if ($item.parents('.' + uploadFile.classFileUpload).length >= 1) {
            let menu = document.getElementById(uploadFile.idContextMenuFileUpload);
            let keytemp = $item.parents('li').attr('data-key');
            keytemp = keytemp != null ? keytemp : '';

            menu.style.display = 'block';
            menu.style.left = e.pageX + "px";
            menu.style.top = e.pageY + "px";
            $(menu).find('a').each((index, obj) => {
                obj.setAttribute('data-key', keytemp);
            });
        }
    };
    document.onclick = (e) => {
        if (document.getElementById(menuFolder.idContextMenuTreeView).style.display == "block") {
            document.getElementById(menuFolder.idContextMenuTreeView).style.display = "none";
        }
        if (document.getElementById(uploadFile.idContextMenuFileUpload).style.display == "block") {
            document.getElementById(uploadFile.idContextMenuFileUpload).style.display = "none";
        }
    };
});

let menuFolder = {
    classTreeView: 'tree-view',
    idContextMenuTreeView: 'contextMenuTreeView',
    folders: [],
    //Get Menu Folder
    getAllTreeView: function () {
        $.ajax({
            url: "/Accounts/FolderUpload/GetByIsUse",
            type: 'GET',
            data: { IDAccount: '1', IsUse: 'Y' },
        }).done((data) => {
            menuFolder.folders = [];
            if (data != null && data.listObj != null) {
                menuFolder.folders = data.listObj;
                menuFolder.refreshTreeView(menuFolder.folders);
            }
        }).catch((err) => {
            menuFolder.folders = [];
        });
    },
    refreshEvent: function () {
        //Sự kiện collapse_expand treeview
        $('.tree-view li').unbind('click').click(menuFolder.isCollapseExpand);

        ////Sự kiện active treeview
        //$('.upload-file .tree-view ul li').unbind('click').click(function (evnt) {
        //    evnt.stopPropagation();
        //    $('.upload-file .tree-view ul li').removeClass('active');
        //    $(this).addClass('active');
        //});

        $('.upload-file .tree-view ul li').unbind('dblclick').dblclick(function (evnt) {
            evnt.stopPropagation();
            $('.upload-file .tree-view ul li').removeClass('active');
            $(this).addClass('active');
            var dataKey = this.getAttribute("data-key");
            if (dataKey != null && dataKey != '') {
                uploadFile.getAllFileUpload(dataKey);
            }
        });
    },
    refreshTreeView: function () {
        let $treeView = $('.' + menuFolder.classTreeView + '>ul');
        $treeView.empty();
        let indexFolder = 0;
        let temFolder = {};

        for (indexFolder = 0; indexFolder < menuFolder.folders.length; indexFolder++) {
            if (menuFolder.folders[indexFolder].levelChild == 1) {
                temFolder = menuFolder.folders[indexFolder];
                let $liTreeView = $('<li></li>');
                $liTreeView.attr('data-key', temFolder.id);
                $liTreeView.html(temFolder.name);
                $treeView.append($liTreeView);

                menuFolder.folders.splice(indexFolder, 1);
                indexFolder--;
                menuFolder.addChildTreeView(menuFolder.folders, temFolder, $liTreeView);
            }
        }
        menuFolder.refreshEvent();
    },
    addChildTreeView: function (data, parent, $liParent) {
        let $ulChild = $("<ul></ul>");
        let isChild = false;
        let indexChildFolder = 0;
        let temChildFolder = {};

        for (indexChildFolder = 0; indexChildFolder < data.length; indexChildFolder++) {
            if (data[indexChildFolder].idParent == parent.id) {
                isChild = true;
                temChildFolder = data[indexChildFolder];

                let $liChild = $('<li></li>');
                $liChild.attr('data-key', temChildFolder.id);
                $liChild.html(temChildFolder.name);
                $ulChild.append($liChild);

                data.splice(indexChildFolder, 1);
                indexChildFolder--;
                menuFolder.addChildTreeView(data, temChildFolder, $liChild);
            }
        }
        if (isChild) {
            if (parent.levelChild >= 2)
                $liParent.addClass("expand");//collapse
            else
                $liParent.addClass("expand");

            $liParent.append($ulChild);
        }
    },
    isCollapseExpand: function (evnt) {
        evnt.stopPropagation();
        let vclass = this.getAttribute('class') + '';
        if (vclass.indexOf('expand') >= 0)
            $(this).removeClass("expand").addClass("collapse");
        else if (vclass.indexOf('collapse') >= 0)
            $(this).removeClass("collapse").addClass("expand");
    },
    //Lấy giá trị của Treeview đang active
    getKeyActiveTreeView: function () {
        let $obj = $('.upload-file .tree-view ul li.active');
        let itemValue = "";
        if ($obj[0] != null)
            itemValue = $obj[0].outerText.split('\n')[0];

        return {
            key: $obj.attr('data-key'),
            value: itemValue
        };
    },
    updateNodeTreeView: function (item, kindType) {
        if (kindType == 1) {
            if (item.idParent == 0)
                $(".upload-file .tree-view ul").append('<li data-key="' + item.id + '">' + item.name + '</li>');
            else {
                let $parent = $(".upload-file .tree-view li[data-key=" + item.idParent + "]");

                if ($parent.find("ul").length > 0)
                    $parent.find("ul").append('<li data-key="' + item.id + '">' + item.name + '</li>');
                else {
                    $parent.addClass("expand");
                    $parent.append('<ul><li data-key="' + item.id + '">' + item.name + '</li></ul>');
                }
            }
        }
        else if (kindType == 2) {
            $(".upload-file .tree-view li[data-key=" + item.id + "]").html(item.name);
        }
        else {
            $(".upload-file .tree-view li").remove("li[data-key=" + item.id + "]");
        }
    },
    showDialogUpdateFolder: function (dataKeyCurrent) {
        let $model = $('#ModalCreateFolder');
        if (dataKeyCurrent != '') {
            $.ajax({
                url: "/Accounts/FolderUpload/GetByID",
                type: 'GET',
                data: { id: dataKeyCurrent, IDAccount: '1' },
            }).done((data) => {
                if (data != null && data.obj != null) {
                    $model.find('input[name="ID"]').val(data.obj.id);
                    $model.find('input[name="IDParent"]').val(data.obj.idParent);
                    $model.find('input[name="Name"]').val(data.obj.name);
                }
            });
        }
        else {
            var item = menuFolder.getKeyActiveTreeView();
            if (item.key == null)
                $model.find('#CreateFolderModalLabel').html('Cập nhật folder');
            else
                $model.find('#CreateFolderModalLabel').html('Cập nhật folder có thư mục cha là "' + item.value + '"');
            $model.find('input[name="ID"]').val("0");
            $model.find('input[name="IDParent"]').val(item.key == null ? '0' : item.key);
            $model.find('input[name="Name"]').val('');
        }

        document.getElementById('ModalButton').setAttribute('data-target', '#ModalCreateFolder');
        document.getElementById('ModalButton').click();
    },
    updateFolder: function () {
        let $model = $('#ModalCreateFolder form');
        let arrayForm = $model.serializeArray();
        let data = {};
        for (var i = 0; i < arrayForm.length; i++) {
            data[arrayForm[i].name] = arrayForm[i].value;
        }

        if (data.ID != null && data.ID != 0) {
            $.ajax({
                url: "/Accounts/FolderUpload/Edit",
                type: 'PUT',
                data: JSON.stringify(data),
                contentType: 'application/json',
            }).done((data) => {
                for (var i = 0; i < menuFolder.folders.length; i++) {
                    if (menuFolder.folders[i].id == data.obj.id) {
                        menuFolder.folders[i] = data.obj;
                        break;
                    }
                }
                menuFolder.updateNodeTreeView(data.obj, 2);
                menuFolder.refreshEvent();
                $("#ModalCreateFolder").modal('hide');
            }).catch((error) => {
                console.log(error);
                $("#ModalCreateFolder").modal('hide');
            });
        }
        else {
            $.ajax({
                url: "/Accounts/FolderUpload/Insert",
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
            }).done((data) => {
                menuFolder.folders.push(data.obj);
                menuFolder.updateNodeTreeView(data.obj, 1);
                menuFolder.refreshEvent();
                $("#ModalCreateFolder").modal('hide');
            }).catch((error) => {
                console.log(error);
                $("#ModalCreateFolder").modal('hide');
            });
        }
    },
    deleteFolder: function (dataKeyCurrent) {
        if (dataKeyCurrent != null && dataKeyCurrent != 0) {
            $.ajax({
                url: "/Accounts/FolderUpload/Delete?id=" + dataKeyCurrent,
                type: 'DELETE',
                contentType: 'application/json; charset=utf-8',
            }).done((data) => {
                if (data.codeError == "00" && data.obj) {
                    document.getElementById(menuFolder.idContextMenuTreeView).style.display = "none";
                    menuFolder.updateNodeTreeView(data.obj, 3);
                }
            }).catch((error) => {
                console.log(error);
                document.getElementById(menuFolder.idContextMenuTreeView).style.display = "none";
            });
        }
    },
    init: function () {
        menuFolder.getAllTreeView();
        $('.edit-folder').click(function (evnt) {
            evnt.stopPropagation();
            let vclass = this.getAttribute('class') + '';
            if (vclass.indexOf('expand') >= 0)
                $(this).removeClass("expand").addClass("collapse");
            else if (vclass.indexOf('collapse') >= 0)
                $(this).removeClass("collapse").addClass("expand");

            //Sự kiện view insert, edit folder
            let dataKey = $(this).find('a').attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            menuFolder.showDialogUpdateFolder(dataKey);
        });
        $('.delete-folder').click(function (evnt) {
            evnt.stopPropagation();
            let vclass = this.getAttribute('class') + '';
            if (vclass.indexOf('expand') >= 0)
                $(this).removeClass("expand").addClass("collapse");
            else if (vclass.indexOf('collapse') >= 0)
                $(this).removeClass("collapse").addClass("expand");

            //Sự kiện delete folder
            let dataKey = $(this).find('a').attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            menuFolder.deleteFolder(dataKey);
        });
        //Sự kiện insert, edit folder
        $('.save-folder').click(function (evnt) {
            menuFolder.updateFolder();
        });
    }
};

var uploadFile = {
    idContextMenuFileUpload: 'contextMenuFileUpload',
    classFileUpload: 'file-upload-list',
    filesUpload: [],
    refreshEvent: function () {
        //Sự kiện active upload file
        $('.upload-file .file-upload-list ul li').unbind('click').click(function (evnt) {
            $(this).toggleClass('active');
        });
    },
    getAllFileUpload: function (idFolder) {
        $.ajax({
            url: "/Accounts/FileUpload/GetFilesByIDFolder/Y",
            type: 'GET',
            data: { IDFolder: idFolder },
        }).done((data) => {
            uploadFile.filesUpload = [];
            if (data != null && data.listObj != null && data.listObj.length > 0) {
                uploadFile.filesUpload = data.listObj;
            }
            uploadFile.refreshFileUpload();
        }).catch((err) => {
            uploadFile.filesUpload = [];
            uploadFile.refreshFileUpload();
        });
    },
    //Lấy giá trị của FileUpload đang active
    getKeyActiveFileUpload: function () {
        let fileUploadActive = [];
        $('.upload-file .file-upload-list ul li.active').each((index, obj) => {
            fileUploadActive.push(obj.getAttribute('data-key'));
        });
        return fileUploadActive;
    },
    refreshFileUpload: function () {
        let $fileUpload = $('.' + uploadFile.classFileUpload + ' ul');
        $fileUpload.empty();
        for (let i = 0; i < uploadFile.filesUpload.length; i++) {
            let $lifileUpload = $('<li></li>');
            $lifileUpload.attr('data-key', uploadFile.filesUpload[i].id);
            $lifileUpload.append('<p class="image"><img src="./uploads/' + uploadFile.filesUpload[i].path + '" alt="' + uploadFile.filesUpload[i].name + '"></p>');
            $lifileUpload.append('<p class="detail">' + uploadFile.filesUpload[i].name + '</p>');
            $fileUpload.append($lifileUpload);
        }
        uploadFile.refreshEvent();
    },
    removeValue: function () {
        document.getElementById(sef.idContextMenuTreeView).removeAttribute('data-key');
        document.getElementById(sef.idContextMenuFileUpload).removeAttribute('data-key');
    },
    copyLinkFileUpload: function (dataKeyCurrent) {
        if (dataKeyCurrent != '') {
            $.ajax({
                url: "/Accounts/FolderUpload/GetByID",
                type: 'GET',
                data: { id: dataKeyCurrent, IDAccount: '1' },
            }).done((data) => {
                document.getElementById(uploadFile.idContextMenuFileUpload).style.display = "none";
                if (data != null && data.obj != null) {
                    navigator.clipboard.writeText((window.location.protocol + '//'
                        + window.location.hostname + ':'
                        + window.location.port
                        + "/uploads/"
                        + data.obj.path).replaceAll('\\', '/'));
                }
            });
        }
    },//Insert, update Folder
    showDialogUpdateFile: function (dataKeyCurrent) {
        let $model = $('#ModalFileUpload');
        if (dataKeyCurrent != '') {
            $.ajax({
                url: "/Accounts/FolderUpload/GetByID",
                type: 'GET',
                data: { id: dataKeyCurrent, IDAccount: '1' },
            }).done((data) => {
                if (data != null && data.obj != null) {
                    $model.find('input[name="ID"]').val(data.obj.id);
                    $model.find('input[name="IDParent"]').val(data.obj.idParent);
                    $model.find('input[name="Name"]').val(data.obj.name);
                    $model.find('input[name="Path"]').val(data.obj.path);
                    document.getElementsByName("file")[0].value = null;
                }
            });
        }
        else {
            var item = menuFolder.getKeyActiveTreeView();
            if (item.key == null)
                $model.find('#FileUploadLabel').html('Cập nhật file');
            else
                $model.find('#FileUploadLabel').html('Cập nhật file có thư mục cha là "' + item.value + '"');
            $model.find('input[name="ID"]').val("0");
            $model.find('input[name="IDParent"]').val(item.key == null ? '0' : item.key);
            $model.find('input[name="Name"]').val('');
            $model.find('input[name="Path"]').val('');
        }

        document.getElementById('ModalButton').setAttribute('data-target', '#ModalFileUpload');
        document.getElementById('ModalButton').click();
    },
    updateFile: function () {
        let $model = $('#ModalFileUpload form');
        let arrayForm = $model.serializeArray();
        let data = new FormData();
        var fileUp = document.getElementsByName("file")[0].files[0];

        for (var i = 0; i < arrayForm.length; i++) {
            data[arrayForm[i].name] = arrayForm[i].value;
            data.append(arrayForm[i].name, arrayForm[i].value);
        }        
        data.append("file", fileUp);

        if (data.IDParent == null || data.IDParent == "" || data.IDParent == '0') {
            alert('Chưa có thư mục. Hãy chọn thư mục trước khi upload file');
            return;
        }
        if (data.Path == "" && (fileUp == null || fileUp.size == 0)) {
            alert('Yêu cầu chọn file');
            return;
        }

        if (data.ID != null && data.ID != 0) {
            $.ajax({
                url: "/Accounts/FileUpload/Edit",
                type: 'PUT',
                data: data,
                contentType: false,
                processData: false,
                timeout: 60000
            }).done((data) => {
                $("#ModalFileUpload").modal('hide');
                if (data != null && data.obj != null && data.obj.id > 0) {
                    let $fileUpload = $('.' + uploadFile.classFileUpload + ' ul li[data-key=' + data.obj.id + ']');
                    $fileUpload.find('p').remove('p');
                    $fileUpload.append('<p class="image"><img src="./uploads/' + data.obj.path + '" alt="' + data.obj.name + '"></p>');
                    $fileUpload.append('<p class="detail">' + data.obj.name + '</p>');

                    document.getElementsByName("file")[0].value = null;
                }
            }).catch((error) => {
                console.log(error);
                $("#ModalFileUpload").modal('hide');
            });
        }
        else {
            $.ajax({
                url: "/Accounts/FileUpload/Insert",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                timeout: 60000
            }).done((data) => {
                $("#ModalFileUpload").modal('hide');
                if (data != null && data.obj != null && data.obj.id > 0) {
                    let $fileUpload = $('.' + uploadFile.classFileUpload + ' ul');
                    let $lifileUpload = $('<li></li>');
                    $lifileUpload.attr('data-key', data.obj.id);
                    $lifileUpload.append('<p class="image"><img src="./uploads/' + data.obj.path + '" alt="' + data.obj.name + '"></p>');
                    $lifileUpload.append('<p class="detail">' + data.obj.name + '</p>');
                    $fileUpload.append($lifileUpload);

                    document.getElementsByName("file")[0].value = null;
                }
            }).catch((error) => {
                console.log(error);
                $("#ModalFileUpload").modal('hide');
            });
        }
    },
    deleteFile: function (dataKeyCurrent) {
        if (dataKeyCurrent != null && dataKeyCurrent != 0) {
            $.ajax({
                url: "/Accounts/FileUpload/Delete?id=" + dataKeyCurrent,
                type: 'DELETE',
                contentType: 'application/json; charset=utf-8',
            }).done((data) => {
                if (data != null && data.obj != null && data.obj.id > 0) {
                    document.getElementById(uploadFile.idContextMenuFileUpload).style.display = "none";
                    let $fileUpload = $('.' + uploadFile.classFileUpload + ' ul li[data-key=' + data.obj.id + ']').remove('li[data-key=' + data.obj.id + ']');
                }
            }).catch((error) => {
                console.log(error);
                document.getElementById(uploadFile.idContextMenuFileUpload).style.display = "none";
            });
        }
    },
    init: function () {
        //Sự kiện copy file upload
        $('.copy-link-file-upload').click(function (evnt) {
            evnt.stopPropagation();
            let vclass = this.getAttribute('class') + '';
            if (vclass.indexOf('expand') >= 0)
                $(this).removeClass("expand").addClass("collapse");
            else if (vclass.indexOf('collapse') >= 0)
                $(this).removeClass("collapse").addClass("expand");

            //Sự kiện delete
            let dataKey = $(this).find('a').attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            uploadFile.copyLinkFileUpload(dataKey);
        });

        $('.edit-file-upload').click(function (evnt) {
            evnt.stopPropagation();
            let vclass = this.getAttribute('class') + '';
            if (vclass.indexOf('expand') >= 0)
                $(this).removeClass("expand").addClass("collapse");
            else if (vclass.indexOf('collapse') >= 0)
                $(this).removeClass("collapse").addClass("expand");

            //Sự kiện view insert, edit
            let dataKey = $(this).find('a').attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            uploadFile.showDialogUpdateFile(dataKey);
        });
        $('.save-file').click(function (evnt) {
            uploadFile.updateFile();
        });

        $('.delete-file-upload').click(function (evnt) {
            evnt.stopPropagation();
            let vclass = this.getAttribute('class') + '';
            if (vclass.indexOf('expand') >= 0)
                $(this).removeClass("expand").addClass("collapse");
            else if (vclass.indexOf('collapse') >= 0)
                $(this).removeClass("collapse").addClass("expand");

            //Sự kiện delete
            let dataKey = $(this).find('a').attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            uploadFile.deleteFile(dataKey);
        });

    }
};


