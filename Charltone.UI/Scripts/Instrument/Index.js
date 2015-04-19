var zoom = (function () {
    var self = {
        config: { url: "" },

        init: function(options) {
            $.extend(self.config, options);
            self.bind();
        },

        bind: function() {
            $.getJSON(site.url + "Instrument/GetDefaultPhotoIds", function (data) {
                $(data).each(function(index, value) {
                    if (value.Id > 0) {
                        $("#img_" + value.Id).click(function() {
                            self.display(value.Id);
                        });
                    }
                });
            });
        },

        display: function(id) {
            $.getJSON(site.url + "Instrument/GetPhotoJson", { 'id': id },
                function(data) {

                    $("#dialog-image-container").append("<a id='lnk-dlgimage_" + id + "' href='javascript:;'><img id='listdialogimg_" + id + "' class='listdialogimg' src='data:image/jpg;base64," + data + "' /></a>");

                    $("#lnk-dlgimage_" + id).click(function() {
                        $("#dialog-image-container").html('');
                        $.unblockUI();
                    });

                    $.blockUI({
                        message: $("#dialog-image-container"),
                        backgroundColor: '#000',
                        color: '#000',
                        css: {
                            top: 30,
                            width: 0,
                            height: 0
                        }
                    });
                });
        }
    };

    return {
        init: self.init
    };
})();