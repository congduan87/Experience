$(document).ready(function () {
    blogCategory.init();
});

let blogCategory = {
    editor: {},
    init: function () {
        blogCategory.getAllCategory();

        $('button[type=button]').click(function (evt) {
            evt.preventDefault();
            let data = APIHelper.serializeObj($('form'));
            data.BlogDetail = blogCategory.editor.getData();
            blogCategory.insert(data).done((data) => {
                if (data != null && data.obj != null && data.obj.id > 0) {
                    alert('Cập nhật thành công');
                    $('button[type=reset]').click();
                }
                else {
                    alert('Cập nhật thất bại');
                }
            }).catch((error) => {
                console.log(error);
            });
        });
    },
    getAllCategory: function () {
        $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetAll",
            type: 'GET',
        }).done((data) => {
            if (data != null && data.listObj != null) {
                let option = '';
                let $select = $('select[name=IDCategory]');
                $select.empty();

                for (var i = 0; i < data.listObj.length; i++) {
                    $option = '<option value="' + data.listObj[i].id + '">' + data.listObj[i].name +'</option>';
                    $select.append($option);
                }
            }
        }).catch((error) => {
            console.log(error);
        });
    },
    insert: async function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/Insert",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },


};