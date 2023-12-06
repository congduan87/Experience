$(document).ready(function () {
    blogCategory.init();
    menuCategory.init();
});

let blogCategory = {
    editor: {},
    init: function () {
        CKEDITOR.ClassicEditor.create(document.getElementById("BlogDetail"), {
            // https://ckeditor.com/docs/ckeditor5/latest/features/toolbar/toolbar.html#extended-toolbar-configuration-format
            toolbar: {
                items: [
                    'exportPDF', 'exportWord', '|',
                    'findAndReplace', 'selectAll', '|',
                    'heading', '|',
                    'bold', 'italic', 'strikethrough', 'underline', 'code', 'subscript', 'superscript', 'removeFormat', '|',
                    'bulletedList', 'numberedList', 'todoList', '|',
                    'outdent', 'indent', '|',
                    'undo', 'redo',
                    '-',
                    'fontSize', 'fontFamily', 'fontColor', 'fontBackgroundColor', 'highlight', '|',
                    'alignment', '|',
                    'link', 'insertImage', 'blockQuote', 'insertTable', 'mediaEmbed', 'codeBlock', 'htmlEmbed', '|',
                    'specialCharacters', 'horizontalLine', 'pageBreak', '|',
                    'textPartLanguage', '|',
                    'sourceEditing'
                ],
                shouldNotGroupWhenFull: true
            },
            // Changing the language of the interface requires loading the language file using the <script> tag.
            // language: 'es',
            list: {
                properties: {
                    styles: true,
                    startIndex: true,
                    reversed: true
                }
            },
            // https://ckeditor.com/docs/ckeditor5/latest/features/headings.html#configuration
            heading: {
                options: [
                    { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                    { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                    { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                    { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
                    { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
                    { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
                    { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' }
                ]
            },
            // https://ckeditor.com/docs/ckeditor5/latest/features/editor-placeholder.html#using-the-editor-configuration
            placeholder: 'Welcome to GiamKichSan',
            // https://ckeditor.com/docs/ckeditor5/latest/features/font.html#configuring-the-font-family-feature
            fontFamily: {
                options: [
                    'default',
                    'Arial, Helvetica, sans-serif',
                    'Courier New, Courier, monospace',
                    'Georgia, serif',
                    'Lucida Sans Unicode, Lucida Grande, sans-serif',
                    'Tahoma, Geneva, sans-serif',
                    'Times New Roman, Times, serif',
                    'Trebuchet MS, Helvetica, sans-serif',
                    'Verdana, Geneva, sans-serif'
                ],
                supportAllValues: true
            },
            // https://ckeditor.com/docs/ckeditor5/latest/features/font.html#configuring-the-font-size-feature
            fontSize: {
                options: [10, 12, 14, 'default', 18, 20, 22],
                supportAllValues: true
            },
            // Be careful with the setting below. It instructs CKEditor to accept ALL HTML markup.
            // https://ckeditor.com/docs/ckeditor5/latest/features/general-html-support.html#enabling-all-html-features
            htmlSupport: {
                allow: [
                    {
                        name: /.*/,
                        attributes: true,
                        classes: true,
                        styles: true
                    }
                ]
            },
            // Be careful with enabling previews
            // https://ckeditor.com/docs/ckeditor5/latest/features/html-embed.html#content-previews
            htmlEmbed: {
                showPreviews: true
            },
            // https://ckeditor.com/docs/ckeditor5/latest/features/link.html#custom-link-attributes-decorators
            link: {
                decorators: {
                    addTargetToExternalLinks: true,
                    defaultProtocol: 'https://',
                    toggleDownloadable: {
                        mode: 'manual',
                        label: 'Downloadable',
                        attributes: {
                            download: 'file'
                        }
                    }
                }
            },
            // https://ckeditor.com/docs/ckeditor5/latest/features/mentions.html#configuration
            mention: {
                feeds: [
                    {
                        marker: '@',
                        feed: [
                            '@apple', '@bears', '@brownie', '@cake', '@cake', '@candy', '@canes', '@chocolate', '@cookie', '@cotton', '@cream',
                            '@cupcake', '@danish', '@donut', '@dragée', '@fruitcake', '@gingerbread', '@gummi', '@ice', '@jelly-o',
                            '@liquorice', '@macaroon', '@marzipan', '@oat', '@pie', '@plum', '@pudding', '@sesame', '@snaps', '@soufflé',
                            '@sugar', '@sweet', '@topping', '@wafer'
                        ],
                        minimumCharacters: 1
                    }
                ]
            },
            // The "super-build" contains more premium features that require additional configuration, disable them below.
            // Do not turn them on unless you read the documentation and know how to configure them and setup the editor.
            removePlugins: [
                // These two are commercial, but you can try them out without registering to a trial.
                // 'ExportPdf',
                // 'ExportWord',
                'AIAssistant',
                'CKBox',
                'CKFinder',
                'EasyImage',
                // This sample uses the Base64UploadAdapter to handle image uploads as it requires no configuration.
                // https://ckeditor.com/docs/ckeditor5/latest/features/images/image-upload/base64-upload-adapter.html
                // Storing images as Base64 is usually a very bad idea.
                // Replace it on production website with other solutions:
                // https://ckeditor.com/docs/ckeditor5/latest/features/images/image-upload/image-upload.html
                // 'Base64UploadAdapter',
                'RealTimeCollaborativeComments',
                'RealTimeCollaborativeTrackChanges',
                'RealTimeCollaborativeRevisionHistory',
                'PresenceList',
                'Comments',
                'TrackChanges',
                'TrackChangesData',
                'RevisionHistory',
                'Pagination',
                'WProofreader',
                // Careful, with the Mathtype plugin CKEditor will not load when loading this sample
                // from a local file system (file://) - load this site via HTTP server if you enable MathType.
                'MathType',
                // The following features are part of the Productivity Pack and require additional license.
                'SlashCommand',
                'Template',
                'DocumentOutline',
                'FormatPainter',
                'TableOfContents',
                'PasteFromOfficeEnhanced'
            ],
        })
            .then(newEditor => {
                blogCategory.editor = newEditor;
            })
            .catch(error => {
                console.log(error);
            });

        $('.edit-dialog').click(blogCategory.evtShowDialog);
        $('select[name=IDCategory]').select2();
        $('select[name=IDTag]').select2();
    },
    getAllBlogByCategory: function (idCategory) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/GetAllByIDCategory",
            type: 'GET',
            data: { IDCatetory: idCategory }
        });
    },
    getAllTag: function () {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Tag/GetAll",
            type: 'GET',
        });
    },
    getByID: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/GetByID?ID=" + id,
            contentType: 'application/json',
            timeout: 60000,
            type: 'GET',
        });
    },
    showDialogUpdate: function (dataKeyCurrent) {
        let $model = $('#ModalCreate');
        var dataCategory = menuCategory.getKeyActiveTreeView();
        if (dataCategory.key == null || dataCategory.key == '' || dataCategory == '0') {
            alert('Yêu cầu chọn tên danh mục!')
            return;
        }
        menuCategory.getAllByIDParent(dataCategory.key).done((data) => {
            if (data != null && data.listObj != null) {
                let option = '';
                let $select = $('select[name=IDCategory]');
                $select.empty();
                $select.append('<option value="' + dataCategory.key + '">' + dataCategory.value + '</option>');
                for (var i = 0; i < data.listObj.length; i++) {
                    $option = '<option value="' + data.listObj[i].id + '">' + data.listObj[i].name + '</option>';
                    $select.append($option);
                }
            }
        }).catch((error) => {
            console.log(error);
        });
        blogCategory.getAllTag().done((data) => {
            if (data != null && data.listObj != null) {
                let option = '';
                let $select = $('select[name=IDTag]');
                $select.empty();                
                for (var i = 0; i < data.listObj.length; i++) {
                    $option = '<option value="' + data.listObj[i].id + '">' + data.listObj[i].name + '</option>';
                    $select.append($option);
                }
            }
        }).catch((error) => {
            console.log(error);
        });
        if (dataKeyCurrent != '') {
            blogCategory.getByID(dataKeyCurrent).done(data => {
                if (data != null && data.obj != null) {
                    if (data.obj.blog != null) {
                        $model.find('input[name="ID"]').val(data.obj.blog.id);
                        $model.find('input[name="DateShow"]').val(data.obj.blog.dateShow);
                        $model.find('input[name="Title"]').val(data.obj.blog.title);
                        $model.find('textarea[name="Description"]').val(data.obj.blog.description);
                        $model.find('input[name="ImageAvatar"]').val(data.obj.blog.imageAvatar);
                    }

                    if (data.obj.blogCategories != null && data.obj.blogCategories.length > 0) {
                        let idcategories = [];
                        data.obj.blogCategories.forEach(element => idcategories.push(element.idCategory));
                        $('select[name=IDCategory]').val(idcategories).trigger('change');
                    }
                    if (data.obj.blogTags != null && data.obj.blogTags.length > 0) {
                        let idTags = [];
                        data.obj.blogTags.forEach(element => idTags.push(element.idTag));
                        $('select[name=IDTag]').val(idTags).trigger('change');
                    }
                    if (data.obj.blogDetails != null && data.obj.blogDetails.length > 0) {
                        let details = '';
                        data.obj.blogDetails.forEach(element => { details += element.description; });
                        blogCategory.editor.setData(details);
                    }

                    

                    document.getElementById('ModalButton').setAttribute('data-target', '#ModalCreate');
                    document.getElementById('ModalButton').click();
                    $('.save-blog').unbind('click').click(blogCategory.evtUpdateBlog);
                }
            });

            return;
        }
        else {
            $model.find('input[name="ID"]').val("0");
            $model.find('input[name="DateShow"]').val('');
            $model.find('input[name="Title"]').val('');
            $model.find('input[name="Description"]').val('');
            $model.find('input[name="ImageAvatar"]').val('');

            document.getElementById('ModalButton').setAttribute('data-target', '#ModalCreate');
            document.getElementById('ModalButton').click();
            $('.save-blog').unbind('click').click(blogCategory.evtUpdateBlog);
        }
    },
    refreshTable: function (data) {
        $("#tbTable").empty();
        if (data != null && data.listObj != null && data.listObj.length > 0) {
            var bodyContent = "";
            var lock = "";
            for (let i = 0; i < data.listObj.length; i++) {
                lock = (data.listObj[i].isActive == 'Y' ? 'class="btn btn-danger btn-xs" title="Khóa"><i class="fa fa-lock"></i>' : 'class="btn btn-dark btn-xs"  title="Mở khóa"><i class="fa fa-unlock"></i>');
                if (i % 0 == 0) {
                    bodyContent = '<tr class="even pointer" id="' + data.listObj[i].id + '">';
                }
                else {
                    bodyContent = '<tr class="odd pointer" id="' + data.listObj[i].id + '">';
                }

                bodyContent += '<td class="">' + (i + 1) + '</td>';
                bodyContent += '<td class="">' + data.listObj[i].dateShow + '</td>';
                bodyContent += '<td class="">' + data.listObj[i].title + '</td>';
                bodyContent += '<td class="">' + data.listObj[i].description + '</td>';
                bodyContent += '<td class="">' + '<button ' + lock + '</button></td>';
                bodyContent += '<td class="last">';
                bodyContent += '<button class="btn btn-info btn-xs" title= "Xem"  data-key="' + data.listObj[i].id + '"><i class="fa fa-eye"></i></button>';
                bodyContent += '<button class="btn btn-warning btn-xs edit-dialog" title= "Sửa"  data-key="' + data.listObj[i].id + '"><i class="fa fa-pencil"></i></button>';
                bodyContent += '<button class="btn btn-dark btn-xs delete-dialog" title= "Xóa" data-key="' + data.listObj[i].id + '"><i class="fa fa-trash"></i></button>';
                bodyContent += '</td>';
                bodyContent += '</tr>';
                $("#tbTable").append(bodyContent);
            }
            blogCategory.refreshEvent();
        }

    },
    insert: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/Insert",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    edit: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/Edit",
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    evtShowDialog: function (evnt) {
        evnt.stopPropagation();
        let dataKey = $(this).attr('data-key');
        dataKey = dataKey == null ? '' : dataKey;
        blogCategory.showDialogUpdate(dataKey);
    },
    evtUpdateBlog: function (evt) {
        evt.preventDefault();
        let data = APIHelper.serializeObj($('#ModalCreate .modal-content form'));
        data.BlogDetail = blogCategory.editor.getData();
        if (!Array.isArray(data.IDTag)) {
            if (isFinite(data.IDTag))
                data.IDTag = [Number(data.IDTag)];
            else
                data.IDTag = [data.IDTag];
        }
        if (!Array.isArray(data.IDCategory)) {
            if (isFinite(data.IDCategory))
                data.IDCategory = [Number(data.IDCategory)];
            else
                data.IDCategory = [data.IDCategory];
        }
        if (data.ID != '' && data.ID != '0') {
            data.ID = Number(data.ID);
            blogCategory.edit(data).done((data) => {
                if (data != null && data.obj != null && data.obj.id > 0) {
                    alert('Cập nhật thành công');
                    $("#ModalCreate").modal('hide');
                }
                else {
                    alert('Cập nhật thất bại');
                }
            }).catch((error) => {
                console.log(error);
            });
        }
        else {
            blogCategory.insert(data).done((data) => {
                if (data != null && data.obj != null && data.obj.id > 0) {
                    alert('Cập nhật thành công');
                    $("#ModalCreate").modal('hide');
                }
                else {
                    alert('Cập nhật thất bại');
                }
            }).catch((error) => {
                console.log(error);
            });
        }
        
    },
    refreshEvent: function () {
        $('.edit-dialog').unbind('click').click(blogCategory.evtShowDialog);
        $('.save-blog').unbind('click').click(blogCategory.evtUpdateBlog);
        $('.delete-dialog').unbind('click').click(function (evnt) {
            evnt.stopPropagation();
            let dataKey = $(this).attr('data-key');
            dataKey = dataKey == null ? '' : dataKey;
            blogCategory.delete(dataKey).done(data => {
                if (data.codeError != '00') {
                    alert(data.strError);
                }
                else {
                    blogCategory.getAll().done(data => { blogCategory.refreshTable(data); });
                }
            });
        });
    },
};

