/*!
* Charltone ordering.edit
* Author: John Charlton
* Date: 2015-04
*/

; (function ($) {

    window.ordering = {};

    ordering.edit = {
        init: function (options) {

            var config = {
                orderingId: 0,
                maxImageWidth: 0,
                maxImageHeight: 0
            };

            jQuery.extend(config, options);

            file.upload.init(
                {
                    maxImageWidth: config.maxImageWidth,
                    maxImageHeight: config.maxImageHeight
                },
                function (result) { displayPhoto(result.id); }
            );

            displayPhoto();

            function displayPhoto() {
                $.getJSON(site.url + "Ordering/GetPhoto", { "id": config.orderingId },
                    function (data) {
                        $("#orderingeditphoto").attr('src', 'data:image/jpg;base64,' + data + '');
                    });
            };
        }
    }
})(jQuery);