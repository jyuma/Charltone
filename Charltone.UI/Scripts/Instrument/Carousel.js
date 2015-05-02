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
                        $('.carousel').carousel('cycle');
                        $(".carouselcloselink").click(function () {
                            $(carousel).html('');
                            $.unblockUI();
                        });
                    }
                    $.blockUI({
                        message: $(carousel),
                        css: {
                            top: 30,
                            left: ($(window).width() - 860) / 2 + 'px',
                            width: 860,
                            height: 810,
                            padding: 0,
                            cursor: 'default',
                            background: '#333',
                            overflow: 'hidden',
                            border: 0
                }
                    });
            });
         }
    }
})(jQuery);