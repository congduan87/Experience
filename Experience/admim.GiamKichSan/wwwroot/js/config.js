let config = {
    baseUrl: "http://localhost:29053",
    token: "",
};

(function ($) {
    $.fn.serializeObject = function () {
        var disabled = $(this).find(':input:disabled').removeAttr('disabled');
        let formdata = $(this).serializeArray();
        disabled.attr('disabled', 'disabled');
        let data = {};
        $(formdata).each(function (index, obj) {
            if (data[obj.name] !== undefined) {
                if (!Array.isArray(data[obj.name])) {
                    data[obj.name] = [data[obj.name]];
                }
                data[obj.name].push(obj.value);
            }
            else {
                data[obj.name] = obj.value;
            }
        });
        return data;
    };
    $.fn.serializeFiles = function () {
        var obj = $(this);
        /* ADD FILE TO PARAM AJAX */
        var formData = new FormData();
        $.each($(obj).find("input[type='file']"), function (i, tag) {
            $.each($(tag)[0].files, function (i, file) {
                formData.append(tag.name, file);
            });
        });
        var disabled = $(this).find(':input:disabled').removeAttr('disabled');
        var params = $(obj).serializeArray();
        disabled.attr('disabled', 'disabled');
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });
        return formData;
    };
})(jQuery);