;(function ($) {

    window.zoom = {};

    zoom.init = {
        display: function (id) {
            $.getJSON(site.url + "Instrument/GetPhotoZoom", { 'id': id },
                function (data) {

                    $("#dialog-image-container").append("<a id='lnk-dlgimage_" + id + "' href='javascript:;'><img id='img_" + id + "' class='dialogimg' src='data:image/jpg;base64," + data + "' /></a>");

                    $("#lnk-dlgimage_" + id).click(function () {
                        $("#dialog-image-container").html('');
                        $('body').unblock();
                    });

                    $('body').block({
                        message: $("#dialog-image-container"),
                        css: {
                            width: 520,     // must correspond with InstrumentPhoto.Width Constant
                            height: 676,    // must correspond with InstrumentPhoto.Height Constant
                            border: 'none'
                        }
                    });
                });
        }
    }
})(jQuery);