let APIHelper = {
    host: 'http://localhost:56018',
    serializeObj: function ($form) {
        let data = {};
        let arrayForm = $form.serializeArray();

        //Add value for attribute of object
        for (var i = 0; i < arrayForm.length; i++) {
            if (data[arrayForm[i].name] != null && Array.isArray(data[arrayForm[i].name])) {
                data[arrayForm[i].name].push(arrayForm[i].value);
            }
            else if (data[arrayForm[i].name] != null) {
                if (isFinite(data[arrayForm[i].name]))
                    data[arrayForm[i].name] = [Number(data[arrayForm[i].name])];
                else
                    data[arrayForm[i].name] = [data[arrayForm[i].name]];

                if (isFinite(arrayForm[i].value))
                    data[arrayForm[i].name].push(Number(arrayForm[i].value));
                else
                    data[arrayForm[i].name].push(arrayForm[i].value);
            }
            else
                data[arrayForm[i].name] = arrayForm[i].value;
        }

        //Add value for files of object
        let $files = $form.find('input[type=file]');
        if ($files.length > 0) {
            for (var i = 0; i < $files.length; i++) {
                if ($files[i].getAttribute('name') != null && data[$files[i].getAttribute('name')] != null) {
                    data[$files[i].getAttribute('name')] = $files[i].files[0];
                }
            }
        }

        return data;
    },

};