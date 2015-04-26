var zoom = (function () {

    var self = {

        display: function (id) {
            $.getJSON(site.url + "Instrument/GetPhotoZoom", { 'id': id },
                function (data) {

                    $("#dialog-image-container").append("<a id='lnk-dlgimage_" + id + "' href='javascript:;'><img id='img_" + id + "' class='dialogimg' src='data:image/jpg;base64," + data + "' /></a>");

                    $("#lnk-dlgimage_" + id).click(function () {
                        $("#dialog-image-container").html('');
                        $.unblockUI();
                    });

                    $.blockUI({
                        message: $("#dialog-image-container"),
                        css: {
                            centerX: true,
                            senterY: true,
                            top: 30,
                            width: 0,
                            height: 0
                        }
                    });
                });
        }
    };

    return {
        display: self.display
    };
})();