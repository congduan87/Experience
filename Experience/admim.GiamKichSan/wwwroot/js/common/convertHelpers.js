let convertHelpers = {
    toNumber: function (val) {
        if (val == null)
            return 0;

        val += '';
        val = val.replaceAll(',', '');
        if (val == '')
            return 0;
        else
            return Number(val);
    }
};