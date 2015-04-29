;(function ($) {

    window.instrument.zoom = {

        init: function (options) {
            var config = {
                photoId: 0,
                maxImageWidth: 0,
                maxImageHeight: 0
            };

            $.extend(config, options);

            $.getJSON(site.url + "Instrument/GetPhotoJson", { 'PhotoId': config.photoId, "Width": config.maxImageWidth, "Height": config.maxImageHeight },
                function (data) {
                    var id = config.photoId;
                    $("#dialog-image-container").append("<a id='lnk-dlgimage_" + id + "' href='javascript:;'><img id='img_" + id + "' class='dialogimg' src='data:image/jpg;base64," + data + "' /></a>");

                    $("#lnk-dlgimage_" + id).click(function () {
                        $("#dialog-image-container").html('');
                        $.unblockUI();
                    });

                    $.blockUI({
                        message: $("#dialog-image-container"),
                        css: {
                            width: config.maxImageWidth,
                            height: config.maxImageHeight,
                            top:  '50px', 
                            left: ($(window).width() - 550) / 2 + 'px', 
                            border: 'none'
                        }
                    });
                });
        }
    }
})(jQuery);