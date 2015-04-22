var file = function () {
    var self = {
        enableUpload: function () {
            $('input:file').change(
                function () {
                    if ($(this).val()) {
                        $('input:submit').removeAttr('disabled');
                    }
                });
        }
    };

    return {
        enableUpload: self.enableUpload
    };
}();