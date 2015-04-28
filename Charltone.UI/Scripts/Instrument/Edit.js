; (function ($) {

    window.instrument.edit = {
        init: function (options) {
            var config = {
                photoId: 0,
                maxImageWidth: 0,
                maxImageHeight: 0,
            };

            $.extend(config, options);

            $.getJSON(site.url + "Instrument/GetPhotoJson", { "PhotoId": config.photoId, "Width": config.maxImageWidth, "Height": config.maxImageHeight },
                function (data) {
                    $("#instreditphoto").attr('src', 'data:image/jpg;base64,' + data + '');
                });
        }
    }
})(jQuery);