let blogRepository = {
    getAll: function () {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/GetAll",
            type: 'GET',
            contentType: 'application/json',
            timeout: 60000,
        });
    },
    getAllByIDCatetory: function (iDCatetory) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/GetAllByIDCategory",
            type: 'GET',
            contentType: 'application/json',
            data: { IDCatetory: iDCatetory },
            timeout: 60000,
        });
    },
    getByID: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/GetByID",
            data: { ID: id },
            contentType: 'application/json',
            timeout: 60000,
            type: 'GET',
        });
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
    active: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/Active?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    deActive: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/DeActive?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    delete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/Delete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    unDelete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Blog/UnDelete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
};