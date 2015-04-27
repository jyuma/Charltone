;(function ($) {

    window.ordering = {};

    ordering.edit = {
        init: function (options) {

            var config = {
                orderingId: 0,
            };

            jQuery.extend(config, options);

            file.upload.init(function (result) {
                displayPhoto(result.id);
            });

            function displayPhoto() {
                $.getJSON(site.url + "Ordering/GetPhotoJson", { "id": config.orderingId },
                    function (data) {
                        $("#orderingeditphoto").attr('src', 'data:image/jpg;base64,' + data + '');
                    });
            };
        }
    }
})(jQuery);