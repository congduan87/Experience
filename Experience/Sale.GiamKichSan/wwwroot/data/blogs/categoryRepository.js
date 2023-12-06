let categoryRepository = {
    getAll: function () {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetAll",
            type: 'GET',
            contentType: 'application/json',
            timeout: 60000,
        });
    },
    getAllByIDParent: function (iDParent) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetAllByIDParent",
            type: 'GET',
            contentType: 'application/json',
            data: { IDParent: iDParent },
            timeout: 60000,
        });
    },
    getByID: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/GetByID",
            data: { ID: id },
            contentType: 'application/json',
            timeout: 60000,
            type: 'GET',
        });
    },
    insert: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/Insert",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    edit: function (data) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/Edit",
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: 'application/json',
            timeout: 60000
        });
    },
    active: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/Active?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    deActive: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/DeActive?id=" + id,
            type: 'PUT',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    delete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/Delete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
    unDelete: function (id) {
        return $.ajax({
            url: APIHelper.host + "/Blogs/Category/UnDelete?id=" + id,
            type: 'DELETE',
            contentType: 'application/json',
            timeout: 60000
        });
    },
};