let menuCategory = {
    classTreeView: 'tree-view',
    idContextMenuTreeView: 'contextMenuTreeView',
    categories: [],
    //Get Menu Folder
    getAllTreeView: function () {
        $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetAll",
            type: 'GET',
        }).done((data) => {
            menuCategory.categories = [];
            if (data != null && data.listObj != null) {
                menuCategory.categories = data.listObj;
                menuCategory.refreshTreeView(menuCategory.categories);
            }
        }).catch((err) => {
            menuCategory.categories = [];
        });
    },
    getAllByIDParent: function (idCategory) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetAllByIDParent",
            type: 'GET',
            data: { IDParent: idCategory },
        });
    },
    refreshEvent: function () {
        //Sự kiện collapse_expand treeview
        $('.tree-view li').unbind('click').click(menuCategory.isCollapseExpand);

        $('.section-treeview .tree-view ul li').unbind('dblclick').dblclick(function (evnt) {
            evnt.stopPropagation();
            $('.section-treeview .tree-view ul li').removeClass('active');
            $(this).addClass('active');
            var dataKey = this.getAttribute("data-key");
            if (dataKey != null && dataKey != '') {
                blogCategory.getAllBlogByCategory(dataKey).done(data => { blogCategory.refreshTable(data); });
            }
        });
    },
    refreshTreeView: function () {
        let $treeView = $('.' + menuCategory.classTreeView + '>ul');
        $treeView.empty();
        let indexFolder = 0;
        let temFolder = {};

        for (indexFolder = 0; indexFolder < menuCategory.categories.length; indexFolder++) {
            if (menuCategory.categories[indexFolder].idParent == 0) {
                temFolder = menuCategory.categories[indexFolder];
                let $liTreeView = $('<li></li>');
                $liTreeView.attr('data-key', temFolder.id);
                $liTreeView.html(temFolder.name);
                $treeView.append($liTreeView);
                menuCategory.addChildTreeView(menuCategory.categories, temFolder, $liTreeView);
            }
        }
        menuCategory.refreshEvent();
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
        if ($obj[0] != null)
            itemValue = $obj[0].outerText.split('\n')[0];

        return {
            key: $obj.attr('data-key'),
            value: itemValue
        };
    },
    init: function () {
        menuCategory.getAllTreeView();
    }
};