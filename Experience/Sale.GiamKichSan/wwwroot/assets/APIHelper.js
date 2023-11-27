let APIHelper = {
    host: 'http://localhost:56018',
    serializeObj: function ($form) {
        let data = {};
        let arrayForm = $form.serializeArray();

        //Add value for attribute of object
        for (var i = 0; i < arrayForm.length; i++) {
            data[arrayForm[i].name] = arrayForm[i].value;
        }

        //Add value for files of object
        let $files = $form.find('input[type=file]');
        if ($files.length > 0) {
            for (var i = 0; i < $files.length; i++) {
                data[$files[i].attr('name')] = $files.files[0];
            }
        }

        return data;
    },

};