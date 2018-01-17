/*!
* Charltone instrument.carousel
* Author: John Charlton
* Date: 2015-04
*/

; (function ($) {

    window.instrument.carousel = {
        show: function (options) {
            var config = {
                productId: 0
            };

            $.extend(config, options);

            $.get(site.url + "Instrument/Carousel", { "id": config.productId },
                function (data) {
                    var carousel = $("#carousel-container");

                    if ($(carousel.children()).length === 0) {
                        $(carousel).html(data);
                        $("#carousel-close-link").click(function () {
                            $(carousel).html("");
                            $.unblockUI();
                        });
                    }

                    $.blockUI({
                        message: $(carousel),
                        css: {
                            top: 60,
                            left: ($(window).width() - 700) / 2 + "px",
                            width: 700,
                            height: 790,
                            cursor: "default",
                            background: "#333",
                            border: 0
                        }
                    });
                    $(".carousel").carousel("cycle");
                });
         }
    }
})(jQuery);