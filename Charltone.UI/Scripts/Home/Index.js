/*!
 * Charltone home.index
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.home = {};

    home.index = {
        init: function (options) {

            var config = {
                maxImageWidth: 0,
                maxImageHeight: 0
            };

            $.extend(config, options);

            file.upload.init(options, function () { window.location.reload(); });
        }
    }
})(jQuery);