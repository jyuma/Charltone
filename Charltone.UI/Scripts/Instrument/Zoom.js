/*!
* Charltone instrument.zoom
* Author: John Charlton
* Date: 2015-04
*/

; (function ($) {

    window.instrument.zoom = {

        init: function (options) {
            var config = {
                photoId: 0,
                maxImageWidth: 0,
                maxImageHeight: 0
            };

            $.extend(config, options);
            $.blockUI.defaults.css = {};

            $.getJSON(site.url + "Instrument/GetPhotoJson", { 'PhotoId': config.photoId, "Width": config.maxImageWidth, "Height": config.maxImageHeight },
                function (data) {
                    var id = config.photoId;

                    $("#zoom-dialog-container").append("<a id='lnk-dlgimage_" + id + "' href='javascript:;'><img class='img-rounded' id='img_" + id + "' class='dialogimg' src='data:image/jpg;base64," + data + "' /></a>");

                    $("#lnk-dlgimage_" + id).click(function () {
                        $("#zoom-dialog-container").html('');
                        $.unblockUI();
                    });

                    $.blockUI({
                        message: $("#zoom-dialog-container"),
                        css: {
                                width: config.maxImageWidth,
                                height: config.maxImageHeight,
                                top: '50px', 
                                left: ($(window).width() - 550) / 2 + 'px', 
                                border: 'none',
                                cursor: 'not-allowed'
                             }
                    });
                });
        }
    }
})(jQuery);