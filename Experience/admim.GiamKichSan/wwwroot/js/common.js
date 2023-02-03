let common = {
    popupPattern: {
        show: function ($obj, innerHTML) {
            $('.custPopupModal')[0].firstElementChild.firstElementChild.innerHTML = innerHTML;
            $('.custPopupModal').modal('show');
        },
        hide: function () {
            $('.custPopupModal').modal('hide');
        }
    },    
    baseApi: {
        get: function (path, data) {
            return $.ajax({
                url: config.baseUrl + '/' + path,
                type: 'GET',
                data: data,
            });
        },
        post: function (path, data) {
            return $.ajax({
                url: config.baseUrl + '/' + path,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
            });
        },
        postForm: function (path, data) {
            var form_data = new FormData();
            if (data instanceof FormData)
                form_data = data;
            else {
                for (var key in data) {
                    form_data.append(key, data[key]);
                }
            }

            return $.ajax({
                url: config.baseUrl + '/' + path,
                type: 'POST',
                data: form_data,
                cache: false,
                contentType: false,
                processData: false,
            });
        },
        put: function (path, data) {
            return $.ajax({
                url: config.baseUrl + '/' + path,
                type: 'PUT',
                data: JSON.stringify(data),
                contentType: 'application/json',
            });
        },
        delete: function (path, data) {
            return $.ajax({
                url: config.baseUrl + '/' + path,
                type: 'DELETE',
            });
        },
    },





};